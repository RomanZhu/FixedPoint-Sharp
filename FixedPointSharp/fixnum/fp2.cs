using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace FixedPoint
{
    [Serializable]
    public struct fp2 : IEquatable<fp2>
    {
        public class EqualityComparer : IEqualityComparer<fp2>
        {
            public static readonly EqualityComparer instance = new EqualityComparer();

            EqualityComparer()
            {
            }

            bool IEqualityComparer<fp2>.Equals(fp2 x, fp2 y)
            {
                return x == y;
            }

            int IEqualityComparer<fp2>.GetHashCode(fp2 obj)
            {
                return obj.GetHashCode();
            }
        }

        public static readonly fp2 left  = new fp2(-fp.one, fp.zero);
        public static readonly fp2 right = new fp2(+fp.one, fp.zero);
        public static readonly fp2 up    = new fp2(fp.zero, +fp.one);
        public static readonly fp2 down  = new fp2(fp.zero, -fp.one);
        public static readonly fp2 one   = new fp2(fp.one,  fp.one);
        public static readonly fp2 zero  = new fp2(fp.zero, fp.zero);

        public fp x;
        public fp y;

        public fp2(fp x, fp y)
        {
            this.x.value = x.value;
            this.y.value = y.value;
        }

        public static fp2 X(fp x)
        {
            fp2 r;

            r.x.value = x.value;
            r.y.value = 0;

            return r;
        }

        public static fp2 Y(fp y)
        {
            fp2 r;

            r.x.value = 0;
            r.y.value = y.value;

            return r;
        }

        public static fp2 operator +(fp2 a, fp2 b)
        {
            fp2 r;

            r.x.value = a.x.value + b.x.value;
            r.y.value = a.y.value + b.y.value;

            return r;
        }

        public static fp2 operator -(fp2 a, fp2 b)
        {
            fp2 r;

            r.x.value = a.x.value - b.x.value;
            r.y.value = a.y.value - b.y.value;

            return r;
        }

        public static fp2 operator -(fp2 a)
        {
            a.x.value = -a.x.value;
            a.y.value = -a.y.value;

            return a;
        }

        public static fp2 operator *(fp2 a, fp b)
        {
            fp2 r;

            r.x.value = (a.x.value * b.value) >> fixlut.PRECISION;
            r.y.value = (a.y.value * b.value) >> fixlut.PRECISION;

            return r;
        }

        public static fp2 operator *(fp b, fp2 a)
        {
            fp2 r;

            r.x.value = (a.x.value * b.value) >> fixlut.PRECISION;
            r.y.value = (a.y.value * b.value) >> fixlut.PRECISION;

            return r;
        }

        public static fp2 operator /(fp2 a, fp b)
        {
            fp2 r;

            r.x.value = (a.x.value << fixlut.PRECISION) / b.value;
            r.y.value = (a.y.value << fixlut.PRECISION) / b.value;

            return r;
        }

        public static fp2 operator /(fp b, fp2 a)
        {
            fp2 r;

            r.x.value = (a.x.value << fixlut.PRECISION) / b.value;
            r.y.value = (a.y.value << fixlut.PRECISION) / b.value;

            return r;
        }

        public static bool operator ==(fp2 a, fp2 b)
        {
            return a.x.value == b.x.value && a.y.value == b.y.value;
        }

        public static bool operator !=(fp2 a, fp2 b)
        {
            return a.x.value != b.x.value || a.y.value != b.y.value;
        }

        public override bool Equals(object obj)
        {
            return obj is fp2 other && this == other;
        }

        public bool Equals(fp2 other)
        {
            return this == other;
        }
        
        public override int GetHashCode()
        {
            return x.GetHashCode() ^ x.GetHashCode() << 2 ^ y.GetHashCode() >> 2;
        }

        public override string ToString()
        {
            return $"({x}, {y})";
        }
    }
}