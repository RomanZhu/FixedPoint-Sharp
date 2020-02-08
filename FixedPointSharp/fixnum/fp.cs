using System;
using System.Collections.Generic;

namespace ffg
{
    [Serializable]
    public struct fp : IEquatable<fp>, IComparable<fp>
    {
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

        internal const int FRACTIONS_COUNT = 5;

        internal const long RAW_ZERO = 0;
        internal const long RAW_ONE  = fixlut.ONE;

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


        // as long
        internal long as_long_int => value >> fixlut.PRECISION;

        public long AsLong => as_long_int;

        // as int
        internal int as_int_int => (int) (value >> fixlut.PRECISION);

        public int AsInt => as_int_int;

        // raw long
        internal long raw_long_int => value;

        public long RawLong => raw_long_int;

        // raw int
        internal int raw_int_int => (int) value;

        public int RawInt => raw_int_int;

        // as float
        public float AsFloat => (float) (value / (double) fixlut.ONE);

        // as double
        public double AsDouble => value / (double) fixlut.ONE;

        internal fp as_multiplier_int => this / _100;

        public fp as_multiplier => as_multiplier_int;

        internal fp as_percent_int => this * _100;

        public fp as_percent => as_percent_int;

        static fp()
        {
            min = new fp(long.MinValue);
            max = new fp(long.MaxValue);

            _0  = parse_int(0);
            _1  = parse_int(1);
            _2  = parse_int(2);
            _3  = parse_int(3);
            _4  = parse_int(4);
            _5  = parse_int(5);
            _6  = parse_int(6);
            _7  = parse_int(7);
            _8  = parse_int(8);
            _9  = parse_int(9);
            _10 = parse_int(10);

            _99  = parse_int(99);
            _100 = parse_int(100);
            _200 = parse_int(200);

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

            pi      = new fp(fixlut.PI);
            pi2     = pi * two;
            deg2rad = new fp(1143L);
            rad2deg = new fp(3754936L);
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
            r.value =
#if !FFG_DISABLE_OVERFLOW_CHECK
                checked
#endif
                    (-a.value);

            return r;
        }

        public static fp operator +(fp a)
        {
            fp r;
            r.value =
#if !FFG_DISABLE_OVERFLOW_CHECK
                checked
#endif
                    (+a.value);

            return r;
        }


        public static fp operator +(fp a, fp b)
        {
            fp r;
            r.value =
#if !FFG_DISABLE_OVERFLOW_CHECK
                checked
#endif
                    (a.value + b.value);

            return r;
        }

        public static fp operator -(fp a, fp b)
        {
            fp r;
            r.value =
#if !FFG_DISABLE_OVERFLOW_CHECK
                checked
#endif
                    (a.value - b.value);

            return r;
        }

        public static fp operator *(fp a, fp b)
        {
            fp r;
            r.value =
#if !FFG_DISABLE_OVERFLOW_CHECK
                checked
#endif
                    ((a.value * b.value) >> fixlut.PRECISION);

            return r;
        }

        public static fp operator /(fp a, fp b)
        {
            fp r;
            r.value =
#if !FFG_DISABLE_OVERFLOW_CHECK
                checked
#endif
                    ((a.value << fixlut.PRECISION) / b.value);

            return r;
        }

        public static fp operator %(fp a, fp b)
        {
            fp r;
            r.value =
#if !FFG_DISABLE_OVERFLOW_CHECK
                checked
#endif
                    (a.value % b.value);

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
            if (obj is fp)
            {
                return value == ((fp) obj).value;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return value.GetHashCode();
        }

        public override string ToString()
        {
            return AsFloat.ToString();
            // var v = Math.Abs(value);
            // var s = $"{v >> fixlut.PRECISION}.{(v % fixlut.ONE).ToString().PadLeft(FRACTIONS_COUNT, '0')}";
            //
            // if (value < 0)
            // {
            //     return "-" + s;
            // }
            //
            // return s;
        }

        internal static fp raw_unsafe_int(long value)
        {
            return new fp(value);
        }

        public static fp raw_unsafe(long value)
        {
            return raw_unsafe_int(value);
        }

        internal static fp parse_int(long value)
        {
            return new fp(checked(value << fixlut.PRECISION));
        }

        public static fp Parse(long value)
        {
            return parse_int(value);
        }

        internal static fp parse_int(float value)
        {
            return new fp(checked((long) (value * fixlut.ONE)));
        }

        public static fp Parse(float value)
        {
            return parse_int(value);
        }

        static long ParseInteger(string format)
        {
            return long.Parse(format) * fixlut.ONE;
        }

        static long ParseFractions(string format)
        {
            if (format.Length < FRACTIONS_COUNT)
            {
                format = format.PadRight(FRACTIONS_COUNT, '0');
            }

            long integer   = 0;
            long fractions = long.Parse(format);

            while (fractions >= fixlut.ONE)
            {
                integer   += fixlut.ONE;
                fractions -= fixlut.ONE;
            }

            return checked(integer + fractions);
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

            var negative = false;
            var fraction = value[0] == '.';

            if (negative = (value[0] == '-'))
            {
                value = value.Substring(1);
            }

            var parts  = value.Split(new[] {'.'}, StringSplitOptions.RemoveEmptyEntries);
            var parsed = default(long);

            switch (parts.Length)
            {
                case 1:
                    if (fraction)
                    {
                        parsed = ParseFractions(parts[0]);
                    }
                    else
                    {
                        parsed = ParseInteger(parts[0]);
                    }

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
    }
}