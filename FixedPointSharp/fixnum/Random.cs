using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace FixedPoint {
    [StructLayout(LayoutKind.Explicit)]
    public struct Random {
        public const int SIZE = 4;

        [FieldOffset(0)]
        public uint state;
        
        /// <summary>
        /// Seed must be non-zero
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Random(uint seed)
        {
            state = seed;
            NextState();
        }

        /// <summary>
        /// Seed must be non-zero
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetState(uint seed)
        {
            state = seed;
            NextState();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private uint NextState() {
            var t  = state;
            state ^= state << 13;
            state ^= state >> 17;
            state ^= state << 5;
            return t;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool NextBool()
        {
            return (NextState() & 1) == 1;
        }

        /// <summary>Returns value in range [-2147483647, 2147483647]</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int NextInt()
        {
            return (int)NextState() ^ -2147483648;
        }
        
        /// <summary>Returns value in range [0, max]</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int NextInt(int max)
        {
            return (int)((NextState() * (ulong)max) >> 32);
        }
        
        /// <summary>Returns value in range [min, max].</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int NextInt(int min, int max)
        {
            var range = (uint)(max - min);
            return (int)(NextState() * (ulong)range >> 32) + min;
        }
        
        /// <summary>Returns value in range [0, 1]</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public fp NextFp() {
            return new fp(NextInt(0, 65535));
        }
        
        /// <summary>Returns vector with all components in range [0, 1]</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public fp2 NextFp2() {
            return new fp2(NextFp(), NextFp());
        }
        
        /// <summary>Returns vector with all components in range [0, 1]</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public fp3 NextFp3() {
            return new fp3(NextFp(), NextFp(), NextFp());
        }
        
        /// <summary>Returns vector with all components in range [0, 1]</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public fp4 NextFp4() {
            return new fp4(NextFp(), NextFp(), NextFp(), NextFp());
        }


        /// <summary>Returns value in range [0, max]</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public fp NextFp(fp max) {
            return NextFp() * max;
        }
        
        /// <summary>Returns vector with all components in range [0, max]</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public fp2 NextFp2(fp2 max) {
            return NextFp2() * max;
        }
        
        /// <summary>Returns vector with all components in range [0, max]</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public fp3 NextFp3(fp3 max) {
            return NextFp3() * max;
        }
        
        /// <summary>Returns vector with all components in range [0, max]</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public fp4 NextFp4(fp4 max) {
            return NextFp4() * max;
        }

        /// <summary>Returns value in range [min, max]</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public fp NextFp(fp min, fp max) {
            return NextFp() * (max - min) + min;
        }

        /// <summary>Returns vector with all components in range [min, max]</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public fp2 NextFp2(fp2 min, fp2 max) {
            return NextFp2() * (max - min) + min;
        }
        
        /// <summary>Returns vector with all components in range [min, max]</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public fp3 NextFp3(fp3 min, fp3 max) {
            return NextFp3() * (max - min) + min;
        }
        
        /// <summary>Returns vector with all components in range [min, max]</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public fp4 NextFp4(fp4 min, fp4 max) {
            return NextFp4() * (max - min) + min;
        }
        
        /// <summary>Returns a normalized 2D direction</summary>
        public fp2 NextDirection2D() {
            var angle = NextFp() * fp.pi * fp._2;
            fixmath.SinCos(angle, out var sin, out var cos);
            return new fp2(sin,cos);
        }
        
        /// <summary>Returns a normalized 3D direction</summary>
        public fp3 NextDirection3D() {
            var z = NextFp(fp._2) - fp._1;
            var r = fixmath.Sqrt(fixmath.Max(fp._1 - z * z, fp._0));
            var angle = NextFp(fp.pi2);
            fixmath.SinCos(angle, out var sin, out var cos);
            return new fp3(cos * r, sin * r, z);
        }
    }
}