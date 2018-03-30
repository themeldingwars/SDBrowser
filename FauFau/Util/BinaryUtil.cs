
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using static FauFau.Util.BinaryWrapper;

namespace FauFau.Util
{
    public static class BinaryUtil
    {

        public static void MTXor(uint seed, ref byte[] data)
        {
            MersenneTwister mt = new MersenneTwister(seed);
            uint l = (uint)data.Length;
            uint x = l >> 2;
            uint y = l & 3;
            byte[] xor = new byte[l];
            for (int i = 0; i < x; i++)
            {
                WriteToBufferLE(ref xor, mt.Next(), i * 4);
            }
            int z = (int)x * 4;
            for (uint i = 0; i < y; i++)
            {
                xor[z + i] = (byte)mt.Next();
            }
            for (int i = 0; i < l; i++)
            {
                data[i] ^= xor[i];
            }
            mt = null;
        }

        public static void MTXor(uint seed, BinaryStream source, BinaryStream destination, int start = -1, int length = -1)
        {
            MersenneTwister mt = new MersenneTwister(seed);
            if (start > 0) { source.ByteOffset = start; }
            uint l = (uint)(length > 0 ? length : (source.Length - source.ByteOffset));
            uint x = l >> 2;
            uint y = l & 3;

            byte[] data = source.Read.ByteArray((int)l);
            byte[] xor = new byte[l];

            for (int i = 0; i < x; i++)
            {
                WriteToBufferLE(ref xor, mt.Next(), i * 4);
            }
            int z = (int)x * 4;
            for (uint i = 0; i < y; i++)
            {
                xor[z + i] = (byte)mt.Next();
            }
            for (int i = 0; i < l; i++)
            {
                data[i] ^= xor[i];
            }
            destination.Write.ByteArray(data);
            mt = null;
        }

        public static void MTXorOldest(uint seed, BinaryStream source, BinaryStream destination, int start = -1, int length = -1)
        {
            MersenneTwister mt = new MersenneTwister(seed);
            if (start > 0) { source.ByteOffset = start; }
            uint l = (uint)(length > 0 ? length : (source.Length - source.ByteOffset));
            uint x = l >> 2;
            uint y = l & 3;

            for (uint i = 0; i < x; i++)
            {
                destination.Write.UInt(source.Read.UInt() ^ mt.Next());
            }
            for (uint i = 0; i < y; i++)
            {
                destination.Write.Byte((byte)(source.Read.Byte() ^ (byte)mt.Next()));
            }
            mt = null;
        }


        public static void Inflate(byte[] source, ref byte[] destination, int targetSize = -1, int sourceStart = -1, int length = -1, int destinationStart = 0)
        {
            MemoryStream ms = new MemoryStream(source);
            if (sourceStart > 0) { ms.Position = sourceStart; }
            DeflateStream ds = new DeflateStream(ms, CompressionMode.Decompress);
            ds.Read(destination, destinationStart, targetSize == -1 ? destination.Length : targetSize);
            ds.Dispose();
            ms.Dispose();
        }
        public static void Inflate(BinaryStream source, BinaryStream destination, int targetSize, int start = -1, int length = -1)
        {
            if (start > 0) { source.ByteOffset = start; }
            uint l = (uint)(length > 0 ? length : (source.Length - source.ByteOffset));

            using (MemoryStream payload = new MemoryStream(source.Read.ByteArray((int)l)))
            using (DeflateStream ds = new DeflateStream(payload, CompressionMode.Decompress))
            {
                byte[] tmp = new byte[targetSize];
                ds.Read(tmp, 0, targetSize);
                destination.Write.ByteArray(tmp);
            }
        }

        public static void InflateUnknownTargetSize(BinaryStream source, BinaryStream destination, int start = -1, int length = -1)
        {
            if (start > 0) { source.ByteOffset = start; }
            uint l = (uint)(length > 0 ? length : (source.Length - source.ByteOffset));

            using (MemoryStream payload = new MemoryStream(source.Read.ByteArray((int)l)))
            using (MemoryStream inflated = new MemoryStream())
            using (DeflateStream ds = new DeflateStream(payload, CompressionMode.Decompress))
            {
                ds.CopyTo(inflated);
                destination.Write.ByteArray(inflated.ToArray());
            }
        }
        public static void Deflate(BinaryStream source, BinaryStream destination, int start = -1, int length = -1)
        {
            if (start > 0) { source.ByteOffset = start; }
            uint l = (uint)(length > 0 ? length : (source.Length - source.ByteOffset));

            using (MemoryStream payload = new MemoryStream(source.Read.ByteArray((int)l)))
            using (MemoryStream deflated = new MemoryStream())
            using (DeflateStream ds = new DeflateStream(payload, CompressionMode.Compress))
            {
                ds.CopyTo(deflated);
                destination.Write.ByteArray(deflated.ToArray());
            }
        }


        public static ushort UShortFromBufferLE(ref byte[] buffer, int offset = 0)
        {
            return (ushort)(buffer[offset] | buffer[offset + 1] << 8);
        }
        public static ushort UShortFromBufferBE(ref byte[] buffer, int offset = 0)
        {
            return (ushort)(buffer[offset + 1] | buffer[offset] << 8);
        }
        public static uint UIntFromBufferLE(ref byte[] buffer, int offset = 0)
        {
            return (uint)(buffer[offset] | buffer[offset + 1] << 8 | buffer[offset + 2] << 16 | buffer[offset + 3] << 24);
        }
        public static uint UIntFromBufferBE(ref byte[] buffer, int offset = 0)
        {
            return (uint)(buffer[offset + 3] | buffer[offset + 2] << 8 | buffer[offset + 1] << 16 | buffer[offset] << 24);
        }
        public static ulong ULongFromBufferLE(ref byte[] buffer, int offset = 0)
        {
            uint low = (uint)(buffer[offset] | buffer[offset + 1] << 8 | buffer[offset + 2] << 16 | buffer[offset + 3] << 24);
            uint high = (uint)(buffer[offset + 4] | buffer[offset + 5] << 8 | buffer[offset + 6] << 16 | buffer[offset + 7] << 24);
            return ((ulong)high) << 32 | low;
        }
        public static ulong ULongFromBufferBE(ref byte[] buffer, int offset = 0)
        {
            uint low = (uint)(buffer[offset + 7] | buffer[offset + 6] << 8 | buffer[offset + 5] << 16 | buffer[offset + 4] << 24);
            uint high = (uint)(buffer[offset + 3] | buffer[offset + 2] << 8 | buffer[offset + 1] << 16 | buffer[offset] << 24);
            return ((ulong)high) << 32 | low;
        }
        public static float FloatFromBufferLE(ref byte[] buffer, ref FloatByteMap _floatByteMapBuffer, int offset = 0)
        {
            _floatByteMapBuffer.b0 = buffer[offset];
            _floatByteMapBuffer.b1 = buffer[offset + 1];
            _floatByteMapBuffer.b2 = buffer[offset + 2];
            _floatByteMapBuffer.b3 = buffer[offset + 3];
            return _floatByteMapBuffer.Float;
        }
        public static float FloatFromBufferBE(ref byte[] buffer, ref FloatByteMap _floatByteMapBuffer, int offset = 0)
        {
            _floatByteMapBuffer.b0 = buffer[offset + 3];
            _floatByteMapBuffer.b1 = buffer[offset + 2];
            _floatByteMapBuffer.b2 = buffer[offset + 1];
            _floatByteMapBuffer.b3 = buffer[offset];
            return _floatByteMapBuffer.Float;
        }
        public static double DoubleFromBufferLE(ref byte[] buffer, ref DoubleByteMap _doubleByteMapBuffer, int offset = 0)
        {
            _doubleByteMapBuffer.b0 = buffer[offset];
            _doubleByteMapBuffer.b1 = buffer[offset + 1];
            _doubleByteMapBuffer.b2 = buffer[offset + 2];
            _doubleByteMapBuffer.b3 = buffer[offset + 3];
            _doubleByteMapBuffer.b4 = buffer[offset + 4];
            _doubleByteMapBuffer.b5 = buffer[offset + 5];
            _doubleByteMapBuffer.b6 = buffer[offset + 6];
            _doubleByteMapBuffer.b7 = buffer[offset + 7];
            return _doubleByteMapBuffer.Double;
        }
        public static double DoubleFromBufferBE(ref byte[] buffer, ref DoubleByteMap _doubleByteMapBuffer, int offset = 0)
        {
            _doubleByteMapBuffer.b0 = buffer[offset + 7];
            _doubleByteMapBuffer.b1 = buffer[offset + 6];
            _doubleByteMapBuffer.b2 = buffer[offset + 5];
            _doubleByteMapBuffer.b3 = buffer[offset + 4];
            _doubleByteMapBuffer.b4 = buffer[offset + 3];
            _doubleByteMapBuffer.b5 = buffer[offset + 2];
            _doubleByteMapBuffer.b6 = buffer[offset + 1];
            _doubleByteMapBuffer.b7 = buffer[offset];
            return _doubleByteMapBuffer.Double;
        }

        public static void WriteToBufferLE(ref byte[] buffer, ushort value, int offset = 0)
        {
            buffer[offset] = (byte)(value);
            buffer[offset + 1] = (byte)(value >> 8);
        }
        public static void WriteToBufferBE(ref byte[] buffer, ushort value, int offset = 0)
        {
            buffer[offset + 1] = (byte)(value);
            buffer[offset] = (byte)(value >> 8);
        }
        public static void WriteToBufferLE(ref byte[] buffer, uint value, int offset = 0)
        {
            buffer[offset] = (byte)(value);
            buffer[offset + 1] = (byte)(value >> 8);
            buffer[offset + 2] = (byte)(value >> 16);
            buffer[offset + 3] = (byte)(value >> 24);
        }
        public static void WriteToBufferBE(ref byte[] buffer, uint value, int offset = 0)
        {
            buffer[offset + 3] = (byte)(value);
            buffer[offset + 2] = (byte)(value >> 8);
            buffer[offset + 1] = (byte)(value >> 16);
            buffer[offset] = (byte)(value >> 24);
        }
        public static void WriteToBufferLE(ref byte[] buffer, ref FloatByteMap _floatByteMapBuffer, float value, int offset = 0)
        {
            _floatByteMapBuffer.Float = value;
            buffer[offset] = _floatByteMapBuffer.b0;
            buffer[offset + 1] = _floatByteMapBuffer.b1;
            buffer[offset + 2] = _floatByteMapBuffer.b2;
            buffer[offset + 3] = _floatByteMapBuffer.b3;
        }
        public static void WriteToBufferBE(ref byte[] buffer, ref FloatByteMap _floatByteMapBuffer, float value, int offset = 0)
        {
            _floatByteMapBuffer.Float = value;
            buffer[offset + 3] = _floatByteMapBuffer.b0;
            buffer[offset + 2] = _floatByteMapBuffer.b1;
            buffer[offset + 1] = _floatByteMapBuffer.b2;
            buffer[offset] = _floatByteMapBuffer.b3;
        }
        public static void WriteToBufferLE(ref byte[] buffer, ref DoubleByteMap _doubleByteMapBuffer, double value, int offset = 0)
        {
            _doubleByteMapBuffer.Double = value;
            buffer[offset] = _doubleByteMapBuffer.b0;
            buffer[offset + 1] = _doubleByteMapBuffer.b1;
            buffer[offset + 2] = _doubleByteMapBuffer.b2;
            buffer[offset + 3] = _doubleByteMapBuffer.b3;
            buffer[offset + 4] = _doubleByteMapBuffer.b4;
            buffer[offset + 5] = _doubleByteMapBuffer.b5;
            buffer[offset + 6] = _doubleByteMapBuffer.b6;
            buffer[offset + 7] = _doubleByteMapBuffer.b7;
        }
        public static void WriteToBufferBE(ref byte[] buffer, ref DoubleByteMap _doubleByteMapBuffer, double value, int offset = 0)
        {
            buffer[offset + 7] = _doubleByteMapBuffer.b0;
            buffer[offset + 6] = _doubleByteMapBuffer.b1;
            buffer[offset + 5] = _doubleByteMapBuffer.b2;
            buffer[offset + 4] = _doubleByteMapBuffer.b3;
            buffer[offset + 3] = _doubleByteMapBuffer.b4;
            buffer[offset + 2] = _doubleByteMapBuffer.b5;
            buffer[offset + 1] = _doubleByteMapBuffer.b6;
            buffer[offset + 0] = _doubleByteMapBuffer.b7;
        }
        public static void WriteToBufferLE(ref byte[] buffer, ulong value, int offset = 0)
        {
            buffer[offset] = (byte)value;
            buffer[offset + 1] = (byte)(value >> 8);
            buffer[offset + 2] = (byte)(value >> 16);
            buffer[offset + 3] = (byte)(value >> 24);
            buffer[offset + 4] = (byte)(value >> 32);
            buffer[offset + 5] = (byte)(value >> 40);
            buffer[offset + 6] = (byte)(value >> 48);
            buffer[offset + 7] = (byte)(value >> 56);
        }
        public static void WriteToBufferBE(ref byte[] buffer, ulong value, int offset = 0)
        {
            buffer[offset + 7] = (byte)value;
            buffer[offset + 6] = (byte)(value >> 8);
            buffer[offset + 5] = (byte)(value >> 16);
            buffer[offset + 4] = (byte)(value >> 24);
            buffer[offset + 3] = (byte)(value >> 32);
            buffer[offset + 2] = (byte)(value >> 40);
            buffer[offset + 1] = (byte)(value >> 48);
            buffer[offset] = (byte)(value >> 56);
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct FloatByteMap
        {
            [FieldOffset(0)] public float Float;
            [FieldOffset(0)] public byte b0;
            [FieldOffset(1)] public byte b1;
            [FieldOffset(2)] public byte b2;
            [FieldOffset(3)] public byte b3;
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct FloatUIntMap
        {
            [FieldOffset(0)] public float Float;
            [FieldOffset(0)] public uint UInt;
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct DoubleByteMap
        {
            [FieldOffset(0)] public double Double;
            [FieldOffset(0)] public byte b0;
            [FieldOffset(1)] public byte b1;
            [FieldOffset(2)] public byte b2;
            [FieldOffset(3)] public byte b3;
            [FieldOffset(3)] public byte b4;
            [FieldOffset(3)] public byte b5;
            [FieldOffset(3)] public byte b6;
            [FieldOffset(3)] public byte b7;
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct DoubleULongMap
        {
            [FieldOffset(0)] public double Double;
            [FieldOffset(0)] public ulong ULong;
        }
    }
}
