using System;
using System.Runtime.CompilerServices;

namespace ffg
{
    public struct fixmath
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp Cos(fp num)
        {
            var sign = fp.one;
            if (num > fp.pi || num < -fp.pi)
                sign = fp.minus_one;

            return fm.Cos(num) * sign;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp Sin(fp num)
        {
            var sign = fp.one;
            if (num > fp.pi || num < -fp.pi)
                sign = fp.minus_one;

            return fm.Sin(num) * sign;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp Tan(fp num)
        {
            var sign = fp.one;
            if (num > fp.pi || num < -fp.pi)
                sign = fp.minus_one;

            return fm.Tan(num) * sign;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp Acos(fp num)
        {
            return fm.Acos(num);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp Asin(fp num)
        {
            return fm.Asin(num);
        }

        //PROBABLY WRONG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp Atan(fp num)
        {
            return fm.Atan(num);
        }

        //WORKS FOR VALUES LESS THAN 512000
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp SqrtAprox(fp num)
        {
            var sign = fp.one;
            if (num < fp.zero)
            {
                sign =  fp.minus_one;
                num  *= fp.minus_one;
            }

            return fm.SqrtAprox(num) * sign;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp Sqrt(fp num)
        {
            return fm.Sqrt(num);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp Floor(fp num)
        {
            return fm.Floor(num);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp Ceil(fp num)
        {
            return fm.Ceil(num);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp Fractions(fp num)
        {
            return fm.Fractions(num);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int RountToInt(fp num)
        {
            return fm.RoundToInt(num);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp Min(fp a, fp b)
        {
            return fm.Min(a, b);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp2 Min(fp2 a, fp2 b)
        {
            return fm.Min(a, b);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp Max(fp a, fp b)
        {
            return fm.Max(a, b);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp2 Max(fp2 a, fp2 b)
        {
            return fm.Max(a, b);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp Dot(fp2 a, fp2 b)
        {
            return fm.Dot(a, b);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp Cross(fp2 a, fp2 b)
        {
            return fm.Cross(a, b);
        }        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp2 Cross(fp2 a, fp s)
        {
            return new fp2(s * a.y, -s * a.x);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp2 Cross(fp s, fp2 a)
        {
            return new fp2(-s * a.y, s * a.x);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp Abs(fp num)
        {
            return fm.Abs(num);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp Clamp(fp num, fp min, fp max)
        {
            return fm.Clamp(num, min, max);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp2 Clamp(fp2 num, fp2 min, fp2 max)
        {
            return fm.Clamp(num, min, max);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp2 ClampMagnitude(fp2 v, fp length)
        {
            return fm.ClampMagnitude(v, length);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp Magnitude(fp2 v)
        {
            return fm.Magnitude(v);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp MagnitudeSqr(fp2 v)
        {
            return fm.SqrMagnitude(v);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp Distance(fp2 a, fp2 b)
        {
            return fm.Distance(a, b);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp DistanceSqr(fp2 a, fp2 b)
        {
            return fm.DistanceSqr(a, b);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp2 Normalize(fp2 v)
        {
            return fm.Normalize(v);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp Lerp(fp from, fp to, fp t)
        {
            return fm.Lerp(from, to, t);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp2 Lerp(fp2 from, fp2 to, fp t)
        {
            return fm.Lerp(from, to, t);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp Angle(fp2 a, fp2 b)
        {
            return fm.Angle(a, b);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp Radians(fp2 a, fp2 b)
        {
            return fm.Radians(a, b);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp RadiansSigned(fp2 a, fp2 b)
        {
            return fm.RadiansSigned(a, b);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp RadiansSkipNormalize(fp2 a, fp2 b)
        {
            return fm.RadiansSkipNormalize(a, b);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp RadiansSignedSkipNormalize(fp2 a, fp2 b)
        {
            return fm.RadiansSignedSkipNormalize(a, b);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp2 Reflect(fp2 vector, fp2 normal)
        {
            return fm.Reflect(vector, normal);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp2 Rotate(fp2 vector, fp angle)
        {
            return fm.Rotate(vector, angle);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp2 ClosestPointOnCicle(fp2 center, fp radius, fp2 point)
        {
            return fm.ClosestPointOnCicle(center, radius, point);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp2 TriangleCenter(fp2 v0, fp2 v1, fp2 v2)
        {
            return fm.TriangleCenter(v0, v1, v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool LinesIntersect(fp2 p, fp2 p2, fp2 q, fp2 q2, out fp2 intersection)
        {
            return fm.LinesIntersect(p, p2, q, q2, out intersection);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool LinesIntersectFast(ref fp2 pt, ref fp2 p2, ref fp2 q, ref fp2 q2)
        {
            return fm.LinesIntersectFast(ref pt, ref p2, ref q, ref q2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool PointInTriangleFast(ref fp2 pt, ref fp2 p0, ref fp2 p1, ref fp2 p2)
        {
            return fm.PointInTriangleFast(ref pt, ref p0, ref p1, ref p2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool LinesIntersectFastWithIntersection(ref fp2 p, ref fp2 p2, ref fp2 q, ref fp2 q2,
            out                                                   fp2 intersection)
        {
            return fm.LinesIntersectFastWithIntersection(ref p, ref p2, ref q, ref q2,
                out intersection);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool LinecastCircleFast(ref fp2 point0, ref fp2 point1, ref fp2 circleCenter, ref fp circleRadius,
            out                                   fp2 point)
        {
            return fm.linecast_circle_fast(ref point0, ref point1, ref circleCenter, ref circleRadius, out point);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool CirclesOverlap(fp2 aOrigin, fp aRadius, fp2 bOrigin, fp bRadius)
        {
            return fm.CirclesOverlap(ref aOrigin, ref aRadius, ref bOrigin, ref bRadius);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RectangleMinMax(fp2 center, fp2 size, out fp2 min, out fp2 max)
        {
            fm.RectangleMinMax(center, size, out min, out max);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AABBsIntersects(fp2 aMin, fp2 aMax, fp2 bMin, fp2 bMax)
        {
            if (aMax.x < bMin.x || aMin.x > bMax.x) return false;
            if (aMax.y < bMin.y || aMin.y > bMax.y) return false;

            return true;
        }
    }

    internal struct fm
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static fp Sqrt(fp num)
        {
            fp r;

            r.value = Sqrt_Raw(num.value);

            return r;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static long Sqrt_Raw(long a)
        {
            if (a == 0)
            {
                return 0;
            }

            var b = (a >> 1) + 1L;
            var c = (b + (a / b)) >> 1;

            while (c < b)
            {
                b = c;
                c = (b + (a / b)) >> 1;
            }

            return b << (fixlut.PRECISION >> 1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static fp Floor(fp num)
        {
            return fp.parse_int(num.as_long_int);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static fp Ceil(fp num)
        {
            var fractions = Fractions(num);

            if (fractions.value == 0)
            {
                return num;
            }

            return num + fp.one;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static fp Fractions(fp num)
        {
            return fp.raw_unsafe_int(num.value & 0x000000000000FFFFL);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static int RoundToInt(fp num)
        {
            var fraction = Fractions(num);

            if (fraction.value >= fp.half.value)
            {
                return num.as_int_int + 1;
            }

            return num.as_int_int;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static fp Sin(fp num)
        {
            return new fp(fixlut.sin(num.value));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static fp Cos(fp num)
        {
            return new fp(fixlut.cos(num.value));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static fp Tan(fp num)
        {
            return new fp(fixlut.tan(num.value));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static fp Asin(fp num)
        {
            return new fp(fixlut.asin(num.value));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static fp Acos(fp num)
        {
            return new fp(fixlut.acos(num.value));
        }

        // PROBABLY NOT CORRECT
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static fp Atan(fp num)
        {
            return new fp(fixlut.atan(num.value));
        }

        //WORKS FOR VALUES LESS THAN 512000
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static fp SqrtAprox(fp num)
        {
            return new fp(fixlut.sqrt_aprox(num.as_long_int));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static fp Clamp(fp num, fp min, fp max)
        {
            if (num.value < min.value)
            {
                return min;
            }

            if (num.value > max.value)
            {
                return max;
            }

            return num;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static fp Clamp01(fp num)
        {
            return Clamp(num, fp.zero, fp.one);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static fp Min(fp a, fp b)
        {
            if (a.value < b.value)
            {
                return a;
            }

            return b;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static fp Max(fp a, fp b)
        {
            if (a.value > b.value)
            {
                return a;
            }

            return b;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static fp Abs(fp num)
        {
            return new fp(Math.Abs(num.value));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static fp Lerp(fp from, fp to, fp t)
        {
            return from + ((to - from) * Clamp01(t));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static fp Sign(fp num)
        {
            return (num.value < fp.RAW_ZERO) ? fp.minus_one : fp.one;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static bool CirclesOverlap(ref fp2 aOrigin, ref fp aRadius, ref fp2 bOrigin,
            ref                                 fp  bRadius)
        {
            var radsum = aRadius.value + bRadius.value;
            return DistanceSqr(ref aOrigin, ref bOrigin).value <= ((radsum * radsum) >> fixlut.PRECISION);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static fp Dot(fp2 a, fp2 b)
        {
            return Dot(ref a, ref b);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static fp Dot(ref fp2 a, ref fp2 b)
        {
            var x = ((a.x.value * b.x.value) >> fixlut.PRECISION);
            var z = ((a.y.value * b.y.value) >> fixlut.PRECISION);

            fp r;

            r.value = x + z;

            return r;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static fp2 Min(fp2 a, fp2 b)
        {
            return new fp2(Min(a.x, b.x), Min(a.y, b.y));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static fp2 Max(fp2 a, fp2 b)
        {
            return new fp2(Max(a.x, b.x), Max(a.y, b.y));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static fp Magnitude(fp2 value)
        {
            return Sqrt(SqrMagnitude(value));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static fp SqrMagnitude(fp2 v)
        {
            return SqrMagnitude(ref v);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static fp SqrMagnitude(ref fp2 v)
        {
            fp r;

            r.value =
                ((v.x.value * v.x.value) >> fixlut.PRECISION) +
                ((v.y.value * v.y.value) >> fixlut.PRECISION);

            return r;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static fp DistanceSqr(fp2 a, fp2 b)
        {
            return DistanceSqr(ref a, ref b);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static fp DistanceSqr(ref fp2 a, ref fp2 b)
        {
            var x = a.x.value - b.x.value;
            var z = a.y.value - b.y.value;

            fp r;
            r.value = ((x * x) >> fixlut.PRECISION) + ((z * z) >> fixlut.PRECISION);
            return r;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static fp Distance(fp2 a, fp2 b)
        {
            fp2 v;

            v.x.value = a.x.value - b.x.value;
            v.y.value = a.y.value - b.y.value;

            return Magnitude(v);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static fp2 Clamp(fp2 value, fp2 min, fp2 max)
        {
            fp2 r;

            r.x = Clamp(value.x, min.x, max.x);
            r.y = Clamp(value.y, min.y, max.y);

            return r;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static fp2 ClampMagnitude(fp2 value, fp length)
        {
            ClampMagnitude(ref value, ref length);
            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void ClampMagnitude(ref fp2 value, ref fp length)
        {
            if (SqrMagnitude(value).value <= ((length.value * length.value) >> fixlut.PRECISION))
            {
                return;
            }

            value = Normalize(value) * length;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static fp Radians(fp2 from, fp2 to)
        {
            var dot = Dot(Normalize(from), Normalize(to));
            return Acos(Clamp(dot, -fp.one, +fp.one));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static fp RadiansSkipNormalize(fp2 from, fp2 to)
        {
            var dot = Dot(from, to);
            return Acos(Clamp(dot, -fp.one, +fp.one));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static fp2 TriangleCenter(fp2 v0, fp2 v1, fp2 v2)
        {
            return Lerp(Lerp(v0, v1, fp.half), v2, fp.half);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static fp RadiansSigned(fp2 v1, fp2 v2)
        {
            var rad  = Radians(v1, v2);
            var sign = Sign(v1.x * v2.y - v1.y * v2.x);

            return rad * sign;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static fp RadiansSignedSkipNormalize(fp2 v1, fp2 v2)
        {
            var rad  = RadiansSkipNormalize(v1, v2);
            var sign = Sign(v1.x * v2.y - v1.y * v2.x);

            return rad * sign;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static fp Angle(fp2 from, fp2 to)
        {
            return Radians(from, to) * fp.rad2deg;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static fp2 Normalize(fp2 v)
        {
            Normalize(ref v);
            return v;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void Normalize(ref fp2 v)
        {
            fp m = default(fp);
            Normalize(ref v, ref m);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void Normalize(ref fp2 v, ref fp m)
        {
            m = Magnitude(v);

            if (m.value <= fp.epsilon.value)
            {
                v = default(fp2);
                return;
            }

            v.x.value = ((v.x.value << fixlut.PRECISION) / m.value);
            v.y.value = ((v.y.value << fixlut.PRECISION) / m.value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static fp2 Lerp(fp2 from, fp2 to, fp t)
        {
            t = Clamp01(t);
            return new fp2(from.x + (to.x - from.x) * t, from.y + (to.y - from.y) * t);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static fp2 Reflect(fp2 vector, fp2 normal)
        {
            fp dot = (vector.x * normal.x) + (vector.y * normal.y);

            fp2 result;

            result.x = vector.x - ((fp.two * dot) * normal.x);
            result.y = vector.y - ((fp.two * dot) * normal.y);

            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static fp2 Rotate(fp2 vector, fp rotation)
        {
            var cs = fixmath.Cos(rotation);
            var sn = fixmath.Sin(rotation);

            var px = (vector.x * cs) - (vector.y * sn);
            var pz = (vector.x * sn) + (vector.y * cs);

            vector.x = px;
            vector.y = pz;

            return vector;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static fp2 ClosestPointOnCicle(fp2 center, fp radius, fp2 point)
        {
            var v = fm.Normalize(point - center);

            v *= radius;
            v += center;

            return v;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void RectangleMinMax(fp2 center, fp2 size, out fp2 min, out fp2 max)
        {
            min.x = center.x - (size.x * fp.half);
            min.y = center.y - (size.y * fp.half);

            max.x = center.x + (size.x * fp.half);
            max.y = center.y + (size.y * fp.half);
        }

//
//    internal static bool linecast_collider_fast(fix3 point0, fix3 point1, ref collider2 collider) {
//      fix3 intersection;
//
//      switch (collider.type) {
//        case collider2_types.circle: return linecast_circle_fast_nopoint(ref point0, ref point1, ref collider.circle.center, ref collider.circle.radius);
//        case collider2_types.aabb: return linecast_aabb(new line { point0 = point0, point1 = point1 }, collider.aabb, out intersection);
//        case collider2_types.oobb: return linecast_oobb(new line { point0 = point0, point1 = point1 }, collider.oobb, out intersection);
//      }
//
//      throw new NotImplementedException(collider.type.ToString());
//    }
//
        internal static bool linecast_circle_fast(ref fp2 point0,        ref fp2 point1, ref fp2 circle_center,
            ref                                       fp  circle_radius, out fp2 point)
        {
            fp2 v;
            fp2 z;
            fp2 z_normalized;
            fp  z_magnitude;

            v.x.value = point0.x.value - circle_center.x.value;
            v.y.value = point0.y.value - circle_center.y.value;

            z.x.value = point1.x.value - point0.x.value;
            z.y.value = point1.y.value - point0.y.value;

            z_normalized = z;
            z_magnitude  = default(fp);

            Normalize(ref z_normalized, ref z_magnitude);

            var b = Dot(ref v, ref z_normalized).value;
            var c = Dot(ref v, ref v).value - ((circle_radius.value * circle_radius.value) >> fixlut.PRECISION);

            if (c > fp.RAW_ZERO && b > fp.RAW_ZERO)
            {
                goto MISS;
            }

            var d = ((b * b) >> fixlut.PRECISION) - c;
            if (d < fp.RAW_ZERO)
            {
                goto MISS;
            }

            var distance = -b - Sqrt_Raw(d);
            if (distance <= z_magnitude.value)
            {
                if (distance < fp.RAW_ZERO)
                {
                    point = point0;
                }
                else
                {
                    point         =  point0;
                    point.x.value += ((z_normalized.x.value * distance) >> fixlut.PRECISION);
                    point.y.value += ((z_normalized.y.value * distance) >> fixlut.PRECISION);
                }

                return true;
            }

            MISS:

            point = fp2.zero;
            return false;
        }

        internal static bool linecast_circle_fast_nopoint(ref fp2 point0, ref fp2 point1, ref fp2 circle_center,
            ref                                               fp  circle_radius)
        {
            fp2 v;
            fp2 z;
            fp2 z_normalized;
            fp  z_magnitude;

            v.x.value = point0.x.value - circle_center.x.value;
            v.y.value = point0.y.value - circle_center.y.value;

            z.x.value = point1.x.value - point0.x.value;
            z.y.value = point1.y.value - point0.y.value;

            z_normalized = z;
            z_magnitude  = default(fp);

            Normalize(ref z_normalized, ref z_magnitude);

            var b = Dot(ref v, ref z_normalized).value;
            var c = Dot(ref v, ref v).value - ((circle_radius.value * circle_radius.value) >> fixlut.PRECISION);

            if (c > fp.RAW_ZERO && b > fp.RAW_ZERO)
            {
                goto MISS;
            }

            var d = ((b * b) >> fixlut.PRECISION) - c;
            if (d < fp.RAW_ZERO)
            {
                goto MISS;
            }

            var distance = -b - Sqrt_Raw(d);
            if (distance <= z_magnitude.value)
            {
                return true;
            }

            MISS:
            return false;
        }
//
//    internal static bool linecast_aabb(line line, collider2.aabb_collider rect, out fix3 intersection) {
//      return linecast_oobb(line, collider2.create_oobb(rect.center, rect.extents, fixnum.zero).oobb, out intersection);
//    }
//
//    static fix3[] linecast_box_intersections = new fix3[4];
//
//    internal static bool linecast_oobb(line line, collider2.oobb_collider rect, out fix3 intersection) {
//      // this could be remove for perf reasons
//      Array.Clear(linecast_box_intersections, 0, linecast_box_intersections.Length);
//
//      // track how many intersections we get
//      int intersectionCount = 0;
//
//      // UL => UR
//      if (LinesIntersect(line.point0, line.point1, rect.ul, rect.ur, out linecast_box_intersections[intersectionCount])) {
//        ++intersectionCount;
//      }
//
//      // UR => LR
//      if (LinesIntersect(line.point0, line.point1, rect.ur, rect.lr, out linecast_box_intersections[intersectionCount])) {
//        ++intersectionCount;
//      }
//
//      // LR => LL
//      if (LinesIntersect(line.point0, line.point1, rect.lr, rect.ll, out linecast_box_intersections[intersectionCount])) {
//        ++intersectionCount;
//      }
//
//      // LL => UL
//      if (LinesIntersect(line.point0, line.point1, rect.ll, rect.ul, out linecast_box_intersections[intersectionCount])) {
//        ++intersectionCount;
//      }
//
//      if (intersectionCount == 0) {
//        intersection = fix3.zero;
//        return false;
//      }
//
//      int minIndex = -1;
//      var minDistance = fixnum.max;
//
//      for (int i = 0; i < intersectionCount; ++i) {
//        var intersectionDistance = DistanceSqr(linecast_box_intersections[i], line.point0);
//
//        if (intersectionDistance < minDistance) {
//          minDistance = intersectionDistance;
//          minIndex = i;
//        }
//      }
//
//      intersection = linecast_box_intersections[minIndex];
//      return true;
//    }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static fp Cross(fp2 a, fp2 b)
        {
            return (a.x * b.y) - (a.y * b.x);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static bool LinesIntersect(fp2 p, fp2 p2, fp2 q, fp2 q2, out fp2 intersection)
        {
            intersection = fp2.zero;

            var r    = p2 - p;
            var s    = q2 - q;
            var rxs  = Cross(r,     s);
            var qpxr = Cross(q - p, r);

            // If r x s = 0 and (q - p) x r = 0, then the two lines are collinear.
            if (rxs == fp.zero && qpxr == fp.zero)
            {
                // 1. If either  0 <= (q - p) * r <= r * r or 0 <= (p - q) * s <= * s
                // then the two lines are overlapping,

                //if (considerCollinearOverlapAsIntersect)
                //  if ((0 <= (q - p) * r && (q - p) * r <= r * r) || (0 <= (p - q) * s && (p - q) * s <= s * s))
                //    return true;

                // 2. If neither 0 <= (q - p) * r = r * r nor 0 <= (p - q) * s <= s * s
                // then the two lines are collinear but disjoint.
                // No need to implement this expression, as it follows from the expression above.
                return false;
            }

            // 3. If r x s = 0 and (q - p) x r != 0, then the two lines are parallel and non-intersecting.
            if (rxs == fp.zero && qpxr != fp.zero)
            {
                return false;
            }

            // t = (q - p) x s / (r x s)
            var t = Cross(q - p, s) / rxs;

            // u = (q - p) x r / (r x s)
            var u = Cross(q - p, r) / rxs;

            // 4. If r x s != 0 and 0 <= t <= 1 and 0 <= u <= 1
            // the two line segments meet at the point p + t r = q + u s.
            if (rxs != fp.zero && (fp.zero <= t && t <= fp.one) && (fp.zero <= u && u <= fp.one))
            {
                // We can calculate the intersection point using either t or u.
                intersection = p + t * r;

                // An intersection was found.
                return true;
            }

            // 5. Otherwise, the two line segments are not parallel but do not intersect.
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static long CrossFast(ref fp2 a, ref fp2 b)
        {
            return ((a.x.value * b.y.value) >> fixlut.PRECISION) - ((a.y.value * b.x.value) >> fixlut.PRECISION);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static bool LinesIntersectFast(ref fp2 p, ref fp2 p2, ref fp2 q, ref fp2 q2)
        {
            // manually inlined fix3 subtract

            var r = default(fp2);
            r.x.value = p2.x.value - p.x.value;
            r.y.value = p2.y.value - p.y.value;

            // manually inlined fix3 subtract

            var s = default(fp2);
            s.x.value = q2.x.value - q.x.value;
            s.y.value = q2.y.value - q.y.value;

            // manually inlined fix3 subtract

            var qp = default(fp2);
            qp.x.value = q.x.value - p.x.value;
            qp.y.value = q.y.value - p.y.value;

            // r crossed with s
            long r_x_s = CrossFast(ref r, ref s);

            // qp crossed with r
            long qp_x_r = CrossFast(ref qp, ref r);

            // r x s = 0 and (q - p) x r = 0, lines are collinear
            if (r_x_s == fp.RAW_ZERO && qp_x_r == fp.RAW_ZERO)
            {
                return false;
            }

            // r x s = 0 and (q - p) x r != 0, lines are parallel
            if (r_x_s == fp.RAW_ZERO && qp_x_r != fp.RAW_ZERO)
            {
                return false;
            }

            // t = (q - p) x s / (r x s)
            long t = (CrossFast(ref qp, ref s) << fixlut.PRECISION) / r_x_s;

            // u = (q - p) x r / (r x s)
            long u = (CrossFast(ref qp, ref r) << fixlut.PRECISION) / r_x_s;

            // r x s != 0 and 0 <= t <= 1 and 0 <= u <= 1
            return r_x_s != fp.RAW_ZERO && (fp.RAW_ZERO <= t && t <= fp.RAW_ONE) &&
                   (fp.RAW_ZERO <= u && u <= fp.RAW_ONE);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static bool LinesIntersectFastWithIntersection(ref fp2 p, ref fp2 p2, ref fp2 q, ref fp2 q2,
            out                                                     fp2 intersection)
        {
            // manually inlined fix3 subtract

            var r = default(fp2);
            r.x.value = p2.x.value - p.x.value;
            r.y.value = p2.y.value - p.y.value;

            // manually inlined fix3 subtract

            var s = default(fp2);
            s.x.value = q2.x.value - q.x.value;
            s.y.value = q2.y.value - q.y.value;

            // manually inlined fix3 subtract

            var qp = default(fp2);
            qp.x.value = q.x.value - p.x.value;
            qp.y.value = q.y.value - p.y.value;

            // r crossed with s
            long r_x_s = CrossFast(ref r, ref s);

            // qp crossed with r
            long qp_x_r = CrossFast(ref qp, ref r);

            // r x s = 0 and (q - p) x r = 0, lines are collinear
            if (r_x_s == fp.RAW_ZERO && qp_x_r == fp.RAW_ZERO)
            {
                intersection = default(fp2);
                return false;
            }

            // r x s = 0 and (q - p) x r != 0, lines are parallel
            if (r_x_s == fp.RAW_ZERO && qp_x_r != fp.RAW_ZERO)
            {
                intersection = default(fp2);
                return false;
            }

            // t = (q - p) x s / (r x s)
            long t = (CrossFast(ref qp, ref s) << fixlut.PRECISION) / r_x_s;

            // u = (q - p) x r / (r x s)
            long u = (CrossFast(ref qp, ref r) << fixlut.PRECISION) / r_x_s;

            // r x s != 0 and 0 <= t <= 1 and 0 <= u <= 1
            if (r_x_s != fp.RAW_ZERO && (fp.RAW_ZERO <= t && t <= fp.RAW_ONE) &&
                (fp.RAW_ZERO <= u && u <= fp.RAW_ONE))
            {
                // inlined fix3 * fixnum multiplication
                r.x.value = ((r.x.value * t) >> fixlut.PRECISION);
                r.y.value = ((r.y.value * t) >> fixlut.PRECISION);

                // inlined fix3 + fix3
                r.x.value += p.x.value;
                r.y.value += p.y.value;

                intersection = r;
                return true;
            }

            intersection = default(fp2);
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static long Sign(ref fp2 p0, ref fp2 p1, ref fp2 p2)
        {
            return (((p0.x.value - p2.x.value) * (p1.y.value - p2.y.value)) >> fixlut.PRECISION) -
                   (((p1.x.value - p2.x.value) * (p0.y.value - p2.y.value)) >> fixlut.PRECISION);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static bool PointInTriangleFast(ref fp2 pt, ref fp2 p0, ref fp2 p1, ref fp2 p2)
        {
            bool b1, b2, b3;

            b1 = Sign(ref pt, ref p0, ref p1) < fp.RAW_ZERO;
            b2 = Sign(ref pt, ref p1, ref p2) < fp.RAW_ZERO;
            b3 = Sign(ref pt, ref p2, ref p0) < fp.RAW_ZERO;

            return (b1 == b2) && (b2 == b3);
        }
    }
}