using System;
using System.Collections.Generic;
using System.Diagnostics;
using FixedPoint;

namespace LUTGenerator {
    public static class BigData {
        public const int  PRECISION       = 16;
        public const long ONE             = 1 << PRECISION;
        
        public static readonly List<int> AcosLut   = new List<int>();
        public static readonly List<int> AsinLut   = new List<int>();

        static BigData() {
            for (int i = 0; i <= ONE; i++) {
                fp val;
                val.value = i;
                var angle   = val.AsDouble;
                var asin    = Math.Asin(angle);
                var moved   = asin * ONE;
                var rounded = moved > 0 ? moved + 0.5f : moved - 0.5f;
                AsinLut.Add((int) rounded);
            }
        }
    }
}