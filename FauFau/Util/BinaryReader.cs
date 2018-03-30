using System.Collections.Generic;
using System.Text;
using static FauFau.Util.BinaryStream;
using static FauFau.Util.BinaryUtil;
using static FauFau.Util.BinaryWrapper;

// TODO: Make array/list readers read entire array as a blob instead of each element. (speed)
namespace FauFau.Util
{
    public class BinaryReader
    {
        private BinaryStream stream;
        private Endianness byteOrder;

        private byte[] _twoByteBuffer = new byte[2];
        private byte[] _fourByteBuffer = new byte[4];
        private byte[] _eightByteBuffer = new byte[8];
        private FloatByteMap _floatByteMapBuffer = new FloatByteMap();
        private FloatUIntMap _floatUIntMapBuffer = new FloatUIntMap();
        private DoubleByteMap _doubleByteMapBuffer = new DoubleByteMap();
        private DoubleULongMap _doubleULongMapBuffer = new DoubleULongMap();
        private Encoding _textEncoder;
        private int _textCharacterWidth;

        public BinaryReader(BinaryStream stream)
        {
            this.stream = stream;
        }


        // -- Bit

        /// <summary>
        /// Reads a single bit from the stream.
        /// </summary>
        public byte Bit()
        {
            return stream.baseStream.ReadBit();
        }

        /// <summary>
        /// Reads an array of bits from the stream.
        /// </summary>
        public byte[] BitArray(int length)
        {
            return stream.baseStream.ReadBit(length);
        }

        /// <summary>
        /// Reads a list of bits from the stream.
        /// </summary>
        public List<byte> BitList(int length)
        {
            byte[] bits = stream.baseStream.ReadBit(length);
            List<byte> ret = new List<byte>(length);
            for (int i = 0; i < length; i++)
            {
                ret.Add(bits[i]);
            }
            return ret;
        }

        // -- Signed Byte

        /// <summary>
        /// Reads a single signed byte from the stream.
        /// </summary>
        public sbyte SByte()
        {
            return (sbyte)stream.baseStream.ReadByte();
        }

        /// <summary>
        /// Reads an array of signed bytes from the stream.
        /// </summary>
        public sbyte[] SByteArray(int length)
        {
            byte[] bytes = stream.baseStream.ReadByte(length);
            sbyte[] ret = new sbyte[length];
            for (int i = 0; i < length; i++)
            {
                ret[i] = (sbyte)bytes[i];
            }
            return ret;
        }

        /// <summary>
        /// Reads a list of signed bytes from the stream.
        /// </summary>
        public List<sbyte> SByteList(int length)
        {
            byte[] bytes = stream.baseStream.ReadByte(length);
            List<sbyte> ret = new List<sbyte>(length);
            for (int i = 0; i < length; i++)
            {
                ret.Add((sbyte)bytes[i]);
            }
            return ret;
        }

        /// <summary>
        /// Reads a single signed byte of variable bit length from the stream.
        /// </summary>
        public sbyte SByteFromBits(int numBits)
        {
            return (sbyte)stream.baseStream.ReadByteFromBits(numBits);
        }

        /// <summary>
        /// Reads an array of signed bytes of variable bit length from the stream.
        /// </summary>
        public sbyte[] SByteArrayFromBits(int count, int numBits)
        {
            sbyte[] ret = new sbyte[count];
            for (int i = 0; i < count; i++)
            {
                ret[i] = (sbyte)stream.baseStream.ReadByteFromBits(numBits);
            }
            return ret;
        }

        /// <summary>
        /// Reads a list of signed bytes of variable bit length from the stream.
        /// </summary>
        public List<sbyte> SByteListFromBits(int count, int numBits)
        {
            List<sbyte> ret = new List<sbyte>(count);
            for (int i = 0; i < count; i++)
            {
                ret.Add((sbyte)stream.baseStream.ReadByteFromBits(numBits));
            }
            return ret;
        }

        // -- Short

        /// <summary>
        /// Reads a single short from the stream.
        /// </summary>
        public short Short()
        {
            return (short)UShort();
        }

        /// <summary>
        /// Reads an array of shorts from the stream.
        /// </summary>
        public short[] ShortArray(int count)
        {
            short[] ret = new short[count];
            for (int i = 0; i < count; i++)
            {
                ret[i] = (short)UShort();
            }
            return ret;
        }

        /// <summary>
        /// Reads a list of shorts from the stream.
        /// </summary>
        public List<short> ShortList(int count)
        {
            List<short> ret = new List<short>(count);
            for (int i = 0; i < count; i++)
            {
                ret.Add((short)UShort());
            }
            return ret;
        }

        /// <summary>
        /// Reads a short of variable bit length from the stream.
        /// </summary>
        public short ShortFromBits(int numBits)
        {
            short val = (short)UShortFromBits(numBits);
            int move = 16 - numBits;
            val <<= move;
            val >>= move;
            return val;
        }

        /// <summary>
        /// Reads an array of shorts of variable bit length from the stream.
        /// </summary>
        public short[] ShortArrayFromBits(int count, int numBits)
        {
            short[] ret = new short[count];
            for (int i = 0; i < count; i++)
            {
                ret[i] = ShortFromBits(numBits);
            }
            return ret;
        }

        /// <summary>
        /// Reads a list of shorts of variable bit length from the stream.
        /// </summary>
        public List<short> ShortListFromBits(int count, int numBits)
        {
            List<short> ret = new List<short>(count);
            for (int i = 0; i < count; i++)
            {
                ret.Add(ShortFromBits(numBits));
            }
            return ret;
        }

        // -- Int

        /// <summary>
        /// Reads a single int from the stream.
        /// </summary>
        public int Int()
        {
            return (int)UInt();
        }

        /// <summary>
        /// Reads an array of ints from the stream.
        /// </summary>
        public int[] IntArray(int count)
        {
            int[] ret = new int[count];
            for (int i = 0; i < count; i++)
            {
                ret[i] = (int)UInt();
            }
            return ret;
        }

        /// <summary>
        /// Reads a list of ints from the stream.
        /// </summary>
        public List<int> IntList(int count)
        {
            List<int> ret = new List<int>(count);
            for (int i = 0; i < count; i++)
            {
                ret.Add((int)UInt());
            }
            return ret;
        }

        /// <summary>
        /// Reads an int of variable bit length from the stream.
        /// </summary>
        public int IntFromBits(int numBits)
        {
            int val = (int)UIntFromBits(numBits);
            int move = 32 - numBits;
            val <<= move;
            val >>= move;
            return (int)val;
        }

        /// <summary>
        /// Reads an array of ints of variable bit length from the stream.
        /// </summary>
        public int[] IntArrayFromBits(int count, int numBits)
        {
            int[] ret = new int[count];
            for (int i = 0; i < count; i++)
            {
                ret[i] = IntFromBits(numBits);
            }
            return ret;
        }

        /// <summary>
        /// Reads a list of ints of variable bit length from the stream.
        /// </summary>
        public List<int> IntListFromBits(int count, int numBits)
        {
            List<int> ret = new List<int>(count);
            for (int i = 0; i < count; i++)
            {
                ret.Add(IntFromBits(numBits));
            }
            return ret;
        }

        // -- Long

        /// <summary>
        /// Reads a single long from the stream.
        /// </summary>
        public long Long()
        {
            return (long)ULong();
        }

        /// <summary>
        /// Reads an array of longs from the stream.
        /// </summary>
        public long[] LongArray(int count)
        {
            long[] ret = new long[count];
            for (int i = 0; i < count; i++)
            {
                ret[i] = (long)ULong();
            }
            return ret;
        }

        /// <summary>
        /// Reads a list of longs from the stream.
        /// </summary>
        public List<long> LongList(int count)
        {
            List<long> ret = new List<long>(count);
            for (int i = 0; i < count; i++)
            {
                ret.Add((long)ULong());
            }
            return ret;
        }

        /// <summary>
        /// Reads a long of variable bit length from the stream.
        /// </summary>
        public long LongFromBits(int numBits)
        {
            if (numBits > 32)
            {
                uint low = UInt();
                int high = (int)UIntFromBits(numBits - 32);

                int move = 64 - numBits;
                high <<= move;
                high >>= move;

                return (long)(((ulong)high) << 32 | low);
            }
            else
            {
                int val = (int)UIntFromBits(numBits);
                int move = 32 - numBits;
                val <<= move;
                val >>= move;
                return (long)val;
            }
        }

        /// <summary>
        /// Reads an array of longs of variable bit length from the stream.
        /// </summary>
        public long[] LongArrayFromBits(int count, int numBits)
        {
            long[] ret = new long[count];
            for (int i = 0; i < count; i++)
            {
                ret[i] = LongFromBits(numBits);
            }
            return ret;
        }

        /// <summary>
        /// Reads a list of longs of variable bit length from the stream.
        /// </summary>
        public List<long> LongListFromBits(int count, int numBits)
        {
            List<long> ret = new List<long>(count);
            for (int i = 0; i < count; i++)
            {
                ret.Add(LongFromBits(numBits));
            }
            return ret;
        }

        // -- Byte

        /// <summary>
        /// Reads a single byte from the stream.
        /// </summary>
        public byte Byte()
        {
            return stream.baseStream.ReadByte();
        }

        /// <summary>
        /// Reads an array of bytes from the stream.
        /// </summary>
        public byte[] ByteArray(int length)
        {
            return stream.baseStream.ReadByte(length);
        }

        /// <summary>
        /// Reads a list of bytes from the stream.
        /// </summary>
        public List<byte> ByteList(int length)
        {
            byte[] bytes = stream.baseStream.ReadByte(length);
            List<byte> ret = new List<byte>(length);
            for (int i = 0; i < length; i++)
            {
                ret.Add(bytes[i]);
            }
            return ret;
        }

        /// <summary>
        /// Reads a byte of variable bit length from the stream.
        /// </summary>
        public byte ByteFromBits(int numBits)
        {
            return stream.baseStream.ReadByteFromBits(numBits);
        }

        /// <summary>
        /// Reads an array of bytes of variable bit length from the stream.
        /// </summary>
        public byte[] ByteArrayFromBits(int count, int numBits)
        {
            byte[] ret = new byte[count];
            for (int i = 0; i < count; i++)
            {
                ret[i] = stream.baseStream.ReadByteFromBits(numBits);
            }
            return ret;
        }

        /// <summary>
        /// Reads a list of bytes of variable bit length from the stream.
        /// </summary>
        public List<byte> ByteListFromBits(int count, int numBits)
        {
            List<byte> ret = new List<byte>(count);
            for (int i = 0; i < count; i++)
            {
                ret.Add(stream.baseStream.ReadByteFromBits(numBits));
            }
            return ret;
        }

        // -- UShort

        /// <summary>
        /// Reads a single ushort from the stream.
        /// </summary>
        public ushort UShort()
        {
            _twoByteBuffer = stream.baseStream.ReadByte(2);
            if (stream.ByteOrder == Endianness.LittleEndian)
            {
                return UShortFromBufferLE(ref _twoByteBuffer);
            }
            else
            {
                return UShortFromBufferBE(ref _twoByteBuffer);
            }
        }

        /// <summary>
        /// Reads an array of ushorts from the stream.
        /// </summary>
        public ushort[] UShortArray(int count)
        {
            ushort[] ret = new ushort[count];
            for (int i = 0; i < count; i++)
            {
                ret[i] = UShort();
            }
            return ret;
        }

        /// <summary>
        /// Reads a list of ushorts from the stream.
        /// </summary>
        public List<ushort> UShortList(int count)
        {
            List<ushort> ret = new List<ushort>(count);
            for (int i = 0; i < count; i++)
            {
                ret.Add(UShort());
            }
            return ret;
        }

        /// <summary>
        /// Reads an ushort of variable bit length from the stream.
        /// </summary>
        public ushort UShortFromBits(int numBits)
        {
            byte[] bytes = stream.baseStream.ReadBytesFromBits(numBits);
            if (bytes.Length == 2)
            {
                return (ushort)(bytes[0] | bytes[1] << 8);
            }
            else
            {
                return bytes[0];
            }
        }

        /// <summary>
        /// Reads an array of ushorts of variable bit length from the stream.
        /// </summary>
        public ushort[] UShortArrayFromBits(int count, int numBits)
        {
            ushort[] ret = new ushort[count];
            for (int i = 0; i < count; i++)
            {
                ret[i] = UShortFromBits(numBits);
            }
            return ret;
        }

        /// <summary>
        /// Reads a list of ushorts of variable bit length from the stream.
        /// </summary>
        public List<ushort> UShortListFromBits(int count, int numBits)
        {
            List<ushort> ret = new List<ushort>(count);
            for (int i = 0; i < count; i++)
            {
                ret.Add(UShortFromBits(numBits));
            }
            return ret;
        }

        // -- UInt

        /// <summary>
        /// Reads a single uint from the stream.
        /// </summary>
        public uint UInt()
        {
            _fourByteBuffer = stream.baseStream.ReadByte(4);
            if (stream.ByteOrder == Endianness.LittleEndian)
            {
                return UIntFromBufferLE(ref _fourByteBuffer);
            }
            else
            {
                return UIntFromBufferBE(ref _fourByteBuffer);
            }
        }

        /// <summary>
        /// Reads an array of uints from the stream.
        /// </summary>
        public uint[] UIntArray(int count)
        {
            uint[] ret = new uint[count];
            for (int i = 0; i < count; i++)
            {
                ret[i] = UInt();
            }
            return ret;
        }

        /// <summary>
        /// Reads a list of uints from the stream.
        /// </summary>
        public List<uint> UIntList(int count)
        {
            List<uint> ret = new List<uint>(count);
            for (int i = 0; i < count; i++)
            {
                ret.Add(UInt());
            }
            return ret;
        }

        /// <summary>
        /// Reads an uint of variable bit length from the stream.
        /// </summary>
        public uint UIntFromBits(int numBits)
        {
            byte[] bytes = stream.baseStream.ReadBytesFromBits(numBits);
            int length = bytes.Length;
            if (length == 4)
            {
                return (uint)(bytes[0] | bytes[1] << 8 | bytes[2] << 16 | bytes[3] << 24);
            }
            else if (length == 3)
            {
                return (uint)(bytes[0] | bytes[1] << 8 | bytes[2] << 16);
            }
            else if (length == 2)
            {
                return (uint)(bytes[0] | bytes[1] << 8);
            }
            else
            {
                return bytes[0];
            }
        }

        /// <summary>
        /// Reads an array of uints of variable bit length from the stream.
        /// </summary>
        public uint[] UIntArrayFromBits(int count, int numBits)
        {
            uint[] ret = new uint[count];
            for (int i = 0; i < count; i++)
            {
                ret[i] = UIntFromBits(numBits);
            }
            return ret;
        }

        /// <summary>
        /// Reads a list of uints of variable bit length from the stream.
        /// </summary>
        public List<uint> UIntListFromBits(int count, int numBits)
        {
            List<uint> ret = new List<uint>(count);
            for (int i = 0; i < count; i++)
            {
                ret.Add(UIntFromBits(numBits));
            }
            return ret;
        }

        // -- ULong

        /// <summary>
        /// Reads a single ulong from the stream.
        /// </summary>
        public ulong ULong()
        {
            _eightByteBuffer = stream.baseStream.ReadByte(8);
            if (stream.ByteOrder == Endianness.LittleEndian)
            {
                return ULongFromBufferLE(ref _eightByteBuffer);
            }
            else
            {
                return ULongFromBufferBE(ref _eightByteBuffer);
            }
        }

        /// <summary>
        /// Reads an array of ulongs from the stream.
        /// </summary>
        public ulong[] ULongArray(int count)
        {
            ulong[] ret = new ulong[count];
            for (int i = 0; i < count; i++)
            {
                ret[i] = ULong();
            }
            return ret;
        }

        /// <summary>
        /// Reads a list of ulongs from the stream.
        /// </summary>
        public List<ulong> ULongList(int count)
        {
            List<ulong> ret = new List<ulong>(count);
            for (int i = 0; i < count; i++)
            {
                ret.Add(ULong());
            }
            return ret;
        }

        /// <summary>
        /// Reads an ulong of variable bit length from the stream.
        /// </summary>
        public ulong ULongFromBits(int numBits)
        {
            if (numBits > 32)
            {
                uint low = UInt();
                uint high = UIntFromBits(numBits - 32);
                return ((ulong)high) << 32 | low;
            }
            else
            {
                return UIntFromBits(numBits);
            }
        }

        /// <summary>
        /// Reads an array of ulongs of variable bit length from the stream.
        /// </summary>
        public ulong[] ULongArrayFromBits(int count, int numBits)
        {
            ulong[] ret = new ulong[count];
            for (int i = 0; i < count; i++)
            {
                ret[i] = ULongFromBits(numBits);
            }
            return ret;
        }

        /// <summary>
        /// Reads a list of ulongs of variable bit length from the stream.
        /// </summary>
        public List<ulong> ULongListFromBits(int count, int numBits)
        {
            List<ulong> ret = new List<ulong>(count);
            for (int i = 0; i < count; i++)
            {
                ret.Add(ULongFromBits(numBits));
            }
            return ret;
        }

        // -- Half

        /// <summary>
        /// Reads a single half from the stream.
        /// </summary>
        public float Half()
        {
            ushort u = UShort(); // already read in correct endianess
            _floatUIntMapBuffer.UInt = HalfLookup.Mantissa[HalfLookup.Offset[u >> 10] + (u & 0x3ff)] + HalfLookup.Exponent[u >> 10];
            return _floatUIntMapBuffer.Float;
        }

        /// <summary>
        /// Reads an array of halfs from the stream.
        /// </summary>
        public float[] HalfArray(int count)
        {
            float[] ret = new float[count];
            for (int i = 0; i < count; i++)
            {
                ret[i] = Half();
            }
            return ret;
        }

        /// <summary>
        /// Reads a list of halfs from the stream.
        /// </summary>
        public List<float> HalfList(int count)
        {
            List<float> ret = new List<float>(count);
            for (int i = 0; i < count; i++)
            {
                ret.Add(Half());
            }
            return ret;
        }

        // -- Float

        /// <summary>
        /// Reads a single float from the stream.
        /// </summary>
        public float Float()
        {
            _floatUIntMapBuffer.UInt = UInt(); // already read in the correct endianess
            return _floatUIntMapBuffer.Float;
        }

        /// <summary>
        /// Reads an array of floats from the stream.
        /// </summary>
        public float[] FloatArray(int count)
        {
            float[] ret = new float[count];
            for (int i = 0; i < count; i++)
            {
                ret[i] = Float();
            }
            return ret;
        }

        /// <summary>
        /// Reads a list of floats from the stream.
        /// </summary>
        public List<float> FloatList(int count)
        {
            List<float> ret = new List<float>(count);
            for (int i = 0; i < count; i++)
            {
                ret.Add(Float());
            }
            return ret;
        }

        // -- Double

        /// <summary>
        /// Reads a single double from the stream.
        /// </summary>
        public double Double()
        {
            _doubleULongMapBuffer.ULong = ULong(); // already read in the correct endianess
            return _doubleULongMapBuffer.Double;
        }

        /// <summary>
        /// Reads an array of doubles from the stream.
        /// </summary>
        public double[] DoubleArray(int count)
        {
            double[] ret = new double[count];
            for (int i = 0; i < count; i++)
            {
                ret[i] = Double();
            }
            return ret;
        }

        /// <summary>
        /// Reads a list of doubles from the stream.
        /// </summary>
        public List<double> DoubleList(int count)
        {
            List<double> ret = new List<double>(count);
            for (int i = 0; i < count; i++)
            {
                ret.Add(Double());
            }
            return ret;
        }

        // -- Char

        /// <summary>
        /// Reads a single char from the stream.
        /// </summary>
        public char Char(TextEncoding encoding = TextEncoding.DEFAULT)
        {
            if (encoding == TextEncoding.DEFAULT)
            {
                encoding = stream.DefaultTextEncoding;
            }
            SetTextEncoding(encoding);
            return _textEncoder.GetChars(ByteArray(_textCharacterWidth))[0];
        }

        /// <summary>
        /// Reads an array of chars from the stream.
        /// </summary>
        public char[] CharArray(int length, TextEncoding encoding = TextEncoding.DEFAULT)
        {
            if (encoding == TextEncoding.DEFAULT)
            {
                encoding = stream.DefaultTextEncoding;
            }
            SetTextEncoding(encoding);
            return _textEncoder.GetChars(ByteArray(_textCharacterWidth * length));
        }

        // -- String

        /// <summary>
        /// Reads a single char as a string from the stream.
        /// </summary>
        public string String(TextEncoding encoding = TextEncoding.DEFAULT)
        {
            if (encoding == TextEncoding.DEFAULT)
            {
                encoding = stream.DefaultTextEncoding;
            }
            SetTextEncoding(encoding);
            return _textEncoder.GetString(ByteArray(_textCharacterWidth));
        }

        /// <summary>
        /// Reads a string from the stream.
        /// </summary>
        public string String(int length, TextEncoding encoding = TextEncoding.DEFAULT)
        {
            if (encoding == TextEncoding.DEFAULT)
            {
                encoding = stream.DefaultTextEncoding;
            }
            SetTextEncoding(encoding);
            return _textEncoder.GetString(ByteArray(_textCharacterWidth * length));
        }


        /// <summary>
        /// Reads a ReadWrite type object from the stream.
        /// </summary>
        public T Type<T>() where T : ReadWrite, new()
        {
            T ret = new T();
            ret.Read(stream);
            return ret;
        }

        /// <summary>
        /// Reads a list of ReadWrite type objects from the stream.
        /// </summary>
        public List<T> TypeList<T>(int count) where T : ReadWrite, new()
        {
            List<T> ret = new List<T>(count);
            for (int i = 0; i < count; i++)
            {
                T entry = new T();
                entry.Read(stream);
                ret.Add(entry);
            }
            return ret;
        }

        /// <summary>
        /// Reads a list of ReadWrite type objects from the stream.
        /// </summary>
        public List<T> TypeList<T>(uint count) where T : ReadWrite, new()
        {
            return TypeList<T>((int)count);
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
