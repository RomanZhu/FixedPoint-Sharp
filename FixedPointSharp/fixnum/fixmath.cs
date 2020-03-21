using System;
using System.Runtime.CompilerServices;

namespace FixedPoint
{
    public partial struct fixmath
    {
        /// <param name="num">Angle in radians</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp Sin(fp num) {
            num.value %= fp.pi2.value;
            num /= fp.pi2;
            return new fp(fixlut.sin(num.value));
        }
        
        /// <param name="num">Angle in radians</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp Cos(fp num) {
            num.value %= fp.pi2.value;
            num       /= fp.pi2;
            return new fp(fixlut.cos(num.value));
        }

        /// <param name="num">Angle in radians</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp Acos(fp num) {
            return new fp(fixlut.acos(num.value));
        }
        
        /// <param name="num">Angle in radians</param>
        public static void SinCos(fp num, out fp sin, out fp cos) {
            num.value %= fp.pi2.value;
            num       /= fp.pi2;
            fixlut.sin_cos(num.value, out var sinVal, out var cosVal);
            sin = fp.ParseRaw(sinVal);
            cos = fp.ParseRaw(cosVal);
        }
        
        /// <param name="num">Angle in radians</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SinCosTan(fp num, out fp sin, out fp cos, out fp tan) {
            num.value %= fp.pi2.value;
            num       /= fp.pi2;
            fixlut.sin_cos_tan(num.value, out var sinVal, out var cosVal, out var tanVal);
            sin = fp.ParseRaw(sinVal);
            cos = fp.ParseRaw(cosVal);
            tan = fp.ParseRaw(tanVal);
        }

        //WORKS FOR VALUES LESS THAN 512000
        /// <summary>
        /// Trims fraction of value and uses lookup table to find the result
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp SqrtAprox(fp num)
        {
            var sign = fp.one;
            if (num < fp.zero)
            {
                sign =  fp.minus_one;
                num  *= fp.minus_one;
            }

            return new fp(fixlut.sqrt_aprox(num.AsLong)) * sign;
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
            return fp.Parse(num.AsLong);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp Ceil(fp num)
        {
            var fractions = num.value & 0x000000000000FFFFL;

            if (fractions == 0)
            {
                return num;
            }

            var full = new fp(num.value >> fixlut.PRECISION << fixlut.PRECISION);

            return full + fp.one;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp Fractions(fp num)
        {
            return new fp(num.value & 0x000000000000FFFFL);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int RoundToInt(fp num)
        {
            var fraction = new fp(num.value & 0x000000000000FFFFL);

            if (fraction.value >= fp.half.value)
            {
                return num.AsInt + 1;
            }

            return num.AsInt;
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
        public static fp Abs(fp num) {
            var v = num.value;
            return new fp((v ^ (v >> 63)) - (v >> 63));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp Abs2(fp num) {
            return new fp(num.value < 0 ? -num.value : num.value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp Abs3(fp num) {
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
        public static fp Clamp01(fp num)
        {
            if (num.value < 0)
            {
                return fp.zero;
            }

            return num.value > fp.one.value ? fp.one : num;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp Lerp(fp from, fp to, fp t) {
            t = Clamp01(t);
            return from + (to - from) * t;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp LerpUnclamped(fp from, fp to, fp t)
        {
            return from + (to - from) * t;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp Sign(fp num)
        {
            return num.value < fixlut.ZERO ? fp.minus_one : fp.one;
        }

        public static bool IsOppositeSign(fp a, fp b) {
            return ((a.value ^ b.value) < 0);
        }
    }
}