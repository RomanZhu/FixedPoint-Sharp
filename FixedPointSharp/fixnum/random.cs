using System.Runtime.CompilerServices;

namespace FixedPoint {
    public struct random {
        public uint state;
        
        /// <summary>
        /// Seed must be non-zero
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public random(uint seed)
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
            return fp.ParseRaw(NextInt(0, 65535));
        }

        /// <summary>Returns value in range [0, max]</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public fp NextFp(fp max) {
            return NextFp() * max;
        }
        
        /// <summary>Returns value in range [min, max]</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public fp NextFp(fp min, fp max) {
            return NextFp() * (max - min) + min;
        }
    }
}