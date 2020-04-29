using System;
using System.Collections.Generic;

namespace LUTGenerator {
    public static class Data {
        public const int  PRECISION       = 16;
        public const long ONE             = 1 << PRECISION;

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

        static Data() {
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
    }
}