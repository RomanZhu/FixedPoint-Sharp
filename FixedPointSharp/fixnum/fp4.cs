using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace FixedPoint {
    [StructLayout(LayoutKind.Explicit)]
    public struct fp4 : IEquatable<fp4> {
        public const int SIZE = 32;

        [FieldOffset(0)]
        public fp x;

        [FieldOffset(8)]
        public fp y;

        [FieldOffset(16)]
        public fp z;

        [FieldOffset(24)]
        public fp w;

        public static readonly fp4 zero;
        public static readonly fp4 one = new fp4 {x = fp._1, y = fp._1, z = fp._1, w = fp._1};
        public static readonly fp4 minus_one = new fp4 {x = fp.minus_one, y = fp.minus_one, z = fp.minus_one, w = fp.minus_one};

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public fp4(fp x, fp y, fp z, fp w) {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public fp4(fp2 xy, fp2 zw) {
            x = xy.x;
            y = xy.y;
            z = zw.x;
            w = zw.y;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public fp4(fp3 v, fp w) {
            x      = v.x;
            y      = v.y;
            z      = v.z;
            this.w = w;
        }

        /// <summary>
        /// Initializes fp4 vector with 48.16 fp format long values
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal fp4(long x, long y, long z, long w) {
            this.x.value = x;
            this.y.value = y;
            this.z.value = z;
            this.w.value = w;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp4 operator *(fp4 lhs, fp4 rhs) {
            return new fp4((lhs.x.value * rhs.x.value) >> fixlut.PRECISION, (lhs.y.value * rhs.y.value) >> fixlut.PRECISION,
                (lhs.z.value * rhs.z.value) >> fixlut.PRECISION, (lhs.w.value * rhs.w.value) >> fixlut.PRECISION);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp4 operator *(fp4 lhs, fp rhs) {
            return new fp4(lhs.x * rhs, lhs.y * rhs, lhs.z * rhs, lhs.w * rhs);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp4 operator *(fp lhs, fp4 rhs) {
            return new fp4(lhs * rhs.x, lhs * rhs.y, lhs * rhs.z, lhs * rhs.w);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp4 operator +(fp4 lhs, fp4 rhs) {
            return new fp4(lhs.x + rhs.x, lhs.y + rhs.y, lhs.z + rhs.z, lhs.w + rhs.w);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp4 operator +(fp4 lhs, fp rhs) {
            return new fp4(lhs.x + rhs, lhs.y + rhs, lhs.z + rhs, lhs.w + rhs);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp4 operator +(fp lhs, fp4 rhs) {
            return new fp4(lhs + rhs.x, lhs + rhs.y, lhs + rhs.z, lhs + rhs.w);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp4 operator -(fp4 lhs, fp4 rhs) {
            return new fp4(lhs.x - rhs.x, lhs.y - rhs.y, lhs.z - rhs.z, lhs.w - rhs.w);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp4 operator -(fp4 lhs, fp rhs) {
            return new fp4(lhs.x - rhs, lhs.y - rhs, lhs.z - rhs, lhs.w - rhs);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp4 operator -(fp lhs, fp4 rhs) {
            return new fp4(lhs - rhs.x, lhs - rhs.y, lhs - rhs.z, lhs - rhs.w);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp4 operator /(fp4 lhs, fp4 rhs) {
            return new fp4(lhs.x / rhs.x, lhs.y / rhs.y, lhs.z / rhs.z, lhs.w / rhs.w);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp4 operator /(fp4 lhs, fp rhs) {
            return new fp4(lhs.x / rhs, lhs.y / rhs, lhs.z / rhs, lhs.w / rhs);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp4 operator /(fp lhs, fp4 rhs) {
            return new fp4(lhs / rhs.x, lhs / rhs.y, lhs / rhs.z, lhs / rhs.w);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp4 operator %(fp4 lhs, fp4 rhs) {
            return new fp4(lhs.x % rhs.x, lhs.y % rhs.y, lhs.z % rhs.z, lhs.w % rhs.w);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp4 operator %(fp4 lhs, fp rhs) {
            return new fp4(lhs.x % rhs, lhs.y % rhs, lhs.z % rhs, lhs.w % rhs);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp4 operator %(fp lhs, fp4 rhs) {
            return new fp4(lhs % rhs.x, lhs % rhs.y, lhs % rhs.z, lhs % rhs.w);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(fp4 a, fp4 b) {
            return a.x.value == b.x.value && a.y.value == b.y.value && a.z.value == b.z.value && a.w.value == b.w.value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(fp4 a, fp4 b) {
            return a.x.value != b.x.value || a.y.value != b.y.value || a.z.value != b.z.value || a.w.value != b.w.value;
        }

        public bool Equals(fp4 other) {
            return x.Equals(other.x) && y.Equals(other.y) && z.Equals(other.z) && w.Equals(other.w);
        }

        public override bool Equals(object obj) {
            return obj is fp4 other && Equals(other);
        }

        public override int GetHashCode() {
            unchecked {
                var hashCode = x.GetHashCode();
                hashCode = (hashCode * 397) ^ y.GetHashCode();
                hashCode = (hashCode * 397) ^ z.GetHashCode();
                hashCode = (hashCode * 397) ^ w.GetHashCode();
                return hashCode;
            }
        }
    }
}