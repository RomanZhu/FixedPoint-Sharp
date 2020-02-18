using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace FixedPoint
{
    [Serializable]
    public struct fp : IEquatable<fp>, IComparable<fp>
    {
        private const long VISUALIZATION_FACTOR    = 152601;
        private const long VISUALIZATION_FRACTIONS = 100000;

        public class Comparer : IComparer<fp>
        {
            public static readonly Comparer instance = new Comparer();

            Comparer()
            {
            }

            int IComparer<fp>.Compare(fp x, fp y)
            {
                return x.value.CompareTo(y.value);
            }
        }

        public class EqualityComparer : IEqualityComparer<fp>
        {
            public static readonly EqualityComparer instance = new EqualityComparer();

            EqualityComparer()
            {
            }

            bool IEqualityComparer<fp>.Equals(fp x, fp y)
            {
                return x.value == y.value;
            }

            int IEqualityComparer<fp>.GetHashCode(fp num)
            {
                return num.value.GetHashCode();
            }
        }

        public static readonly fp min;
        public static readonly fp max;
        public static readonly fp _0;
        public static readonly fp _0_01;
        public static readonly fp _0_02;
        public static readonly fp _0_03;
        public static readonly fp _0_04;
        public static readonly fp _0_05;
        public static readonly fp _0_10;
        public static readonly fp _0_20;
        public static readonly fp _0_25;
        public static readonly fp _0_33;
        public static readonly fp _0_50;
        public static readonly fp _0_75;
        public static readonly fp _0_95;
        public static readonly fp _1;
        public static readonly fp _1_10;
        public static readonly fp _2;
        public static readonly fp _3;
        public static readonly fp _4;
        public static readonly fp _5;
        public static readonly fp _6;
        public static readonly fp _7;
        public static readonly fp _8;
        public static readonly fp _9;
        public static readonly fp _10;
        public static readonly fp _99;
        public static readonly fp _100;
        public static readonly fp _200;
        public static readonly fp zero;
        public static readonly fp one;
        public static readonly fp two;
        public static readonly fp three;
        public static readonly fp half;
        public static readonly fp point_99;
        public static readonly fp point_33;
        public static readonly fp point_one;
        public static readonly fp point_zero_one;
        public static readonly fp minus_one;
        public static readonly fp one_point_five;
        public static readonly fp one_point_zero_one;
        public static readonly fp pi;
        public static readonly fp pi2;
        public static readonly fp deg2rad;
        public static readonly fp rad2deg;
        public static readonly fp rad_90degrees;
        public static readonly fp epsilon;

        public long value;

        public long   RawLong  => value;
        public int    RawInt   => (int) value;
        public long   AsLong   => value >> fixlut.PRECISION;
        public int    AsInt    => (int) (value >> fixlut.PRECISION);
        public float  AsFloat  => (float) (value / (double) fixlut.ONE);
        public double AsDouble => value / (double) fixlut.ONE;

        public fp AsMultiplier => this / _100;
        public fp AsPercent    => this * _100;

        static fp()
        {
            min = new fp(long.MinValue);
            max = new fp(long.MaxValue);

            _0  = Parse(0);
            _1  = Parse(1);
            _2  = Parse(2);
            _3  = Parse(3);
            _4  = Parse(4);
            _5  = Parse(5);
            _6  = Parse(6);
            _7  = Parse(7);
            _8  = Parse(8);
            _9  = Parse(9);
            _10 = Parse(10);

            _99  = Parse(99);
            _100 = Parse(100);
            _200 = Parse(200);

            _0_01 = _1    / _100;
            _0_02 = _0_01 * _2;
            _0_03 = _0_01 * _3;
            _0_04 = _0_01 * _4;
            _0_05 = _0_01 * _5;

            _0_10 = _1    / _10;
            _0_20 = _1    / _5;
            _0_25 = _1    / _4;
            _0_33 = _1    / _3;
            _0_50 = _1    / _2;
            _0_75 = _0_25 * _3;
            _0_95 = _1 - _0_05;

            _1_10 = _1 + _0_10;

            zero               = new fp(0);
            one                = new fp(fixlut.ONE);
            two                = one       + one;
            three              = one + one + one;
            half               = one / two;
            point_one          = new fp(6553L);
            point_zero_one     = point_one * point_one;
            minus_one          = new fp(-fixlut.ONE);
            point_99           = one - point_zero_one;
            point_33           = one / three;
            one_point_five     = one + half;
            one_point_zero_one = one + point_zero_one;

            pi            = new fp(fixlut.PI);
            pi2           = pi * two;
            deg2rad       = new fp(1143L);
            rad2deg       = new fp(3754936L);
            rad_90degrees = pi * half;
            epsilon       = new fp(1);
        }

        internal fp(long v)
        {
            value = v;
        }

        public static fp operator -(fp a)
        {
            fp r;
            r.value = -a.value;
            return r;
        }

        public static fp operator +(fp a)
        {
            fp r;
            r.value = +a.value;
            return r;
        }


        public static fp operator +(fp a, fp b)
        {
            fp r;
            r.value = a.value + b.value;
            return r;
        }

        public static fp operator -(fp a, fp b)
        {
            fp r;
            r.value = a.value - b.value;
            return r;
        }

        public static fp operator *(fp a, fp b)
        {
            fp r;
            r.value = (a.value * b.value) >> fixlut.PRECISION;
            return r;
        }

        public static fp operator /(fp a, fp b)
        {
            fp r;
            r.value = (a.value << fixlut.PRECISION) / b.value;
            return r;
        }

        public static fp operator %(fp a, fp b)
        {
            fp r;
            r.value = a.value % b.value;
            return r;
        }

        public static bool operator <(fp a, fp b)
        {
            return a.value < b.value;
        }

        public static bool operator <=(fp a, fp b)
        {
            return a.value <= b.value;
        }

        public static bool operator >(fp a, fp b)
        {
            return a.value > b.value;
        }

        public static bool operator >=(fp a, fp b)
        {
            return a.value >= b.value;
        }

        public static bool operator ==(fp a, fp b)
        {
            return a.value == b.value;
        }

        public static bool operator !=(fp a, fp b)
        {
            return a.value != b.value;
        }

        public int CompareTo(fp other)
        {
            return value.CompareTo(other.value);
        }

        public bool Equals(fp other)
        {
            return value == other.value;
        }

        public override bool Equals(object obj)
        {
            return obj is fp other && this == other;
        }

        public override int GetHashCode()
        {
            return value.GetHashCode();
        }

        public override string ToString()
        {
            var v                 = Math.Abs(value);
            var fraction          = v % fixlut.ONE;
            var correctedFraction = fraction * VISUALIZATION_FACTOR / VISUALIZATION_FRACTIONS;

            var s = $"{v >> fixlut.PRECISION}.{correctedFraction.ToString().PadLeft(fixlut.FRACTIONS_COUNT, '0')}";

            if (value < 0)
            {
                return "-" + s;
            }

            return s;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp ParseRaw(long value)
        {
            return new fp(value);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp Parse(long value)
        {
            return new fp(value << fixlut.PRECISION);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp Parse(float value)
        {
            value = (float)Math.Round(value, 5);
            var str = value.ToString("F5", CultureInfo.InvariantCulture);
            var split = str.Split('.');
            var fractionStr = split[1].PadRight(fixlut.FRACTIONS_COUNT, '0');
            return Parse($"{split[0]}.{fractionStr.Substring(0, 5)}");
        }

        public static fp Parse(string value)
        {
            if (value == null)
            {
                return zero;
            }

            if (value.Trim().Length == 0)
            {
                return zero;
            }
            
            var negative = value[0] == '-';

            if (negative)
            {
                value = value.Substring(1);
            }

            var fraction = value[0] == '.';

            var  parts = value.Split(new []{'.'}, StringSplitOptions.RemoveEmptyEntries);
            long parsed;

            switch (parts.Length)
            {
                case 1:
                    parsed = fraction ? ParseFractions(parts[0]) : ParseInteger(parts[0]);
                    break;

                case 2:
                    parsed = checked(ParseInteger(parts[0]) + ParseFractions(parts[1]));
                    break;

                default:
                    throw new FormatException(value);
            }

            if (negative)
            {
                return new fp(-parsed);
            }

            return new fp(parsed);
        }
        
        private static long ParseInteger(string format)
        {
            return long.Parse(format) * fixlut.ONE;
        }

        private static long ParseFractions(string format)
        {
            if (format.Length < fixlut.FRACTIONS_COUNT)
            {
                format = format.PadRight(fixlut.FRACTIONS_COUNT, '0');
            }

            long integer   = 0;
            long fractions = long.Parse(format);
            fractions = fractions * VISUALIZATION_FRACTIONS / VISUALIZATION_FACTOR;

            while (fractions >= fixlut.ONE)
            {
                integer   += fixlut.ONE;
                fractions -= fixlut.ONE;
            }

            return checked(integer + fractions);
        }
    }
}