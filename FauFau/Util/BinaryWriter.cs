using System;
using System.Collections.Generic;
using System.Text;
using static FauFau.Util.BinaryStream;
using static FauFau.Util.BinaryUtil;

namespace FauFau.Util
{
    public class BinaryWriter
    {
        private BinaryStream stream;

        private byte[] _twoByteBuffer = new byte[2];
        private byte[] _fourByteBuffer = new byte[4];
        private byte[] _eightByteBuffer = new byte[8];
        private FloatByteMap _floatByteMapBuffer = new FloatByteMap();
        private FloatUIntMap _floatUIntMapBuffer = new FloatUIntMap();
        private DoubleByteMap _doubleByteMapBuffer = new DoubleByteMap();
        private DoubleULongMap _doubleULongMapBuffer = new DoubleULongMap();
        private Encoding _textEncoder;
        private int _textCharacterWidth;

        public BinaryWriter(BinaryStream stream)
        {
            this.stream = stream;
        }

        // -- Bit

        /// <summary>
        /// Writes a single bit to the stream.
        /// </summary>
        public void Bit(byte bit)
        {
            stream.baseStream.WriteBit(bit);
        }

        /// <summary>
        /// Writes an array of bits to the stream.
        /// </summary>
        public void BitArray(byte[] bits)
        {
            stream.baseStream.WriteBit(bits);
        }

        /// <summary>
        /// Writes a list of bits to the stream.
        /// </summary>
        public void BitList(List<byte> bits)
        {
            stream.baseStream.WriteBit(bits.ToArray());
        }

        // -- Signed Byte

        /// <summary>
        /// Writes a single signed byte to the stream.
        /// </summary>
        public void SByte(sbyte value)
        {
            stream.baseStream.WriteByte((byte)value);
        }

        /// <summary>
        /// Writes an array of signed bytes to the stream.
        /// </summary>
        public void SByteArray(sbyte[] value)
        {
            byte[] data = new byte[value.Length];
            Buffer.BlockCopy(value, 0, data, 0, value.Length);
            stream.baseStream.WriteByte(data);
        }

        /// <summary>
        /// Writes a list of signed bytes to the stream.
        /// </summary>
        public void SByteList(List<sbyte> value)
        {
            byte[] data = new byte[value.Count];
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = (byte)value[i];
            }
            stream.baseStream.WriteByte(data);
        }

        /// <summary>
        /// Writes a single signed byte of variable bit length to the stream.
        /// </summary>
        public void SByteAsBits(sbyte value, int numBits)
        {
            ByteAsBits((byte)value, numBits);
        }

        /// <summary>
        /// Writes an array of signed bytes of variable bit length to the stream.
        /// </summary>
        public void SByteArrayAsBits(sbyte[] value, int numBits)
        {
            byte[] data = new byte[value.Length];
            Buffer.BlockCopy(value, 0, data, 0, value.Length);
            ByteArrayAsBits(data, numBits);
        }

        /// <summary>
        /// Writes a list of signed bytes of variable bit length to the stream.
        /// </summary>
        public void SByteListAsBits(List<sbyte> value, int numBits)
        {
            byte[] data = new byte[value.Count];
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = (byte)value[i];
            }
            ByteArrayAsBits(data, numBits);
        }

        // -- Short

        /// <summary>
        /// Writes a single short to the stream.
        /// </summary>
        public void Short(short value)
        {
            UShort((ushort)value);
        }

        /// <summary>
        /// Writes an array of shorts to the stream.
        /// </summary>
        public void ShortArray(short[] value)
        {
            byte[] buffer = new byte[value.Length * 2];
            if (stream.ByteOrder == Endianness.LittleEndian)
            {
                for (int i = 0; i < value.Length; i++)
                {
                    WriteToBufferLE(ref buffer, (ushort)value[i], i * 2);
                }
            }
            else
            {
                for (int i = 0; i < value.Length; i++)
                {
                    WriteToBufferBE(ref buffer, (ushort)value[i], i * 2);
                }
            }
            stream.baseStream.WriteByte(buffer);
        }

        /// <summary>
        /// Writes a list of shorts to the stream.
        /// </summary>
        public void ShortList(List<short> value)
        {
            byte[] buffer = new byte[value.Count * 2];
            if (stream.ByteOrder == Endianness.LittleEndian)
            {
                for (int i = 0; i < value.Count; i++)
                {
                    WriteToBufferLE(ref buffer, (ushort)value[i], i * 2);
                }
            }
            else
            {
                for (int i = 0; i < value.Count; i++)
                {
                    WriteToBufferBE(ref buffer, (ushort)value[i], i * 2);
                }
            }
            stream.baseStream.WriteByte(buffer);
        }

        /// <summary>
        /// Writes a short of variable bit length to the stream.
        /// </summary>
        public void ShortAsBits(short value, int numBits)
        {
            ushort val = (ushort)value;
            // move sign
            if (value < 0)
            {
                val |= (ushort)(1 << numBits - 1);
            }
            else
            {
                val = val &= (ushort)~(1 << numBits - 1);
            }
            UShortAsBits(val, numBits);
        }

        /// <summary>
        /// Writes an array of shorts of variable bit length to the stream.
        /// </summary>
        public void ShortArrayAsBits(short[] value, int numBitsPerValue)
        {
            for (int i = 0; i < value.Length; i++)
            {
                UShortAsBits((ushort)value[i], numBitsPerValue);
            }
        }

        /// <summary>
        /// Writes a list of shorts of variable bit length to the stream.
        /// </summary>
        public void ShortListAsBits(List<short> value, int numBitsPerValue)
        {
            for (int i = 0; i < value.Count; i++)
            {
                UShortAsBits((ushort)value[i], numBitsPerValue);
            }
        }

        // -- Int

        /// <summary>
        /// Writes a single int to the stream.
        /// </summary>
        public void Int(int value)
        {
            UInt((uint)value);
        }

        /// <summary>
        /// Writes an array of ints to the stream.
        /// </summary>
        public void IntArray(int[] value)
        {
            byte[] buffer = new byte[value.Length * 4];
            if (stream.ByteOrder == Endianness.LittleEndian)
            {
                for (int i = 0; i < value.Length; i++)
                {
                    WriteToBufferLE(ref buffer, (uint)value[i], i * 4);
                }
            }
            else
            {
                for (int i = 0; i < value.Length; i++)
                {
                    WriteToBufferBE(ref buffer, (uint)value[i], i * 4);
                }
            }
            stream.baseStream.WriteByte(buffer);
        }

        /// <summary>
        /// Writes a list of ints to the stream.
        /// </summary>
        public void IntList(List<int> value)
        {
            byte[] buffer = new byte[value.Count * 4];
            if (stream.ByteOrder == Endianness.LittleEndian)
            {
                for (int i = 0; i < value.Count; i++)
                {
                    WriteToBufferLE(ref buffer, (uint)value[i], i * 4);
                }
            }
            else
            {
                for (int i = 0; i < value.Count; i++)
                {
                    WriteToBufferBE(ref buffer, (uint)value[i], i * 4);
                }
            }
            stream.baseStream.WriteByte(buffer);
        }

        /// <summary>
        /// Writes an int of variable bit length to the stream.
        /// </summary>
        public void IntAsBits(int value, int numBits)
        {
            uint val = (uint)value;
            // move sign
            if (value < 0)
            {
                val |= (uint)(1 << numBits - 1);
            }
            else
            {
                val = val &= (uint)~(1 << numBits - 1);
            }
            UIntAsBits(val, numBits);
        }

        /// <summary>
        /// Writes an array of ints of variable bit length to the stream.
        /// </summary>
        public void IntArrayAsBits(int[] value, int numBitsPerValue)
        {
            for (int i = 0; i < value.Length; i++)
            {
                UIntAsBits((uint)value[i], numBitsPerValue);
            }
        }

        /// <summary>
        /// Writes a list of ints of variable bit length to the stream.
        /// </summary>

        public void IntListAsBits(List<int> value, int numBitsPerValue)
        {
            for (int i = 0; i < value.Count; i++)
            {
                UIntAsBits((uint)value[i], numBitsPerValue);
            }
        }

        // -- Long

        /// <summary>
        /// Writes a single long to the stream.
        /// </summary>
        public void Long(long value)
        {
            ULong((ulong)value);
        }

        /// <summary>
        /// Writes an array of longs to the stream.
        /// </summary>
        public void LongArray(long[] value)
        {
            byte[] buffer = new byte[value.Length * 8];
            if (stream.ByteOrder == Endianness.LittleEndian)
            {
                for (int i = 0; i < value.Length; i++)
                {
                    WriteToBufferLE(ref buffer, (ulong)value[i], i * 8);
                }
            }
            else
            {
                for (int i = 0; i < value.Length; i++)
                {
                    WriteToBufferBE(ref buffer, (ulong)value[i], i * 8);
                }
            }
            stream.baseStream.WriteByte(buffer);
        }

        /// <summary>
        /// Writes a list of longs to the stream.
        /// </summary>
        public void LongList(List<long> value)
        {
            byte[] buffer = new byte[value.Count * 8];
            if (stream.ByteOrder == Endianness.LittleEndian)
            {
                for (int i = 0; i < value.Count; i++)
                {
                    WriteToBufferLE(ref buffer, (ulong)value[i], i * 8);
                }
            }
            else
            {
                for (int i = 0; i < value.Count; i++)
                {
                    WriteToBufferBE(ref buffer, (ulong)value[i], i * 8);
                }
            }
            stream.baseStream.WriteByte(buffer);
        }

        /// <summary>
        /// Writes a long of variable bit length to the stream.
        /// </summary>
        public void LongAsBits(long value, int numBits)
        {
            ulong val = (ulong)value;
            // move sign
            if (value < 0)
            {
                val |= (ulong)(1 << numBits - 1);
            }
            else
            {
                val = val &= (ulong)~(1 << numBits - 1);
            }
            ULongAsBits(val, numBits);
        }

        /// <summary>
        /// Writes an array of longs of variable bit length to the stream.
        /// </summary>
        public void LongArrayAsBits(long[] value, int numBitsPerValue)
        {
            for (int i = 0; i < value.Length; i++)
            {
                ULongAsBits((ulong)value[i], numBitsPerValue);
            }
        }

        /// <summary>
        /// Writes a list of longs of variable bit length to the stream.
        /// </summary>
        public void LongAsBits(List<long> value, int numBitsPerValue)
        {
            for (int i = 0; i < value.Count; i++)
            {
                ULongAsBits((ulong)value[i], numBitsPerValue);
            }
        }

        // -- Byte

        /// <summary>
        /// Writes a single byte to the stream.
        /// </summary>
        public void Byte(byte value)
        {
            stream.baseStream.WriteByte(value);
        }

        /// <summary>
        /// Writes an array of bytes to the stream.
        /// </summary>
        public void ByteArray(byte[] bytes)
        {
            stream.baseStream.WriteByte(bytes);
        }

        /// <summary>
        /// Writes a list of bytes to the stream.
        /// </summary>
        public void ByteList(List<byte> bytes)
        {
            stream.baseStream.WriteByte(bytes.ToArray());
        }

        /// <summary>
        /// Writes a byte of variable bit length to the stream.
        /// </summary>
        public void ByteAsBits(byte value, int numBits)
        {
            stream.baseStream.WriteByteAsBits(value, numBits);
        }

        /// <summary>
        /// Writes an array of bytes of variable bit length to the stream.
        /// </summary>
        public void ByteArrayAsBits(byte[] bytes, int numBits)
        {
            for (int i = 0; i < bytes.Length; i++)
            {
                stream.baseStream.WriteByteAsBits(bytes[i], numBits);
            }
        }

        /// <summary>
        /// Writes a list of bytes of variable bit length to the stream.
        /// </summary>
        public void ByteListAsBits(List<byte> bytes, int numBits)
        {
            for (int i = 0; i < bytes.Count; i++)
            {
                stream.baseStream.WriteByteAsBits(bytes[i], numBits);
            }
        }

        // -- UShort

        /// <summary>
        /// Writes a single ushort to the stream.
        /// </summary>
        public void UShort(ushort value)
        {
            if (stream.ByteOrder == Endianness.LittleEndian)
            {
                WriteToBufferLE(ref _twoByteBuffer, value);
            }
            else
            {
                WriteToBufferBE(ref _twoByteBuffer, value);
            }
            stream.baseStream.WriteByte(_twoByteBuffer);
        }

        /// <summary>
        /// Writes an array of ushorts to the stream.
        /// </summary>
        public void UShortArray(ushort[] value)
        {
            byte[] buffer = new byte[value.Length * 2];
            if (stream.ByteOrder == Endianness.LittleEndian)
            {
                for (int i = 0; i < value.Length; i++)
                {
                    WriteToBufferLE(ref buffer, value[i], i * 2);
                }
            }
            else
            {
                for (int i = 0; i < value.Length; i++)
                {
                    WriteToBufferBE(ref buffer, value[i], i * 2);
                }
            }
            stream.baseStream.WriteByte(buffer);
        }

        /// <summary>
        /// Writes a list of ushorts to the stream.
        /// </summary>
        public void UShortList(List<ushort> value)
        {
            byte[] buffer = new byte[value.Count * 2];
            if (stream.ByteOrder == Endianness.LittleEndian)
            {
                for (int i = 0; i < value.Count; i++)
                {
                    WriteToBufferLE(ref buffer, value[i], i * 2);
                }
            }
            else
            {
                for (int i = 0; i < value.Count; i++)
                {
                    WriteToBufferBE(ref buffer, value[i], i * 2);
                }
            }
            stream.baseStream.WriteByte(buffer);
        }

        /// <summary>
        /// Writes an ushort of variable bit length to the stream.
        /// </summary>
        public void UShortAsBits(ushort value, int numBits)
        {
            WriteToBufferLE(ref _twoByteBuffer, value);
            stream.baseStream.WriteBytesAsBits(_twoByteBuffer, numBits);
        }

        /// <summary>
        /// Writes an array of ushorts of variable bit length to the stream.
        /// </summary>
        public void UShortArrayAsBits(ushort[] value, int numBitsPerValue)
        {
            for (int i = 0; i < value.Length; i++)
            {
                UShortAsBits(value[i], numBitsPerValue);
            }
        }

        /// <summary>
        /// Writes a list of ushorts of variable bit length to the stream.
        /// </summary>
        public void UShortListAsBits(List<ushort> value, int numBitsPerValue)
        {
            for (int i = 0; i < value.Count; i++)
            {
                UShortAsBits(value[i], numBitsPerValue);
            }
        }

        // -- UInt

        /// <summary>
        /// Writes a single uint to the stream.
        /// </summary>
        public void UInt(uint value)
        {
            if (stream.ByteOrder == Endianness.LittleEndian)
            {
                WriteToBufferLE(ref _fourByteBuffer, value);
            }
            else
            {
                WriteToBufferBE(ref _fourByteBuffer, value);
            }
            stream.baseStream.WriteByte(_fourByteBuffer);
        }

        /// <summary>
        /// Writes an array of uints to the stream.
        /// </summary>
        public void UIntArray(uint[] value)
        {
            byte[] buffer = new byte[value.Length * 4];
            if (stream.ByteOrder == Endianness.LittleEndian)
            {
                for (int i = 0; i < value.Length; i++)
                {
                    WriteToBufferLE(ref buffer, value[i], i * 4);
                }
            }
            else
            {
                for (int i = 0; i < value.Length; i++)
                {
                    WriteToBufferBE(ref buffer, value[i], i * 4);
                }
            }
            stream.baseStream.WriteByte(buffer);
        }

        /// <summary>
        /// Writes a list of uints to the stream.
        /// </summary>
        public void UIntList(List<uint> value)
        {
            byte[] buffer = new byte[value.Count * 4];
            if (stream.ByteOrder == Endianness.LittleEndian)
            {
                for (int i = 0; i < value.Count; i++)
                {
                    WriteToBufferLE(ref buffer, value[i], i * 4);
                }
            }
            else
            {
                for (int i = 0; i < value.Count; i++)
                {
                    WriteToBufferBE(ref buffer, value[i], i * 4);
                }
            }
            stream.baseStream.WriteByte(buffer);
        }

        /// <summary>
        /// Writes an uint of variable bit length to the stream.
        /// </summary>
        public void UIntAsBits(uint value, int numBits)
        {
            WriteToBufferLE(ref _fourByteBuffer, value);
            stream.baseStream.WriteBytesAsBits(_fourByteBuffer, numBits);
        }

        /// <summary>
        /// Writes an array of uints of variable bit length to the stream.
        /// </summary>
        public void UIntArrayAsBits(uint[] value, int numBitsPerValue)
        {
            for (int i = 0; i < value.Length; i++)
            {
                UIntAsBits(value[i], numBitsPerValue);
            }
        }

        /// <summary>
        /// Writes a list of uints of variable bit length to the stream.
        /// </summary>
        public void UIntListAsBits(List<uint> value, int numBitsPerValue)
        {
            for (int i = 0; i < value.Count; i++)
            {
                UIntAsBits(value[i], numBitsPerValue);
            }
        }

        // -- ULong

        /// <summary>
        /// Writes a single ulong to the stream.
        /// </summary>
        public void ULong(ulong value)
        {
            if (stream.ByteOrder == Endianness.LittleEndian)
            {
                WriteToBufferLE(ref _eightByteBuffer, value);
            }
            else
            {
                WriteToBufferBE(ref _eightByteBuffer, value);
            }
            stream.baseStream.WriteByte(_eightByteBuffer);
        }

        /// <summary>
        /// Writes an array of ulongs to the stream.
        /// </summary>
        public void ULongArray(ulong[] value)
        {
            byte[] buffer = new byte[value.Length * 8];
            if (stream.ByteOrder == Endianness.LittleEndian)
            {
                for (int i = 0; i < value.Length; i++)
                {
                    WriteToBufferLE(ref buffer, value[i], i * 8);
                }
            }
            else
            {
                for (int i = 0; i < value.Length; i++)
                {
                    WriteToBufferLE(ref buffer, value[i], i * 8);
                }
            }
            stream.baseStream.WriteByte(buffer);
        }

        /// <summary>
        /// Writes a list of ulongs to the stream.
        /// </summary>
        public void ULongList(List<ulong> value)
        {
            byte[] buffer = new byte[value.Count * 8];
            if (stream.ByteOrder == Endianness.LittleEndian)
            {
                for (int i = 0; i < value.Count; i++)
                {
                    WriteToBufferLE(ref buffer, value[i], i * 8);
                }
            }
            else
            {
                for (int i = 0; i < value.Count; i++)
                {
                    WriteToBufferLE(ref buffer, value[i], i * 8);
                }
            }
            stream.baseStream.WriteByte(buffer);
        }


        /// <summary>
        /// Writes an ulong of variable bit length to the stream.
        /// </summary>
        public void ULongAsBits(ulong value, int numBits)
        {
            WriteToBufferLE(ref _eightByteBuffer, value);
            stream.baseStream.WriteBytesAsBits(_eightByteBuffer, numBits);
        }

        /// <summary>
        /// Writes an array of ulongs of variable bit length to the stream.
        /// </summary>
        public void ULongArrayAsBits(ulong[] value, int numBitsPerValue)
        {
            for (int i = 0; i < value.Length; i++)
            {
                ULongAsBits(value[i], numBitsPerValue);
            }
        }

        /// <summary>
        /// Writes a list of ulongs of variable bit length to the stream.
        /// </summary>
        public void ULongListAsBits(List<ulong> value, int numBitsPerValue)
        {
            for (int i = 0; i < value.Count; i++)
            {
                ULongAsBits(value[i], numBitsPerValue);
            }
        }

        // -- Half

        /// <summary>
        /// Writes a single int to the stream.
        /// </summary>
        public void Half(float value)
        {
            _floatUIntMapBuffer.Float = value;
            uint u = _floatUIntMapBuffer.UInt;
            UShort((ushort)(HalfLookup.Base[(u >> 23) & 0x1ff] + ((u & 0x007fffff) >> HalfLookup.Shift[u >> 23])));
        }

        /// <summary>
        /// Writes an array of halfs to the stream.
        /// </summary>
        public void HalfArray(float[] value)
        {
            ushort[] us = new ushort[value.Length];
            for (int i = 0; i < value.Length; i++)
            {
                _floatUIntMapBuffer.Float = value[i];
                uint u = _floatUIntMapBuffer.UInt;
                us[i] = (ushort)(HalfLookup.Base[(u >> 23) & 0x1ff] + ((u & 0x007fffff) >> HalfLookup.Shift[u >> 23]));
            }
            UShortArray(us);
        }

        /// <summary>
        /// Writes a list of halfs to the stream.
        /// </summary>
        public void HalfList(List<float> value)
        {
            ushort[] us = new ushort[value.Count];
            for (int i = 0; i < value.Count; i++)
            {
                _floatUIntMapBuffer.Float = value[i];
                uint u = _floatUIntMapBuffer.UInt;
                us[i] = (ushort)(HalfLookup.Base[(u >> 23) & 0x1ff] + ((u & 0x007fffff) >> HalfLookup.Shift[u >> 23]));
            }
            UShortArray(us);
        }

        // -- Float

        /// <summary>
        /// Writes a single float to the stream.
        /// </summary>
        public void Float(float value)
        {
            _floatUIntMapBuffer.Float = value;
            UInt(_floatUIntMapBuffer.UInt);
        }

        /// <summary>
        /// Writes an array of floats to the stream.
        /// </summary>
        public void FloatArray(float[] value)
        {
            byte[] buffer = new byte[value.Length * 4];
            if (stream.ByteOrder == Endianness.LittleEndian)
            {
                for (int i = 0; i < value.Length; i++)
                {
                    WriteToBufferLE(ref buffer, ref _floatByteMapBuffer, value[i], i * 4);
                }
            }
            else
            {
                for (int i = 0; i < value.Length; i++)
                {
                    WriteToBufferBE(ref buffer, ref _floatByteMapBuffer, value[i], i * 4);
                }
            }
            stream.baseStream.WriteByte(buffer);
        }

        /// <summary>
        /// Writes a list of floats to the stream.
        /// </summary>
        public void FloatList(List<float> value)
        {
            byte[] buffer = new byte[value.Count * 4];
            if (stream.ByteOrder == Endianness.LittleEndian)
            {
                for (int i = 0; i < value.Count; i++)
                {
                    WriteToBufferLE(ref buffer, ref _floatByteMapBuffer, value[i], i * 4);
                }
            }
            else
            {
                for (int i = 0; i < value.Count; i++)
                {
                    WriteToBufferBE(ref buffer, ref _floatByteMapBuffer, value[i], i * 4);
                }
            }
            stream.baseStream.WriteByte(buffer);
        }

        // -- Double

        /// <summary>
        /// Writes a single double to the stream.
        /// </summary>
        public void Double(double value)
        {
            _doubleULongMapBuffer.Double = value;
            ULong(_doubleULongMapBuffer.ULong);
        }

        /// <summary>
        /// Writes an array of doubles to the stream.
        /// </summary>
        public void DoubleArray(double[] value)
        {
            byte[] buffer = new byte[value.Length * 8];
            if (stream.ByteOrder == Endianness.LittleEndian)
            {
                for (int i = 0; i < value.Length; i++)
                {
                    WriteToBufferLE(ref buffer, ref _doubleByteMapBuffer, value[i], i * 8);
                }
            }
            else
            {
                for (int i = 0; i < value.Length; i++)
                {
                    WriteToBufferBE(ref buffer, ref _doubleByteMapBuffer, value[i], i * 8);
                }
            }
            stream.baseStream.WriteByte(buffer);
        }

        /// <summary>
        /// Writes a list of doubles to the stream.
        /// </summary>
        public void DoubleList(List<double> value)
        {
            byte[] buffer = new byte[value.Count * 8];
            if (stream.ByteOrder == Endianness.LittleEndian)
            {
                for (int i = 0; i < value.Count; i++)
                {
                    WriteToBufferLE(ref buffer, ref _doubleByteMapBuffer, value[i], i * 8);
                }
            }
            else
            {
                for (int i = 0; i < value.Count; i++)
                {
                    WriteToBufferBE(ref buffer, ref _doubleByteMapBuffer, value[i], i * 8);
                }
            }
            stream.baseStream.WriteByte(buffer);
        }

        // -- Char

        /// <summary>
        /// Writes a single char to the stream.
        /// </summary>
        public void Char(char character, TextEncoding encoding = TextEncoding.DEFAULT)
        {
            CharArray(new char[] { character }, encoding);
        }

        /// <summary>
        /// Writes an array of chars to the stream.
        /// </summary>
        public void CharArray(char[] characters, TextEncoding encoding = TextEncoding.DEFAULT)
        {
            if (encoding == TextEncoding.DEFAULT)
            {
                encoding = stream.DefaultTextEncoding;
            }
            SetTextEncoding(encoding);
            stream.baseStream.WriteByte(_textEncoder.GetBytes(characters));
        }

        // -- String

        /// <summary>
        /// Writes a string to the stream.
        /// </summary>
        public void String(string str, TextEncoding encoding = TextEncoding.DEFAULT)
        {
            if (encoding == TextEncoding.DEFAULT)
            {
                encoding = stream.DefaultTextEncoding;
            }
            SetTextEncoding(encoding);
            stream.baseStream.WriteByte(_textEncoder.GetBytes(str));
        }

        private void SetTextEncoding(TextEncoding encoding)
        {
            if (encoding == TextEncoding.UNICODE)
            {
                _textEncoder = Encoding.Unicode;
                _textCharacterWidth = 2;
            }
            else if (encoding == TextEncoding.UTF32)
            {
                _textEncoder = Encoding.UTF32;
                _textCharacterWidth = 4;
            }
            else if (encoding == TextEncoding.UTF8)
            {
                _textEncoder = Encoding.UTF8;
                _textCharacterWidth = 1;
            }
            else if (encoding == TextEncoding.UTF7)
            {
                _textEncoder = Encoding.UTF7;
                _textCharacterWidth = 1;
            }
            else
            {
                _textEncoder = Encoding.ASCII;
                _textCharacterWidth = 1;
            }
        }

    }
}
