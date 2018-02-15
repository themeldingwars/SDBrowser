using System;
using System.IO;

namespace FauFau.Util
{
    public sealed class BitStream : IDisposable
    {
        private Stream stream;
        private int bitOffset = 0;
        private byte bitBuffer = 0;
        private bool bitBufferDirty = false;
        private long byteOffset = 0;
        private bool msb = false;

        public BitStream(bool MostSignificantBit = false)
        {
            this.stream = new MemoryStream();
            this.MostSignificantBit = MostSignificantBit;
            if (!EndOfStream)
            {
                bitBuffer = (byte)stream.ReadByte();
            }
        }
        public BitStream(Stream stream, bool MostSignificantBit = false)
        {
            if (stream == null)
            {
                this.stream = new MemoryStream();
            }
            else
            {
                this.stream = stream;
            }
            this.MostSignificantBit = MostSignificantBit;
            if (!EndOfStream)
            {
                bitBuffer = (byte)stream.ReadByte();
            }
        }

        /// <summary>
        /// What bit order sould we read in? Most significant bit is a tad slower to read/write.
        /// </summary>
        public bool MostSignificantBit
        {
            get { return msb; }
            set { msb = value; }
        }

        /// <summary>
        /// Returns the current length of the stream.
        /// </summary>
        public long Length
        {
            get
            {
                return stream.Length;
            }
        }

        /// <summary>
        /// Checks if we're on the last byte in the stream.
        /// </summary>
        public bool EndOfStream
        {
            get { return stream.Length == stream.Position; }
        }

        /// <summary>
        /// Returns or sets our current byte position in the stream.
        /// </summary>
        public long ByteOffset
        {
            get { return byteOffset; }
            set
            {
                Flush();
                byteOffset = value;
                stream.Position = byteOffset;
                bitBuffer = (byte)stream.ReadByte();
            }
        }

        /// <summary>
        /// Returns or sets our current bit position on the current byte.
        /// </summary>
        public int BitOffset
        {
            get { return bitOffset; }
            set
            {
                unchecked
                {
                    if ((uint)value > 7)
                    {
                        int bytes = (int)(value / 8);
                        Flush();
                        byteOffset += bytes;
                        stream.Position = byteOffset;
                        bitBuffer = (byte)stream.ReadByte();
                        bitOffset = (value - (bytes * 8));
                    }
                    else
                    {
                        bitOffset = value;
                    }
                }
            }
        }

        /// <summary>
        /// Writes dirty bits to the stream.
        /// </summary>
        public void Flush()
        {
            if (bitBufferDirty)
            {
                stream.Position = byteOffset;
                stream.WriteByte(bitBuffer);
                bitBufferDirty = false;
            }
        }

        /// <summary>
        /// Reads a single bit from the stream.
        /// </summary>
        public byte ReadBit()
        {
            byte value;
            if (msb)
            {
                value = (byte)((bitBuffer >> (7 - bitOffset)) & 1);
            }
            else
            {
                value = (byte)((bitBuffer >> (bitOffset)) & 1);
            }
            BitOffset = bitOffset + 1;
            return value;
        }

        /// <summary>
        /// Reads n bits from the stream.
        /// </summary>
        public byte[] ReadBit(int length)
        {
            byte[] ret = new byte[length];
            if (msb)
            {
                for (int i = 0; i < length; i++)
                {
                    ret[i] = (byte)((bitBuffer >> (7 - bitOffset)) & 1);
                    if (bitOffset == 7)
                    {
                        byteOffset++;
                        stream.Position = byteOffset;
                        bitBuffer = (byte)stream.ReadByte();
                        bitOffset = 0;
                    }
                    else
                    {
                        bitOffset++;
                    }
                }
            }
            else
            {
                for (int i = 0; i < length; i++)
                {
                    ret[i] = (byte)((bitBuffer >> (bitOffset)) & 1);
                    if (bitOffset == 7)
                    {
                        byteOffset++;
                        stream.Position = byteOffset;
                        bitBuffer = (byte)stream.ReadByte();
                        bitOffset = 0;
                    }
                    else
                    {
                        bitOffset++;
                    }
                }
            }
            return ret;
        }

        /// <summary>
        /// Writes a single bit to the stream.
        /// </summary>
        public void WriteBit(byte value)
        {
            if (value == 1)
            {
                bitBuffer |= (byte)(1 << (msb ? 7 - bitOffset : bitOffset));
            }
            else
            {
                bitBuffer &= (byte)~(1 << (msb ? 7 - bitOffset : bitOffset));
            }
            bitBufferDirty = true;
            BitOffset = bitOffset + 1;
        }

        /// <summary>
        /// Writes n bits to the stream.
        /// </summary>
        public void WriteBit(byte[] value)
        {
            for (int i = 0; i < value.Length; i++)
            {
                if (value[i] == 1)
                {
                    bitBuffer |= (byte)(1 << (msb ? 7 - bitOffset : bitOffset));
                }
                else
                {
                    bitBuffer &= (byte)~(1 << (msb ? 7 - bitOffset : bitOffset));
                }
                bitBufferDirty = true;
                if (bitOffset == 7)
                {
                    Flush();
                    byteOffset++;
                    stream.Position = byteOffset;
                    bitBuffer = (byte)stream.ReadByte();
                    bitOffset = 0;
                }
                else
                {
                    bitOffset++;
                }
            }
        }

        /// <summary>
        /// Reads a single byte from the stream.
        /// </summary>
        public byte ReadByte()
        {
            if (bitOffset == 0) // we aligned boyos!
            {
                byte ret = (msb ? BitReverseTable[bitBuffer] : bitBuffer);
                byteOffset++;
                stream.Position = byteOffset;
                bitBuffer = (byte)stream.ReadByte();
                return ret;
            }
            else
            {
                return _ReadBitsAsBytes(1, true)[0];
            }
        }

        /// <summary>
        /// Reads n bytes from the stream.
        /// </summary>
        public byte[] ReadByte(int length)
        {
            if (bitOffset == 0)
            {
                byte[] ret = new byte[length];

                if (stream.Position == byteOffset + 1)
                {
                    ret[0] = bitBuffer;
                    stream.Read(ret, 1, length - 1);
                }
                else
                {
                    stream.Position = byteOffset;
                    stream.Read(ret, 0, length);
                }
                
                byteOffset += length;
                bitBuffer = (byte)stream.ReadByte();
                if (msb)
                {
                    for (int i = 0; i < ret.Length; i++)
                    {
                        ret[i] = BitReverseTable[ret[i]];
                    }
                }
                return ret;
            }
            else
            {
                return _ReadBitsAsBytes(length, true);
            }
        }

        /// <summary>
        /// Reads a single byte of variable bitlength from the stream.
        /// </summary>
        public byte ReadByteFromBits(int length)
        {
            return _ReadBitsAsBytes(length)[0];
        }

        /// <summary>
        /// Reads n bits from the stream and packs them as bytes.
        /// </summary>
        public byte[] ReadBytesFromBits(int length)
        {
            return _ReadBitsAsBytes(length);
        }

        /// <summary>
        /// Writes a single byte to the stream.
        /// </summary>
        public void WriteByte(byte value)
        {
            if (bitOffset == 0)
            {
                stream.Position = byteOffset;
                stream.WriteByte((msb ? BitReverseTable[value] : value));
                byteOffset++;
                bitBuffer = (byte)stream.ReadByte();
            }
            else
            {
                _WriteBytesAsBits(new byte[] { value }, 1, true);
            }
        }

        /// <summary>
        /// Writes n bytes from the stream.
        /// </summary>
        public void WriteByte(byte[] data)
        {
            if (bitOffset == 0)
            {
                stream.Position = byteOffset;
                if (msb)
                {
                    for (int i = 0; i < data.Length; i++)
                    {
                        data[i] = BitReverseTable[data[i]];
                    }
                }
                stream.Write(data, 0, data.Length);
                byteOffset += data.Length;
                bitBuffer = (byte)stream.ReadByte();
            }
            else
            {
                _WriteBytesAsBits(data, data.Length, true);
            }
        }

        /// <summary>
        /// Writes a single byte of variable bitlength to the stream.
        /// </summary>
        public void WriteByteAsBits(byte data, int length)
        {
            _WriteBytesAsBits(new byte[] { data }, length);
        }

        /// <summary>
        /// Writes n bits to stream from supplied byte array.
        /// </summary>
        public void WriteBytesAsBits(byte[] data, int length)
        {
            _WriteBytesAsBits(data, length);
        }

        /// <summary>
        /// Copies this streams content to another stream.
        /// </summary>
        public void CopyTo(Stream destination)
        {
            Flush();
            stream.CopyTo(destination);
        }

        /// <summary>
        /// Releases all the resources used by the stream.
        /// </summary>
        public void Dispose()
        {
            if (stream != null)
            {
                Flush();
                stream.Dispose();
            }
        }

        /// <summary>
        /// Closes the underlying stream, it's better if you properly dispose of it though.
        /// </summary>
        public void Close()
        {
            if (stream != null)
            {
                Flush();
                stream.Close();
            }
        }

        private byte[] _ReadBitsAsBytes(int length, bool lengthInBytes = false)
        {
            int l = length;
            if (lengthInBytes)
            {
                length *= 8;
            }
            else
            {
                l = length >> 3;
                if (l << 3 != length)
                {
                    l++;
                }

            }

            byte[] bits = ReadBit(length);
            byte[] res = new byte[l];

            for (int i = 0, x = 0; i < length;)
            {
                for (int p = 0; p < 8 && i < length; i++, p++)
                {
                    res[x] |= (byte)(bits[i] << p);
                }
                x++;
            }
            return res;
        }
        private void _WriteBytesAsBits(byte[] data, int length, bool lengthInBytes = false)
        {
            if (lengthInBytes)
            {
                length *= 8;
            }
            byte[] bits = new byte[length];
            for (int i = 0, position = 0; i < length;)
            {
                for (int p = 0; p < 8 && i < length; i++, p++)
                {
                    bits[i] = (byte)(data[position] >> p & 1);
                }
                position++;
            }
            WriteBit(bits);
        }

        private static byte[] BitReverseTable =
        {
            0x00, 0x80, 0x40, 0xc0, 0x20, 0xa0, 0x60, 0xe0,
            0x10, 0x90, 0x50, 0xd0, 0x30, 0xb0, 0x70, 0xf0,
            0x08, 0x88, 0x48, 0xc8, 0x28, 0xa8, 0x68, 0xe8,
            0x18, 0x98, 0x58, 0xd8, 0x38, 0xb8, 0x78, 0xf8,
            0x04, 0x84, 0x44, 0xc4, 0x24, 0xa4, 0x64, 0xe4,
            0x14, 0x94, 0x54, 0xd4, 0x34, 0xb4, 0x74, 0xf4,
            0x0c, 0x8c, 0x4c, 0xcc, 0x2c, 0xac, 0x6c, 0xec,
            0x1c, 0x9c, 0x5c, 0xdc, 0x3c, 0xbc, 0x7c, 0xfc,
            0x02, 0x82, 0x42, 0xc2, 0x22, 0xa2, 0x62, 0xe2,
            0x12, 0x92, 0x52, 0xd2, 0x32, 0xb2, 0x72, 0xf2,
            0x0a, 0x8a, 0x4a, 0xca, 0x2a, 0xaa, 0x6a, 0xea,
            0x1a, 0x9a, 0x5a, 0xda, 0x3a, 0xba, 0x7a, 0xfa,
            0x06, 0x86, 0x46, 0xc6, 0x26, 0xa6, 0x66, 0xe6,
            0x16, 0x96, 0x56, 0xd6, 0x36, 0xb6, 0x76, 0xf6,
            0x0e, 0x8e, 0x4e, 0xce, 0x2e, 0xae, 0x6e, 0xee,
            0x1e, 0x9e, 0x5e, 0xde, 0x3e, 0xbe, 0x7e, 0xfe,
            0x01, 0x81, 0x41, 0xc1, 0x21, 0xa1, 0x61, 0xe1,
            0x11, 0x91, 0x51, 0xd1, 0x31, 0xb1, 0x71, 0xf1,
            0x09, 0x89, 0x49, 0xc9, 0x29, 0xa9, 0x69, 0xe9,
            0x19, 0x99, 0x59, 0xd9, 0x39, 0xb9, 0x79, 0xf9,
            0x05, 0x85, 0x45, 0xc5, 0x25, 0xa5, 0x65, 0xe5,
            0x15, 0x95, 0x55, 0xd5, 0x35, 0xb5, 0x75, 0xf5,
            0x0d, 0x8d, 0x4d, 0xcd, 0x2d, 0xad, 0x6d, 0xed,
            0x1d, 0x9d, 0x5d, 0xdd, 0x3d, 0xbd, 0x7d, 0xfd,
            0x03, 0x83, 0x43, 0xc3, 0x23, 0xa3, 0x63, 0xe3,
            0x13, 0x93, 0x53, 0xd3, 0x33, 0xb3, 0x73, 0xf3,
            0x0b, 0x8b, 0x4b, 0xcb, 0x2b, 0xab, 0x6b, 0xeb,
            0x1b, 0x9b, 0x5b, 0xdb, 0x3b, 0xbb, 0x7b, 0xfb,
            0x07, 0x87, 0x47, 0xc7, 0x27, 0xa7, 0x67, 0xe7,
            0x17, 0x97, 0x57, 0xd7, 0x37, 0xb7, 0x77, 0xf7,
            0x0f, 0x8f, 0x4f, 0xcf, 0x2f, 0xaf, 0x6f, 0xef,
            0x1f, 0x9f, 0x5f, 0xdf, 0x3f, 0xbf, 0x7f, 0xff
        };
    }
}
