using System.Runtime.CompilerServices;

namespace FixedPoint {
    public struct fixmath3x3 {
        /// <summary>Return the float3x3 transpose of a float3x3 matrix.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp3x3 transpose(fp3x3 v)
        {
            return new fp3x3(
                v.c0.x, v.c0.y, v.c0.z,
                v.c1.x, v.c1.y, v.c1.z,
                v.c2.x, v.c2.y, v.c2.z);
        }
        
        public static fp3x3 inverse(fp3x3 m)
        {
            var c0 = m.c0;
            var c1 = m.c1;
            var c2 = m.c2;

            var t0 = new fp3(c1.x, c2.x, c0.x);
            var t1 = new fp3(c1.y, c2.y, c0.y);
            var t2 = new fp3(c1.z, c2.z, c0.z);

            var m0 = t1 * new fp3(t2.y, t2.z, t2.x) -new fp3(t1.y, t1.z, t1.x) * t2;
            var m1 = new fp3(t0.y, t0.z, t0.x) * t2 - t0 * new fp3(t2.y, t2.z, t2.x);
            var m2 = t0 * new fp3(t1.y, t1.z, t1.x) - new fp3(t0.y, t0.z, t0.x) * t1;
            
            var rcpDet = fp.one / fixmath.Sum(new fp3(t0.z, t0.x, t0.y) * m0);
            return new fp3x3(m0, m1, m2) * rcpDet;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp determinant(fp3x3 m)
        {
            var c0 = m.c0;
            var c1 = m.c1;
            var c2 = m.c2;

            var m00 = c1.y * c2.z - c1.z * c2.y;
            var m01 = c0.y * c2.z - c0.z * c2.y;
            var m02 = c0.y * c1.z - c0.z * c1.y;

            return c0.x * m00 - c1.x * m01 + c2.x * m02;
        }
    }
}