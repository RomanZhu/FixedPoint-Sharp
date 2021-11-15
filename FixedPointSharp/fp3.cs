using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Deterministic.FixedPoint {
    [Serializable]
    [StructLayout(LayoutKind.Explicit, Size = SIZE)]
    public struct fp3 : IEquatable<fp3> {
        public const int SIZE = 24;

        public static readonly fp3 left      = new fp3(-fp._1,       fp._0,        fp._0);
        public static readonly fp3 right     = new fp3(fp._1,        fp._0,        fp._0);
        public static readonly fp3 up        = new fp3(fp._0,        fp._1,        fp._0);
        public static readonly fp3 down      = new fp3(fp._0,        fp.minus_one, fp._0);
        public static readonly fp3 forward   = new fp3(fp._0,        fp._0,        fp._1);
        public static readonly fp3 backward  = new fp3(fp._0,        fp._0,        fp.minus_one);
        public static readonly fp3 one       = new fp3(fp._1,        fp._1,        fp._1);
        public static readonly fp3 minus_one = new fp3(fp.minus_one, fp.minus_one, fp.minus_one);
        public static readonly fp3 zero      = new fp3(fp._0,        fp._0,        fp._0);

        [FieldOffset(0)]
        public fp x;

        [FieldOffset(8)]
        public fp y;

        [FieldOffset(16)]
        public fp z;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public fp3(fp x, fp y, fp z) {
            this.x.value = x.value;
            this.y.value = y.value;
            this.z.value = z.value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public fp3(fp2 xy, fp z) {
            x.value      = xy.x.value;
            y.value      = xy.y.value;
            this.z.value = z.value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp3 operator +(fp3 a, fp3 b) {
            a.x.value += b.x.value;
            a.y.value += b.y.value;
            a.z.value += b.z.value;

            return a;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp3 operator -(fp3 a, fp3 b) {
            a.x.value -= b.x.value;
            a.y.value -= b.y.value;
            a.z.value -= b.z.value;

            return a;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp3 operator -(fp3 a) {
            a.x.value = -a.x.value;
            a.y.value = -a.y.value;
            a.z.value = -a.z.value;

            return a;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp3 operator *(fp3 a, fp3 b) {
            a.x.value = (a.x.value * b.x.value) >> fixlut.PRECISION;
            a.y.value = (a.y.value * b.y.value) >> fixlut.PRECISION;
            a.z.value = (a.z.value * b.z.value) >> fixlut.PRECISION;

            return a;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp3 operator *(fp3 a, fp b) {
            a.x.value = (a.x.value * b.value) >> fixlut.PRECISION;
            a.y.value = (a.y.value * b.value) >> fixlut.PRECISION;
            a.z.value = (a.z.value * b.value) >> fixlut.PRECISION;

            return a;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp3 operator *(fp b, fp3 a) {
            a.x.value = (a.x.value * b.value) >> fixlut.PRECISION;
            a.y.value = (a.y.value * b.value) >> fixlut.PRECISION;
            a.z.value = (a.z.value * b.value) >> fixlut.PRECISION;

            return a;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp3 operator /(fp3 a, fp3 b) {
            a.x.value = (a.x.value << fixlut.PRECISION) / b.x.value;
            a.y.value = (a.y.value << fixlut.PRECISION) / b.y.value;
            a.z.value = (a.z.value << fixlut.PRECISION) / b.z.value;

            return a;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp3 operator /(fp3 a, fp b) {
            a.x.value = (a.x.value << fixlut.PRECISION) / b.value;
            a.y.value = (a.y.value << fixlut.PRECISION) / b.value;
            a.z.value = (a.z.value << fixlut.PRECISION) / b.value;

            return a;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp3 operator /(fp b, fp3 a) {
            a.x.value = (a.x.value << fixlut.PRECISION) / b.value;
            a.y.value = (a.y.value << fixlut.PRECISION) / b.value;
            a.z.value = (a.z.value << fixlut.PRECISION) / b.value;

            return a;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(fp3 a, fp3 b) {
            return a.x.value == b.x.value && a.y.value == b.y.value && a.z.value == b.z.value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(fp3 a, fp3 b) {
            return a.x.value != b.x.value || a.y.value != b.y.value || a.z.value != b.z.value;
        }

        public bool Equals(fp3 other) {
            return x.Equals(other.x) && y.Equals(other.y) && z.Equals(other.z);
        }

        public override bool Equals(object obj) {
            return obj is fp3 other && this == other;
        }

        public override int GetHashCode() {
            unchecked {
                var hashCode = x.GetHashCode();
                hashCode = (hashCode * 397) ^ y.GetHashCode();
                hashCode = (hashCode * 397) ^ z.GetHashCode();
                return hashCode;
            }
        }

        public override string ToString() {
            return $"({x}, {y}, {z})";
        }

        public class EqualityComparer : IEqualityComparer<fp3> {
            public static readonly EqualityComparer instance = new EqualityComparer();

            private EqualityComparer() { }

            bool IEqualityComparer<fp3>.Equals(fp3 x, fp3 y) {
                return x == y;
            }

            int IEqualityComparer<fp3>.GetHashCode(fp3 obj) {
                return obj.GetHashCode();
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public fp3 Normalize() {
            return fixmath.Normalize(this);
        }
    }
}