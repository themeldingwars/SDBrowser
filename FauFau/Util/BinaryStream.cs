using System;
using System.IO;
using System.Runtime.InteropServices;

namespace FauFau.Util
{
    public class BinaryStream : IDisposable
    {

        private Endianness bitOrder;
        private Endianness byteOrder;
        private TextEncoding defaultTextEncoding;

        public BitStream baseStream;
        public BinaryReader Read;
        public BinaryWriter Write;

        public BinaryStream()
        {
            Initialize(null, Endianness.LittleEndian, Endianness.LittleEndian, TextEncoding.ASCII);
        }
        public BinaryStream(Stream stream)
        {
            Initialize(stream, Endianness.LittleEndian, Endianness.LittleEndian, TextEncoding.ASCII);
        }
        public BinaryStream(Stream stream, Endianness byteOrder = Endianness.LittleEndian, Endianness bitOrder = Endianness.LittleEndian, TextEncoding textEncoding = TextEncoding.ASCII)
        {
            Initialize(stream, byteOrder, bitOrder, textEncoding);
        }
        public BinaryStream(Stream stream, TextEncoding textEncoding = TextEncoding.ASCII, Endianness byteOrder = Endianness.LittleEndian, Endianness bitOrder = Endianness.LittleEndian)
        {
            Initialize(stream, byteOrder, bitOrder, textEncoding);
        }
        public BinaryStream(Endianness byteOrder = Endianness.LittleEndian, Endianness bitOrder = Endianness.LittleEndian, TextEncoding textEncoding = TextEncoding.ASCII)
        {
            Initialize(null, byteOrder, bitOrder, textEncoding);
        }
        public BinaryStream(TextEncoding textEncoding = TextEncoding.ASCII, Endianness byteOrder = Endianness.LittleEndian, Endianness bitOrder = Endianness.LittleEndian)
        {
            Initialize(null, byteOrder, bitOrder, textEncoding);
        }
        private void Initialize(Stream stream, Endianness byteOrder, Endianness bitOrder, TextEncoding textEncoding)
        {
            this.baseStream = new BitStream(stream, (bitOrder == Endianness.BigEndian));
            this.byteOrder = byteOrder;
            this.bitOrder = bitOrder;
            this.defaultTextEncoding = textEncoding;
            this.Read = new BinaryReader(this);
            this.Write = new BinaryWriter(this);
        }

        /// <summary>
        /// Order of the bits to be read & written in.
        /// </summary>
        public Endianness BitOrder
        {
            get
            {
                return bitOrder;
            }
            set
            {
                bitOrder = value;
                baseStream.MostSignificantBit = (bitOrder == Endianness.BigEndian);
            }
        }

        /// <summary>
        /// Order of the bytes to be read & written in.
        /// </summary>
        public Endianness ByteOrder
        {
            get { return byteOrder; }
            set { byteOrder = value; }
        }

        /// <summary>
        /// Default encoding to be used on string.
        /// </summary>
        public TextEncoding DefaultTextEncoding
        {
            get { return defaultTextEncoding; }
            set
            {
                if (value == TextEncoding.DEFAULT)
                {
                    value = TextEncoding.ASCII;
                }
                defaultTextEncoding = value;
            }
        }

        /// <summary>
        /// Returns the current length of the stream.
        /// </summary>
        public long Length
        {
            get { return baseStream.Length; }
        }

        /// <summary>
        /// Checks if we're on the last byte in the stream.
        /// </summary>
        public bool EndOfStream
        {
            get { return baseStream.EndOfStream; }
        }

        /// <summary>
        /// Returns or sets our current byte position in the stream.
        /// </summary>
        public long ByteOffset
        {
            get { return baseStream.ByteOffset; }
            set { baseStream.ByteOffset = value; }
        }

        /// <summary>
        /// Returns or sets our current bit position on the current byte.
        /// </summary>
        public int BitOffset
        {
            get { return baseStream.BitOffset; }
            set { baseStream.BitOffset = value; }
        }

        /// <summary>
        /// Writes dirty bits to the stream.
        /// </summary>
        public void Flush()
        {
            baseStream.Flush();
        }

        /// <summary>
        /// Closes the underlying stream, it's better if you properly dispose of it though.
        /// </summary>
        public void Close()
        {
            if (baseStream != null)
            {
                baseStream.Flush();
                baseStream.Close();
            }
        }

        /// <summary>
        /// Releases all the resources used by the stream.
        /// </summary>
        public void Dispose()
        {
            if (baseStream != null)
            {
                baseStream.Dispose();
            }
        }

        public enum Endianness
        {
            LittleEndian,
            BigEndian
        }
        public enum TextEncoding
        {
            DEFAULT,
            ASCII,
            UTF7,
            UTF8,
            UTF32,
            UNICODE
        }
    }
}
