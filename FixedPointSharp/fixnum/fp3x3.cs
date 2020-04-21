using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace FixedPoint {
    [Serializable]
    [StructLayout(LayoutKind.Explicit)]
    public struct fp3x3 : IEquatable<fp3x3> {

        public static readonly fp3x3 identity = new fp3x3(fp.one, fp.zero, fp.zero, fp.zero, fp.one, fp.zero, fp.zero, fp.zero, fp.one);
        public static readonly fp3x3 zero;

        [FieldOffset(0)]
        public fp3 c0;
        [FieldOffset(sizeof(long)*3)]
        public fp3 c1;
        [FieldOffset(sizeof(long)*3*2)]
        public fp3 c2;
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public fp3x3(fp3 c0, fp3 c1, fp3 c2)
        { 
            this.c0 = c0;
            this.c1 = c1;
            this.c2 = c2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public fp3x3(fp m00, fp m01, fp m02, fp m10, fp m11, fp m12, fp m20, fp m21, fp m22)
        { 
            c0 = new fp3(m00, m10, m20);
            c1 = new fp3(m01, m11, m21);
            c2 = new fp3(m02, m12, m22);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp3x3 operator * (fp3x3 lhs, fp3x3 rhs) { return new fp3x3 (lhs.c0 * rhs.c0, lhs.c1 * rhs.c1, lhs.c2 * rhs.c2); }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp3x3 operator * (fp3x3 lhs, fp rhs) { return new fp3x3 (lhs.c0 * rhs, lhs.c1 * rhs, lhs.c2 * rhs); }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp3x3 operator * (fp lhs, fp3x3 rhs) { return new fp3x3 (lhs * rhs.c0, lhs * rhs.c1, lhs * rhs.c2); }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp3x3 operator + (fp3x3 lhs, fp3x3 rhs) { return new fp3x3 (lhs.c0 + rhs.c0, lhs.c1 + rhs.c1, lhs.c2 + rhs.c2); }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp3x3 operator - (fp3x3 lhs, fp3x3 rhs) { return new fp3x3 (lhs.c0 - rhs.c0, lhs.c1 - rhs.c1, lhs.c2 - rhs.c2); }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp3x3 operator / (fp3x3 lhs, fp3x3 rhs) { return new fp3x3 (lhs.c0 / rhs.c0, lhs.c1 / rhs.c1, lhs.c2 / rhs.c2); }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp3x3 operator / (fp3x3 lhs, fp rhs) { return new fp3x3 (lhs.c0 / rhs, lhs.c1 / rhs, lhs.c2 / rhs); }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp3x3 operator / (fp lhs, fp3x3 rhs) { return new fp3x3 (lhs / rhs.c0, lhs / rhs.c1, lhs / rhs.c2); }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp3x3 operator - (fp3x3 val) { return new fp3x3 (-val.c0, -val.c1, -val.c2); }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator == (fp3x3 lhs, fp3x3 rhs) { return lhs.c0 == rhs.c0 && lhs.c1 == rhs.c1 && lhs.c2 == rhs.c2; }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator != (fp3x3 lhs, fp3x3 rhs) { return lhs.c0 != rhs.c0 || lhs.c1 != rhs.c1 || lhs.c2 != rhs.c2; }

        public bool Equals(fp3x3 other) {
            return c0.Equals(other.c0) && c1.Equals(other.c1) && c2.Equals(other.c2);
        }

        public override bool Equals(object obj) {
            return obj is fp3x3 other && Equals(other);
        }

        public override int GetHashCode() {
            unchecked {
                var hashCode = c0.GetHashCode();
                hashCode = (hashCode * 397) ^ c1.GetHashCode();
                hashCode = (hashCode * 397) ^ c2.GetHashCode();
                return hashCode;
            }
        }

        public override string ToString() {
            return $"({c0.x}, {c0.y}, {c0.z}, {c1.x}, {c0.y}, {c0.z}, {c2.x}, {c2.y}, {c2.z})";
        }
        
        public class EqualityComparer : IEqualityComparer<fp3x3>
        {
            public static readonly EqualityComparer instance = new EqualityComparer();

            EqualityComparer()
            {
            }

            bool IEqualityComparer<fp3x3>.Equals(fp3x3 x, fp3x3 y)
            {
                return x == y;
            }

            int IEqualityComparer<fp3x3>.GetHashCode(fp3x3 obj)
            {
                return obj.GetHashCode();
            }
        }
    }
}