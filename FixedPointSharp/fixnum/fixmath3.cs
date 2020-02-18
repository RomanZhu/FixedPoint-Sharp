using System.Runtime.CompilerServices;

namespace FixedPoint
{
    public partial struct fixmath
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp3 Abs(fp3 v)
        {
            return new fp3(Abs(v.x), Abs(v.y), Abs(v.z));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp Dot(fp3 a, fp3 b)
        {
            var x = ((a.x.value * b.x.value) >> fixlut.PRECISION);
            var y = ((a.y.value * b.y.value) >> fixlut.PRECISION);
            var z = ((a.z.value * b.z.value) >> fixlut.PRECISION);

            fp r;

            r.value = x + y + z;

            return r;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp3 Cross(fp3 a, fp3 b)
        {
            fp3 r;

            r.x = a.y * b.z - a.z * b.y;
            r.y = a.z * b.x - a.x * b.z;
            r.z = a.x * b.y - a.y * b.x;
            
            return r;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp Distance(fp3 p1, fp3 p2)
        {
            fp3 v;
            v.x.value = p1.x.value - p2.x.value;
            v.y.value = p1.y.value - p2.y.value;
            v.z.value = p1.z.value - p2.z.value;

            return Magnitude(v);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp DistanceSqr(fp3 p1, fp3 p2)
        {
            fp3 v;
            v.x.value = p1.x.value - p2.x.value;
            v.y.value = p1.y.value - p2.y.value;
            v.z.value = p1.z.value - p2.z.value;

            return MagnitudeSqr(v);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp3 Lerp(fp3 from, fp3 to, fp t)
        {
            t = Clamp(t, fp.zero, fp.one);
            
            return new fp3(Lerp(from.x, to.x, t), Lerp(from.y, to.y, t), Lerp(from.z, to.z, t));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp3 LerpUnclamped(fp3 from, fp3 to, fp t)
        {
            return new fp3(Lerp(from.x, to.x, t), Lerp(from.y, to.y, t), Lerp(from.z, to.z, t));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp3 Reflect(fp3 vector, fp3 normal)
        {
            var num = -fp._2 * Dot(normal, vector);
            return new fp3(num * normal.x + vector.x, num * normal.y + vector.y, num * normal.z + vector.z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp3 Project(fp3 vector, fp3 normal)
        {
            var sqrMag = MagnitudeSqr(normal);
            if (sqrMag < fp.epsilon)
                return fp3.zero;

            var dot = Dot(vector, normal);
            return normal * dot / sqrMag;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp3 ProjectOnPlane(fp3 vector, fp3 planeNormal)
        {
            var sqrMag = MagnitudeSqr(planeNormal);
            if (sqrMag < fp.epsilon)
                return vector;

            var dot = Dot(vector, planeNormal);
            return vector - planeNormal * dot / sqrMag;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp3 MoveTowards(fp3 current, fp3 target, fp maxDelta)
        {
            var v = target - current;
            var sqrMagnitude = MagnitudeSqr(v);
            if (v == fp3.zero || maxDelta >= fp.zero && sqrMagnitude <= maxDelta * maxDelta)
                return target;

            var magnitude = Sqrt(sqrMagnitude);
            return current + v / magnitude * maxDelta;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp Angle(fp3 a, fp3 b)
        {
            var sqr = MagnitudeSqr(a) * MagnitudeSqr(b);
            var n = Sqrt(sqr);

            if (n < fp.epsilon)
            {
                return fp.zero;
            }

            return Acos(Clamp(Dot(a, b) / n, fp.minus_one, fp.one)) * fp.rad2deg;
        }

        public static fp AngleSigned(fp3 a, fp3 b, fp3 axis)
        {
            var angle = Angle(a, b);
            long num2 = ((a.y.value * b.z.value) >> fixlut.PRECISION) - ((a.z.value * b.y.value) >> fixlut.PRECISION);
            long num3 = ((a.z.value * b.x.value) >> fixlut.PRECISION) - ((a.x.value * b.z.value) >> fixlut.PRECISION);
            long num4 = ((a.x.value * b.y.value) >> fixlut.PRECISION) - ((a.y.value * b.x.value) >> fixlut.PRECISION);
            var sign = (((axis.x.value * num2) >> fixlut.PRECISION) +
                        ((axis.y.value * num3) >> fixlut.PRECISION) +
                        ((axis.z.value * num4) >> fixlut.PRECISION)) < 0
                ? fp.minus_one
                : fp.one;
            return angle * sign;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp Radians(fp3 a, fp3 b)
        {
            return Acos(Clamp(Dot(Normalize(a), Normalize(b)), fp.minus_one, fp.one));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp RadiansSkipNormalize(fp3 a, fp3 b)
        {
            return Acos(Clamp(Dot(a, b), fp.minus_one, fp.one));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp MagnitudeSqr(fp3 v)
        {
            fp r;

            r.value =
                ((v.x.value * v.x.value) >> fixlut.PRECISION) +
                ((v.y.value * v.y.value) >> fixlut.PRECISION) +
                ((v.z.value * v.z.value) >> fixlut.PRECISION);

            return r;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp Magnitude(fp3 v)
        {
            fp r;

            r.value =
                ((v.x.value * v.x.value) >> fixlut.PRECISION) +
                ((v.y.value * v.y.value) >> fixlut.PRECISION) +
                ((v.z.value * v.z.value) >> fixlut.PRECISION);

            fp r1;

            //manually inlined Sqrt()
            if (r.value == 0)
            {
                r1.value = 0;
            }
            else
            {
                var b = (r.value >> 1) + 1L;
                var c = (b + (r.value / b)) >> 1;

                while (c < b)
                {
                    b = c;
                    c = (b + (r.value / b)) >> 1;
                }

                r1.value = b << (fixlut.PRECISION >> 1);
            }

            r = r1;

            return r;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp3 MagnitudeClamp(fp3 v, fp length)
        {
            var sqrMagnitude = MagnitudeSqr(v);
            if (sqrMagnitude <= length * length)
                return v;

            var magnitude = Sqrt(sqrMagnitude);
            var normalized = v / magnitude;
            return normalized * length;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp3 MagnitudeSet(fp3 v, fp length)
        {
            return Normalize(v) * length;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp3 Min(fp3 a, fp3 b)
        {
            return new fp3(Min(a.x, b.x), Min(a.y, b.y), Min(a.z, b.z));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp3 Max(fp3 a, fp3 b)
        {
            return new fp3(Max(a.x, b.x), Max(a.y, b.y), Max(a.z, b.z));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp3 Normalize(fp3 v)
        {
            if (v == fp3.zero)
                return fp3.zero;

            var magnitude = Magnitude(v);

            return v / magnitude;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp3 Normalize(fp3 v, out fp magnitude)
        {
            if (v == fp3.zero)
            {
                magnitude = fp.zero;
                return fp3.zero;
            }

            magnitude = Magnitude(v);
            return v / magnitude;
        }
    }
}