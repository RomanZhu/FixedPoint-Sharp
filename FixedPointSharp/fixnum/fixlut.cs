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

        public const int SIN_TABLE_SIZE = 512;
        public static readonly List<int> SinLut2 = new List<int>(SIN_TABLE_SIZE+1);

        static fixlut() {
            for (var i = 0; i < SIN_TABLE_SIZE; i++) {
                var angle = 2 * Math.PI * i / SIN_TABLE_SIZE;
                var sinValue = Math.Sin(angle);
                var moved = sinValue * ONE;
                var rounded = moved > 0 ? moved + 0.5f : moved - 0.5f;
                var rounded2 = (int) rounded;
                SinLut2.Add(rounded2);
            }
            
            SinLut2.Add(SinLut2[0]);
        }

        public static long sin(long value) {
            if (value < 0) {
                value = -value;
            }

            var index = (int) (value >> SHIFT);
            var fraction = (value - (index << SHIFT)) << 9;
            var a = SinLut2[index];
            var b = SinLut2[index + 1];
            var v2 = a + (((b - a) * fraction) >> PRECISION);
            return v2;
        }
        
        public static long cos(long value) {
            value += fp._0_25.value;
            
            if (value < 0) {
                value = -value;
            }

            var index    = (int) (value >> SHIFT);
            var fraction = (value - (index << SHIFT)) << 9;
            var a        = SinLut2[index];
            var b        = SinLut2[index + 1];
            var v2       = a + (((b - a) * fraction) >> PRECISION);
            return v2;
        }

        public static void sin_cos(long value, out long sin, out long cos) {
            if (value < 0) {
                value = -value;
            }

            var index    = (int) (value >> SHIFT);
            var fraction = (value - (index << SHIFT)) << 9;
            var a        = SinLut2[index];
            var b        = SinLut2[index + 1];
            sin = a + (((b - a) * fraction) >> PRECISION);

            value += fp._0_25.value;

            index    = (int) (value >> SHIFT);
            fraction = (value - (index << SHIFT)) << 9;
            a        = SinLut2[index];
            b        = SinLut2[index + 1];
            cos      = a + (((b - a) * fraction) >> PRECISION);
        }
        
        public static void sin_cos_tan(long value, out long sin, out long cos, out long tan) {
            if (value < 0) {
                value = -value;
            }

            var index    = (int) (value >> SHIFT);
            var fraction = (value - (index << SHIFT)) << 9;
            var a        = SinLut2[index];
            var b        = SinLut2[index + 1];
            sin = a + (((b - a) * fraction) >> PRECISION);

            value += fp._0_25.value;
            index    = (int) (value >> SHIFT);
            fraction = (value - (index << SHIFT)) << 9;
            a        = SinLut2[index];
            b        = SinLut2[index + 1];
            cos      = a + (((b - a) * fraction) >> PRECISION);
            
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
            var a        = SinLut2[index];
            var b        = SinLut2[index + 1];
            var v2       = a + (((b - a) * fraction) >> PRECISION);
            return v2;
        }
    }
}