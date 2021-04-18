using System;
using System.Collections.Generic;

namespace LUTGenerator {
    public static class Data {
        public const int  PRECISION       = 16;
        public const long ONE             = 1 << PRECISION;

        public const int SIN_VALUE_COUNT     = 512;
        public const int SIN_COS_VALUE_COUNT = 512;
        public const int TAN_VALUE_COUNT     = 512;

        public static readonly List<int> SinLut    = new List<int>(SIN_VALUE_COUNT + 1);
        public static readonly List<int> SinCosLut = new List<int>(SIN_COS_VALUE_COUNT * 2 + 2);
        public static readonly List<int> TanLut    = new List<int>(TAN_VALUE_COUNT + 1);

        static Data() {
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
        }
    }
}