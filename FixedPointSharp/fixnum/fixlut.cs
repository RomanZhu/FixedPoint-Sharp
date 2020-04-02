using System;
using System.Collections.Generic;

namespace FixedPoint {
    public static partial class fixlut {
        public const int  FRACTIONS_COUNT = 5;
        public const int  PRECISION       = 16;
        public const int  SHIFT           = 16 - 9;
        public const long PI              = 205887L;
        public const long ONE             = 1 << PRECISION;
        public const long ZERO            = 0;

        public const int SIN_VALUE_COUNT     = 512;
        public const int SIN_COS_VALUE_COUNT = 512;
        public const int TAN_VALUE_COUNT     = 512;
        public const int ACOS_VALUE_COUNT    = 512;
        public const int ASIN_VALUE_COUNT    = 512;

        public static readonly List<int> SinLut    = new List<int>(SIN_VALUE_COUNT + 1);
        public static readonly List<int> SinCosLut = new List<int>(SIN_COS_VALUE_COUNT * 2 + 2);
        public static readonly List<int> TanLut    = new List<int>(TAN_VALUE_COUNT + 1);
        public static readonly List<int> AcosLut   = new List<int>(ACOS_VALUE_COUNT + 2);
        public static readonly List<int> AsinLut   = new List<int>(ASIN_VALUE_COUNT + 2);
        
                
        public static readonly List<uint> ufp16p16sqrtlut_1 = new List<uint>();
        public static readonly List<uint> ufp16p16sqrtlut_16 = new List<uint>();
        public static readonly List<uint> ufp16p16sqrtlut_256 = new List<uint>();
        public static readonly List<uint> ufp16p16sqrtlut_64k = new List<uint>();

        static fixlut() {
            for (var i=0; i<=128; i++) {
                var d = Math.Sqrt(i/512.0);
                ufp16p16sqrtlut_1.Add((uint) (d * ONE + 0.5f));
            }
            for (var i=0; i<=128; i++) {
                var d = Math.Sqrt(i/8.0);
                ufp16p16sqrtlut_16.Add((uint) (d * ONE + 0.5f));
            }

            for (var i=0; i<=120; i++) {
                var d = Math.Sqrt(16.0 + i*2.0);
                ufp16p16sqrtlut_256.Add((uint) (d * ONE + 0.5f));
            }

            for (var i=0; i<=128; i++) {
                var d = Math.Sqrt(256.0 + i*2.0*256.0);
                ufp16p16sqrtlut_64k.Add((uint) (d * ONE + 0.5f));
            }

            for (var i = 0; i < SIN_COS_VALUE_COUNT; i++) {
                var angle = 2 * Math.PI * i / SIN_COS_VALUE_COUNT;

                var sinValue   = Math.Sin(angle);
                var movedSin   = sinValue * ONE;
                var roundedSin = movedSin > 0 ? movedSin + 0.5f : movedSin - 0.5f;
                SinLut.Add((int) roundedSin);
            }

            SinLut.Add(SinLut[0]);

            for (var i = 0; i < SIN_COS_VALUE_COUNT; i++) {
                var angle = 2 * Math.PI * i / SIN_COS_VALUE_COUNT;

                var sinValue   = Math.Sin(angle);
                var movedSin   = sinValue * ONE;
                var roundedSin = movedSin > 0 ? movedSin + 0.5f : movedSin - 0.5f;
                SinCosLut.Add((int) roundedSin);

                var cosValue   = Math.Cos(angle);
                var movedCos   = cosValue * ONE;
                var roundedCos = movedCos > 0 ? movedCos + 0.5f : movedCos - 0.5f;
                SinCosLut.Add((int) roundedCos);
            }

            SinCosLut.Add(SinCosLut[0]);
            SinCosLut.Add(SinCosLut[1]);

            for (var i = 0; i < TAN_VALUE_COUNT; i++) {
                var angle = 2 * Math.PI * i / TAN_VALUE_COUNT;

                var value   = Math.Tan(angle);
                var moved   = value * ONE;
                var rounded = moved > 0 ? moved + 0.5f : moved - 0.5f;
                TanLut.Add((int) rounded);
            }

            TanLut.Add(TanLut[0]);

            for (var i = 0; i < ASIN_VALUE_COUNT; i++) {
                var angle = 2f * i / ASIN_VALUE_COUNT;
                angle -= 1;

                if (i == ASIN_VALUE_COUNT - 1)
                    angle = 1;

                var value   = Math.Asin(angle);
                var moved   = value * ONE;
                var rounded = moved > 0 ? moved + 0.5f : moved - 0.5f;
                AsinLut.Add((int) rounded);
            }

            //Special handling for value of 1, as graph is not symmetric
            AsinLut.Add(AsinLut[ASIN_VALUE_COUNT - 1]);
            AsinLut.Add(AsinLut[ASIN_VALUE_COUNT - 1]);

            for (var i = 0; i < ACOS_VALUE_COUNT; i++) {
                var angle = 2f * i / ACOS_VALUE_COUNT;
                angle -= 1;

                if (i == ACOS_VALUE_COUNT - 1)
                    angle = 1;

                var value   = Math.Acos(angle);
                var moved   = value * ONE;
                var rounded = moved > 0 ? moved + 0.5f : moved - 0.5f;
                AcosLut.Add((int) rounded);
            }

            //Special handling for value of 1, as graph is not symmetric
            AcosLut.Add(AcosLut[ACOS_VALUE_COUNT - 1]);
            AcosLut.Add(AcosLut[ACOS_VALUE_COUNT - 1]);
        }

        public static long _sqrtufp16p16lut(long x) {
            return __genericufp16p16lut(ufp16p16sqrtlut_1, ufp16p16sqrtlut_16, ufp16p16sqrtlut_256, ufp16p16sqrtlut_64k, x);
        }

        private static long __genericufp16p16lut(List<uint> lut1, List<uint> lut16, List<uint> lut256, List<uint> lut64k, long x) {
            uint val1;
            uint val2;
            long fraction;

            if (x < 16384) {
                var index = (int) x >> 7;
                fraction = (x<<7) & 0x0ffff;
                val1     = lut1[index];
                val2     = lut1[index + 1];
            }else if (x < 16 << 16) {
                var index = (int) (x >> 13);
                fraction = (x << 3) & 0x0ffff;
                val1     = lut16[index];
                val2     = lut16[index + 1];
            }
            else if (x < 256 << 16) {
                var index = (int) (((x >> 16) - 16) >> 1);
                fraction = (x >> 1) & 0x0ffff;
                val1     = lut256[index];
                val2     = lut256[index + 1];
            }
            else {
                var index = (int) (((x >> 16) - 256) >> 9);
                fraction = ((x - (256 << 16)) >> 9) & 0x0ffff;
                val1     = lut64k[index];
                val2     = lut64k[index + 1];
            }

            return val1 + (fraction * (val2 - val1) >> PRECISION);
        }

        public static long sin(long value) {
            if (value < 0) {
                value = -value;
            }

            var index    = (int) (value >> SHIFT);
            var fraction = (value - (index << SHIFT)) << 9;
            var a        = SinLut[index];
            var b        = SinLut[index + 1];
            var v2       = a + (((b - a) * fraction) >> PRECISION);
            return v2;
        }

        public static long cos(long value) {
            if (value < 0) {
                value = -value;
            }

            value += fp._0_25.value;

            var index    = (int) (value >> SHIFT);
            var fraction = (value - (index << SHIFT)) << 9;
            var a        = SinLut[index];
            var b        = SinLut[index + 1];
            var v2       = a + (((b - a) * fraction) >> PRECISION);
            return v2;
        }


        public static long tan(long value) {
            var sign = 1;

            if (value < 0) {
                value = -value;
                sign  = -1;
            }

            var index    = (int) (value >> SHIFT);
            var fraction = (value - (index << SHIFT)) << 9;
            var a        = TanLut[index];
            var b        = TanLut[index + 1];
            var v2       = a + (((b - a) * fraction) >> PRECISION);
            return v2 * sign;
        }

        public static void sin_cos(long value, out long sin, out long cos) {
            if (value < 0) {
                value = -value;
            }

            var index       = (int) (value >> SHIFT);
            var doubleIndex = index * 2;
            var fractions   = (value - (index << SHIFT)) << 9;

            var sinA = SinCosLut[doubleIndex];
            var cosA = SinCosLut[doubleIndex + 1];
            var sinB = SinCosLut[doubleIndex + 2];
            var cosB = SinCosLut[doubleIndex + 3];

            sin = sinA + (((sinB - sinA) * fractions) >> PRECISION);
            cos = cosA + (((cosB - cosA) * fractions) >> PRECISION);
        }

        public static void sin_cos_tan(long value, out long sin, out long cos, out long tan) {
            if (value < 0) {
                value = -value;
            }

            var index       = (int) (value >> SHIFT);
            var doubleIndex = index * 2;
            var fractions   = (value - (index << SHIFT)) << 9;

            var sinA = SinCosLut[doubleIndex];
            var cosA = SinCosLut[doubleIndex + 1];
            var sinB = SinCosLut[doubleIndex + 2];
            var cosB = SinCosLut[doubleIndex + 3];

            sin = sinA + (((sinB - sinA) * fractions) >> PRECISION);
            cos = cosA + (((cosB - cosA) * fractions) >> PRECISION);
            tan = (sin << PRECISION) / cos;
        }

        public static long acos(long value) {
            var index    = (int) (value >> SHIFT);
            var fraction = (value - (index << SHIFT)) << 9;
            var a        = AcosLut[index];
            var b        = AcosLut[index + 1];
            var v2       = a + (((b - a) * fraction) >> PRECISION);
            return v2;
        }

        public static long asin(long value) {
            var index    = (int) (value >> SHIFT);
            var fraction = (value - (index << SHIFT)) << 9;
            var a        = AsinLut[index];
            var b        = AsinLut[index + 1];
            var v2       = a + (((b - a) * fraction) >> PRECISION);
            return v2;
        }
    }
}