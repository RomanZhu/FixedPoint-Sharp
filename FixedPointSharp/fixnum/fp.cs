using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace FixedPoint {
    [Serializable]
    [StructLayout(LayoutKind.Explicit)]
    public struct fp : IEquatable<fp>, IComparable<fp> {
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
        public static readonly fp pi_div_4;
        public static readonly fp one_div_pi2;
        public static readonly fp deg2rad;
        public static readonly fp rad2deg;
        public static readonly fp rad_90degrees;
        public static readonly fp epsilon;
        public static readonly fp e;

        [FieldOffset(0)]
        public long value;

        public long   RawLong  => value;
        public int    RawInt   => (int) value;
        public long   AsLong   => value >> fixlut.PRECISION;
        public int    AsInt    => (int) (value >> fixlut.PRECISION);
        public float  AsFloat  => (float) (value / (double) fixlut.ONE);
        public double AsDouble => value / (double) fixlut.ONE;

        public fp AsMultiplier => this / _100;
        public fp AsPercent    => this * _100;

        static fp() {
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

            _0_01 = _1 / _100;
            _0_02 = _0_01 * _2;
            _0_03 = _0_01 * _3;
            _0_04 = _0_01 * _4;
            _0_05 = _0_01 * _5;

            _0_10 = _1 / _10;
            _0_20 = _1 / _5;
            _0_25 = _1 / _4;
            _0_33 = _1 / _3;
            _0_50 = _1 / _2;
            _0_75 = _0_25 * _3;
            _0_95 = _1 - _0_05;

            _1_10 = _1 + _0_10;

            zero               = new fp(0);
            one                = new fp(fixlut.ONE);
            two                = one + one;
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
            e             = new fp(178145L);
            one_div_pi2   = one / pi2;
            pi_div_4      = pi / _4;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal fp(long v) {
            value = v;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp operator -(fp a) {
            fp r;
            r.value = -a.value;
            return r;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp operator +(fp a) {
            fp r;
            r.value = +a.value;
            return r;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp operator +(fp a, fp b) {
            fp r;
            r.value = a.value + b.value;
            return r;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp operator -(fp a, fp b) {
            fp r;
            r.value = a.value - b.value;
            return r;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp operator *(fp a, fp b) {
            fp r;
            r.value = (a.value * b.value) >> fixlut.PRECISION;
            return r;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp operator /(fp a, fp b) {
            fp r;
            r.value = (a.value << fixlut.PRECISION) / b.value;
            return r;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp operator %(fp a, fp b) {
            fp r;
            r.value = a.value % b.value;
            return r;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator <(fp a, fp b) {
            return a.value < b.value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator <=(fp a, fp b) {
            return a.value <= b.value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator >(fp a, fp b) {
            return a.value > b.value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator >=(fp a, fp b) {
            return a.value >= b.value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(fp a, fp b) {
            return a.value == b.value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(fp a, fp b) {
            return a.value != b.value;
        }

        public int CompareTo(fp other) {
            return value.CompareTo(other.value);
        }

        public bool Equals(fp other) {
            return value == other.value;
        }

        public override bool Equals(object obj) {
            return obj is fp other && this == other;
        }

        public override int GetHashCode() {
            return value.GetHashCode();
        }

        public override string ToString() {
            var corrected = Math.Round(AsDouble, 5);
            return $"{corrected:F5}";
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp ParseRaw(long value) {
            return new fp(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp Parse(long value) {
            return new fp(value << fixlut.PRECISION);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp Parse(float value) {
            return new fp((long) (value * fixlut.ONE + 0.5f * (value < 0 ? -1 : 1)));
        }

        public static fp Parse(string value) {
            var doubleValue = double.Parse(value);
            var longValue   = (long) (doubleValue * fixlut.ONE);
            return new fp(longValue);
        }

        public class Comparer : IComparer<fp> {
            public static readonly Comparer instance = new Comparer();

            private Comparer() { }

            int IComparer<fp>.Compare(fp x, fp y) {
                return x.value.CompareTo(y.value);
            }
        }

        public class EqualityComparer : IEqualityComparer<fp> {
            public static readonly EqualityComparer instance = new EqualityComparer();

            private EqualityComparer() { }

            bool IEqualityComparer<fp>.Equals(fp x, fp y) {
                return x.value == y.value;
            }

            int IEqualityComparer<fp>.GetHashCode(fp num) {
                return num.value.GetHashCode();
            }
        }
    }
}