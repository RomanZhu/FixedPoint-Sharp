using System;
using System.Runtime.CompilerServices;

namespace ffg
{
    public partial struct fixmath
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp Cos(fp num)
        {
            var sign = fp.one;
            if (num > fp.pi || num < -fp.pi)
                sign = fp.minus_one;

            return new fp(fixlut.cos(num.value)) * sign;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp Sin(fp num)
        {
            var sign = fp.one;
            if (num > fp.pi || num < -fp.pi)
                sign = fp.minus_one;

            return new fp(fixlut.sin(num.value)) * sign;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp Tan(fp num)
        {
            var sign = fp.one;
            if (num > fp.pi || num < -fp.pi)
                sign = fp.minus_one;

            return new fp(fixlut.tan(num.value)) * sign;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp Acos(fp num)
        {
            return new fp(fixlut.acos(num.value));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp Asin(fp num)
        {
            return new fp(fixlut.asin(num.value));
        }

        //PROBABLY WRONG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp Atan(fp num)
        {
            return new fp(fixlut.atan(num.value));
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

            return new fp(fixlut.sqrt_aprox(num.as_long_int)) * sign;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp Sqrt(fp num)
        {
            fp r;

            if (num.value == 0)
            {
                r.value = 0;
            }
            else
            {
                var b = (num.value >> 1) + 1L;
                var c = (b + (num.value / b)) >> 1;

                while (c < b)
                {
                    b = c;
                    c = (b + (num.value / b)) >> 1;
                }

                r.value = b << (fixlut.PRECISION >> 1);
            }

            return r;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp Floor(fp num)
        {
            return fp.parse_int(num.as_long_int);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp Ceil(fp num)
        {
            var fractions = fp.raw_unsafe_int(num.value & 0x000000000000FFFFL);

            if (fractions.value == 0)
            {
                return num;
            }

            return num + fp.one;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp Fractions(fp num)
        {
            return fp.raw_unsafe_int(num.value & 0x000000000000FFFFL);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int RountToInt(fp num)
        {
            var fraction = fp.raw_unsafe_int(num.value & 0x000000000000FFFFL);

            if (fraction.value >= fp.half.value)
            {
                return num.as_int_int + 1;
            }

            return num.as_int_int;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp Min(fp a, fp b)
        {
            if (a.value < b.value)
            {
                return a;
            }

            return b;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp Max(fp a, fp b)
        {
            if (a.value > b.value)
            {
                return a;
            }

            return b;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp Abs(fp num)
        {
            return new fp(Math.Abs(num.value));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp Clamp(fp num, fp min, fp max)
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
        public static fp Lerp(fp from, fp to, fp t) {
            var ret = Clamp(t, fp.zero, fp.one);
            return from + ((to - from) * ret);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp LerpUnclamped(fp from, fp to, fp t) {
            return from + ((to - from) * t);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp Sign(fp num)
        {
            return num.value < fp.RAW_ZERO ? fp.minus_one : fp.one;
        }
    }
}