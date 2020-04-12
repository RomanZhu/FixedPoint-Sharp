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

        public static readonly List<uint> SqrtLut16  = new List<uint>();
        public static readonly List<uint> SqrtLut256 = new List<uint>();
        public static readonly List<uint> SqrtLut65536 = new List<uint>();
        public static readonly List<uint> SqrtLut36Mil = new List<uint>();

        static fixlut() {
            for (var i = 0; i <= 128; i++) {
                double addition = 0;
                var    d        = Math.Sqrt(i / 8.0);
                if (i > 0 && i < 60) {
                    var before             = Math.Sqrt((i - 1) / 8.0);
                    var after              = Math.Sqrt((i + 1) / 8.0);
                    var approximatedBefore = Lerp(before, d,     0.5f);
                    var approximatedAfter  = Lerp(d,      after, 0.5f);
                    var actualBefore       = Math.Sqrt((i - 0.5f) / 8.0);
                    var actualAfter        = Math.Sqrt((i + 0.5f) / 8.0);
                    var errorBefore        = actualBefore - approximatedBefore;
                    var errorAfter         = actualAfter - approximatedAfter;
                    addition = Lerp(Math.Min(errorBefore, errorAfter), Math.Max(errorBefore, errorAfter), 0.65f) / 2f;
                    if (i == 2)
                        addition -= 0.001f;
                }

                SqrtLut16.Add((uint) ((d + addition) * ONE + 0.5f));
            }

            for (var i = 0; i <= 120; i++) {
                double addition = 0;
                var    d        = Math.Sqrt(16.0 + i * 2.0);
                if (i > 0 && i < 60) {
                    var before             = Math.Sqrt(16.0 + (i - 1) * 2.0);
                    var after              = Math.Sqrt(16.0 + (i + 1) * 2.0);
                    var approximatedBefore = Lerp(before, d,     0.5f);
                    var approximatedAfter  = Lerp(d,      after, 0.5f);
                    var actualBefore       = Math.Sqrt(16.0 + (i - 0.5f) * 2.0);
                    var actualAfter        = Math.Sqrt(16.0 + (i + 0.5f) * 2.0);
                    var errorBefore        = actualBefore - approximatedBefore;
                    var errorAfter         = actualAfter - approximatedAfter;
                    addition = Lerp(Math.Min(errorBefore, errorAfter), Math.Max(errorBefore, errorAfter), 0.75f) / 2f;
                }

                SqrtLut256.Add((uint) ((d + addition) * ONE + 0.5f));
            }

            for (var i = 0; i <= 128; i++) {
                double addition = 0;
                var    d        = Math.Sqrt(256.0 + i * 2.0 * 256.0);
                if (i > 0 && i < 60) {
                    var before             = Math.Sqrt(256.0 + (i - 1) * 2.0 * 256.0);
                    var after              = Math.Sqrt(256.0 + (i + 1) * 2.0 * 256.0);
                    var approximatedBefore = Lerp(before, d,     0.5f);
                    var approximatedAfter  = Lerp(d,      after, 0.5f);
                    var actualBefore       = Math.Sqrt(256.0 + (i - 0.5f) * 2.0 * 256.0);
                    var actualAfter        = Math.Sqrt(256.0 + (i + 0.5f) * 2.0 * 256.0);
                    var errorBefore        = actualBefore - approximatedBefore;
                    var errorAfter         = actualAfter - approximatedAfter;
                    addition = Lerp(Math.Min(errorBefore, errorAfter), Math.Max(errorBefore, errorAfter), 0.75f) / 2f;
                }

                SqrtLut65536.Add((uint) ((d + addition) * ONE + 0.5f));
            }

            for (var i = 0; i <= 128; i++) {
                double addition = 0;
                var    d        = Math.Sqrt(65536.0 + i * 262144.0);
                if (i > 1 && i < 60) {
                    var before             = Math.Sqrt(65536.0 + (i - 1) * 262144.0);
                    var after              = Math.Sqrt(65536.0 + (i + 1) * 262144.0);
                    var approximatedBefore = Lerp(before, d,     0.5f);
                    var approximatedAfter  = Lerp(d,      after, 0.5f);
                    var actualBefore       = Math.Sqrt(65536.0 + (i - 0.5f) * 262144.0);
                    var actualAfter        = Math.Sqrt(65536.0 + (i + 0.5f) * 262144.0);
                    var errorBefore        = actualBefore - approximatedBefore;
                    var errorAfter         = actualAfter - approximatedAfter;
                    addition = Lerp(Math.Min(errorBefore, errorAfter), Math.Max(errorBefore, errorAfter), 0.75f) / 2f;
                    if (i == 2)
                        addition -= 1.501f;
                }

                SqrtLut36Mil.Add((uint) ((d + addition) * ONE + 0.5f));
            }

            double Lerp(double a, double b, double t) {
                return a + t * (b - a);
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

        public static long sqrt(long value) {
            int  val1;
            int  val2;
            long fraction;

            if (value < 16 << 16) {
                var index = (int) (value >> 13);
                fraction = (value << 3) & 0x0ffff;
                //Compensate lack of precision in the 0.0 - 0.125 range
                 if (index == 0) {
                     if (fraction < 32768) {
                         var normFraction = fraction << 1;
                         if (normFraction > 32768) {
                             return 11650 + (((16505 - 11650) * ((normFraction - 32768) << 1)) >> PRECISION);
                         }
                
                         if (normFraction > 16384) {
                             return 8250 + (((11650 - 8250) * ((normFraction - 16384) << 2)) >> PRECISION);
                         }
                
                         if (normFraction > 8192) {
                             return 5850 + (((8250 - 5850) * ((normFraction - 8192) << 3)) >> PRECISION);
                         }
                
                         if (normFraction > 4096) {
                             return 4135 + (((5850 - 4135) * ((normFraction - 4096) << 4)) >> PRECISION);
                         }
                
                         if (normFraction > 2048) {
                             return 2955 + (((4135 - 2955) * ((normFraction - 2048) << 5)) >> PRECISION);
                         }
                
                         if (normFraction > 1024) {
                             return 2025 + (((2955 - 2025) * ((normFraction - 1024) << 6)) >> PRECISION);
                         }
                
                         if (normFraction > 512) {
                             return 1485 + (((2025 - 1485) * ((normFraction - 512) << 7)) >> PRECISION);
                         }
                
                         if (normFraction > 256) {
                             return 1055 + (((1485 - 1055) * ((normFraction - 256) << 8)) >> PRECISION);
                         }
                
                         if (normFraction > 128) {
                             return 750 + (((1055 - 750) * ((normFraction - 128) << 9)) >> PRECISION);
                         }
                
                         if (normFraction > 64) {
                             return 550 + (((750 - 550) * ((normFraction - 64) << 10)) >> PRECISION);
                         }
                
                         if (normFraction > 32) {
                             return 380 + (((550 - 380) * ((normFraction - 32) << 11)) >> PRECISION);
                         }
                
                         return (380 * (normFraction << 11)) >> PRECISION;
                     }
                
                     return 16505 + (((23330 - 16505) * ((fraction - 32768) << 1)) >> PRECISION);
                 }
                
                 if (index == 1) {
                     if (fraction < 32768) {
                         return 23330 + (((28420 - 23330) * (fraction << 1)) >> PRECISION);
                     }
                
                     return 28420 + (((32868 - 28420) * ((fraction - 32768) << 1)) >> PRECISION);
                 }

                val1 = (int) SqrtLut16[index];
                val2 = (int) SqrtLut16[index + 1];
            }
            else if (value < 256 << 16) {
                var index = (int) (((value >> 16) - 16) >> 1);
                fraction = (value >> 1) & 0x0ffff;
                if (index == 0) {
                    if (fraction < 32768) {
                        return 262144 + (((270212 - 262144) * (fraction << 1)) >> PRECISION);
                    }
                
                    return 270212 + (((278046 - 270212) * ((fraction - 32768) << 1)) >> PRECISION);
                }
                
                if (index == 1) {
                    if (fraction < 32768) {
                        return 278046 + (((285665 - 278046) * (fraction << 1)) >> PRECISION);
                    }
                
                    return 285665 + (((293130 - 285665) * ((fraction - 32768) << 1)) >> PRECISION);
                }

                val1 = (int) SqrtLut256[index];
                val2 = (int) SqrtLut256[index + 1];
            }
            else if (value < (long) 65536 << 16) {
                var index = (int) (((value >> 16) - 256) >> 9);
                fraction = ((value - (256 << 16)) >> 9) & 0x0ffff;

                if (index == 0) {
                    if (fraction < 32768) {
                        return 1048576 + (((1495910 - 1048576) * (fraction << 1)) >> PRECISION);
                    }
                
                    return 1495910 + (((1816187 - 1495910) * ((fraction - 32768) << 1)) >> PRECISION);
                }
                
                if (index == 1) {
                    if (fraction < 32768) {
                        return 1816187 + (((2097152 - 1816187) * (fraction << 1)) >> PRECISION);
                    }
                
                    return 2097152 + (((2351887 - 2097152) * ((fraction - 32768) << 1)) >> PRECISION);
                }

                val1 = (int) SqrtLut65536[index];
                val2 = (int) SqrtLut65536[index + 1];
            }
            else if(value < (long)33619968<<16) {
                var index = (int) (((value >> 16) - 65536) >> 18);
                fraction = ((value - ((long) 65536 << 16)) >> 18) & 0x0ffff;

                if (index == 0) {
                    if (fraction < 32768) {
                        var normFraction = fraction << 1;
                        if (normFraction > 32768) {
                            return 23918103 + (((29258990 - 23918103) * ((normFraction - 32768) << 1)) >> PRECISION);
                        }
                
                        return 16777216 + (((23918103 - 16777216) * (normFraction << 1)) >> PRECISION);
                    }
                
                    return 29258990 + (((37554995 - 29258990) * ((fraction - 32768) << 1)) >> PRECISION);
                }
                
                if (index == 1) {
                    if (fraction < 32768) {
                        return 37554995 + (((44488341 - 37554995) * (fraction << 1)) >> PRECISION);
                    }
                
                    return 44488341 + (((50431648 - 44488341) * ((fraction - 32768) << 1)) >> PRECISION);
                }

                val1 = (int) SqrtLut36Mil[index];
                val2 = (int) SqrtLut36Mil[index + 1];
            }
            else {
                return fixmath.Sqrt(fp.ParseRaw(value)).value;
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