using System;
using System.Collections.Generic;
using System.Text;

namespace FauFau.Util
{
    static class HalfLookup
    {
        public static uint[] Mantissa = new uint[2048];
        public static uint[] Exponent = new uint[64];
        public static ushort[] Offset = new ushort[64];
        public static ushort[] Base = new ushort[512];
        public static sbyte[] Shift = new sbyte[512];
        static HalfLookup()
        {
            // mantissa
            uint _fraction = 0x800000;
            uint _fractionComplement = 0xFF7FFFFF;
            uint _bias = 0x38800000;
            Mantissa[0] = 0;
            Mantissa[1024] = 0x38000000;
            for (uint i = 1; i < 1024; i++)
            {
                uint mantissa = i << 13;

                Mantissa[i + 1024] = (0x38000000 + mantissa);

                uint exponent = 0;
                while ((mantissa & _fraction) == 0)
                {
                    mantissa <<= 1;
                    exponent -= _fraction;
                }
                mantissa &= _fractionComplement;
                exponent += _bias;

                Mantissa[i] = mantissa | exponent;
            }

            // exponents
            Exponent[0] = 0;
            Exponent[31] = 0x47800000;
            Exponent[32] = 0x80000000;
            Exponent[63] = 0xc7800000;
            for (uint i = 1; i < 31; i++)
            {
                uint x = i << 23;
                Exponent[i] = x;
                Exponent[i + 32] = (0x80000000 + x);
            }

            // offsets
            Offset[0] = 0;
            Offset[32] = 0;
            for (uint i = 1; i < 32; i++)
            {
                Offset[i] = 1024;
                Offset[i + 32] = 1024;
            }

            // base and shift
            for (uint i = 0; i < 256; ++i)
            {
                sbyte e = (sbyte)(127 - i);
                if (e > 24)
                {
                    Base[i | 0x000] = 0x0;
                    Base[i | 0x100] = 0x8000;
                    Shift[i | 0x000] = 24;
                    Shift[i | 0x100] = 24;
                }
                else if (e > 14)
                {
                    Base[i | 0x000] = (ushort)(0x0400 >> (18 + e));
                    Base[i | 0x100] = (ushort)((0x0400 >> (18 + e)) | 0x8000);
                    Shift[i | 0x000] = (sbyte)(e - 1);
                    Shift[i | 0x100] = (sbyte)(e - 1);
                }
                else if (e > -16)
                {
                    Base[i | 0x000] = (ushort)((15 - e) << 10);
                    Base[i | 0x100] = (ushort)(((15 - e) << 10) | 0x8000);
                    Shift[i | 0x000] = 13;
                    Shift[i | 0x100] = 13;
                }
                else if (e > -128)
                {
                    Base[i | 0x000] = 0x7C00;
                    Base[i | 0x100] = 0xFC00;
                    Shift[i | 0x000] = 24;
                    Shift[i | 0x100] = 24;
                }
                else
                {
                    Base[i | 0x000] = 0x7C00;
                    Base[i | 0x100] = 0xFC00;
                    Shift[i | 0x000] = 13;
                    Shift[i | 0x100] = 13;
                }
            }
        }
    }
}
