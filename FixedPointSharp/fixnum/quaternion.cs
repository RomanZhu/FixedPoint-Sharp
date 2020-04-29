using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace FixedPoint {
    [Serializable]
    [StructLayout(LayoutKind.Explicit)]
    public struct quaternion  : IEquatable<quaternion> {
        [FieldOffset(0)]
        public fp4 value;

        public static readonly quaternion identity = new quaternion(fp.zero, fp.zero, fp.zero, fp.one);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public quaternion(fp x, fp y, fp z, fp w) { value.x = x; value.y = y; value.z = z; value.w = w; }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public quaternion(fp4 value) { this.value = value; }
        
        public quaternion(fp3x3 m)
        {
            var u = m.c0;
            var v = m.c1;
            var w = m.c2;
            value = fp4.one;

            
            // uint  u_sign = (asuint(u.x) & 0x80000000);
            // var t      = v.y + asfloat(asuint(w.z) ^ u_sign);
            // uint4 u_mask = uint4((int)u_sign >> 31);
            // uint4 t_mask = uint4(asint(t) >> 31);
            //
            // var tr = fp.one + fixmath.Abs(u.x);
            //
            // uint4 sign_flips = uint4(0x00000000, 0x80000000, 0x80000000, 0x80000000) ^ (u_mask & uint4(0x00000000, 0x80000000, 0x00000000, 0x80000000)) ^ (t_mask & uint4(0x80000000, 0x80000000, 0x80000000, 0x00000000));
            //
            // value =new fp4(tr, u.y, w.x, v.z) + asfloat(asuint(new fp4(t, v.x, u.z, w.y)) ^ sign_flips); // +---, +++-, ++-+, +-++
            //
            // value = asfloat((asuint(value) & ~u_mask) | (asuint(value.zwxy) & u_mask));
            // value = asfloat((asuint(value.wzyx) & ~t_mask) | (asuint(value) & t_mask));
            // value = normalize(value);
        }
        
        public static quaternion AxisAngle(fp3 axis, fp angle)
        {
            fixmath.SinCos(fp._0_50 * angle, out var sin, out var cos);
            return new quaternion(new fp4(axis * sin, cos));
        }
        
        public static quaternion EulerXYZ(fp3 xyz)
        {
            // return mul(rotateZ(xyz.z), mul(rotateY(xyz.y), rotateX(xyz.x)));
            var angle = fp._0_50 * xyz;
            fixmath.SinCos(angle.x, out var xS, out var xC);
            fixmath.SinCos(angle.y, out var yS, out var yC);
            fixmath.SinCos(angle.z, out var zS, out var zC);
            return new quaternion(
                new fp4(xS, yS, zS, xC) * new fp4(yC, xC, xC, yC) * new fp4(zC, zC, yC, zC) +
                new fp4(yS, xS, xS, yS) * new fp4(zS, zS, yS, zS) * new fp4(xC, yC, zC, xS) * 
                new fp4(fp.minus_one, fp.one, fp.minus_one, fp.one)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static quaternion RotateX(fp angle)
        {
            fixmath.SinCos(fp._0_50 * angle, out var sina, out var cosa);
            return new quaternion(sina, fp.zero, fp.zero, cosa);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static quaternion RotateY(fp angle)
        {
            fixmath.SinCos(fp._0_50 * angle, out var sina, out var cosa);
            return new quaternion(fp.zero, sina, fp.zero, cosa);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static quaternion RotateZ(fp angle)
        {
            fixmath.SinCos(fp._0_50 * angle, out var sina, out var cosa);
            return new quaternion(fp.zero, fp.zero, sina, cosa);
        }
        
        public static quaternion LookRotation(fp3 forward, fp3 up) {
            var t = fixmath.Normalize(fixmath.Cross(up, forward));
            return new quaternion(new fp3x3(t, fixmath.Cross(forward, t), forward));
        }

        public bool Equals(quaternion other) {
            return value.x.Equals(other.value.x) && value.y.Equals(other.value.y) && value.z.Equals(other.value.z) && value.w.Equals(other.value.w);
        }

        public override bool Equals(object obj) {
            return obj is quaternion other && Equals(other);
        }

        public override int GetHashCode() {
            unchecked {
                var hashCode = value.x.GetHashCode();
                hashCode = (hashCode * 397) ^ value.y.GetHashCode();
                hashCode = (hashCode * 397) ^ value.z.GetHashCode();
                hashCode = (hashCode * 397) ^ value.w.GetHashCode();
                return hashCode;
            }
        }
    }
}