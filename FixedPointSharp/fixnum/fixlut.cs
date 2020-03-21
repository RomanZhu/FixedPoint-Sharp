using System;
using System.Collections.Generic;

namespace FixedPoint
{
    public static partial class fixlut
    {
        public const int  FRACTIONS_COUNT = 5;
        public const int  PRECISION = 16;
        public const int  SHIFT = 16 - 9;
        public const long PI        = 205887L;
        public const long ONE       = 1 << PRECISION;
        public const long ZERO      = 0;

        public const int SIN_VALUES_COUNT = 512;
        public static readonly List<int> SinCosLut = new List<int>(SIN_VALUES_COUNT * 2 + 2);

        static fixlut() {
            for (var i = 0; i < SIN_VALUES_COUNT; i++) {
                var angle = 2 * Math.PI * i / SIN_VALUES_COUNT;
                
                var sinValue = Math.Sin(angle);
                var movedSin = sinValue * ONE;
                var roundedSin = movedSin > 0 ? movedSin + 0.5f : movedSin - 0.5f;
                SinCosLut.Add((int) roundedSin);
                
                var cosValue = Math.Cos(angle);
                var movedCos    = cosValue * ONE;
                var roundedCos  = movedCos > 0 ? movedCos + 0.5f : movedCos - 0.5f;
                SinCosLut.Add((int) roundedCos);
            }
            
            SinCosLut.Add(SinCosLut[0]);
            SinCosLut.Add(SinCosLut[1]);
        }

        public static long sin(long value) {
            if (value < 0) {
                value = -value;
            }

            var index = (int) (value >> SHIFT);
            var doubleIndex = index * 2;
            var fraction = (value - (index << SHIFT)) << 9;
            var a = SinCosLut[doubleIndex];
            var b = SinCosLut[doubleIndex + 2];
            var v2 = a + (((b - a) * fraction) >> PRECISION);
            return v2;
        }
        
        public static long cos(long value) {
            if (value < 0) {
                value = -value;
            }

            var index    = (int) (value >> SHIFT);
            var doubleIndex = index * 2;
            var fraction = (value - (index << SHIFT)) << 9;
            var a        = SinCosLut[doubleIndex + 1];
            var b        = SinCosLut[doubleIndex + 3];
            var v2       = a + (((b - a) * fraction) >> PRECISION);
            return v2;
        }

        public static void sin_cos(long value, out long sin, out long cos) {
            if (value < 0) {
                value = -value;
            }

            var index     = (int) (value >> SHIFT);
            var doubleIndex = index * 2;
            var fractions = (value - (index << SHIFT)) << 9;

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

            var index     = (int) (value >> SHIFT);
            var doubleIndex = index * 2;
            var fractions = (value - (index << SHIFT)) << 9;

            var sinA = SinCosLut[doubleIndex];
            var cosA = SinCosLut[doubleIndex + 1];
            var sinB = SinCosLut[doubleIndex + 2];
            var cosB = SinCosLut[doubleIndex + 3];

            sin = sinA + (((sinB - sinA) * fractions) >> PRECISION);
            cos = cosA + (((cosB - cosA) * fractions) >> PRECISION);
            tan = (sin << PRECISION) / cos;
        }
        
        public static long sqrt_aprox(long value)
        {
#if FIXNUM_STRIP_LUTS
            return 0;
#else
            return sqrt_aprox_lut[value];
#endif
        }

        public static long acos(long value)
        {
#if FIXNUM_STRIP_LUTS
            return 0;
#else
            if ((value < -65536) || (value > 65536))
            {
                return long.MinValue;
            }

            return acos_lut[value + 65536];
#endif
        }
        
        public static long acos2(long value) {
            value += fp._0_25.value;
            
            if (value < 0) {
                value = -value;
            }

            var index    = (int) (value >> SHIFT);
            var fraction = (value - (index << SHIFT)) << 9;
            var a        = SinCosLut[index];
            var b        = SinCosLut[index + 1];
            var v2       = a + (((b - a) * fraction) >> PRECISION);
            return v2;
        }
    }
}