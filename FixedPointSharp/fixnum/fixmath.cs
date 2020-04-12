using System;
using System.Runtime.CompilerServices;

namespace FixedPoint {
    public partial struct fixmath {
        private static readonly fp _atan2Number1;
        private static readonly fp _atan2Number2;
        private static readonly fp _atan2Number3;
        private static readonly fp _atan2Number4;
        private static readonly fp _atan2Number5;
        private static readonly fp _atan2Number6;
        private static readonly fp _atan2Number7;
        private static readonly fp _atan2Number8;
        private static readonly fp _atan_2Number1;
        private static readonly fp _atan_2Number2;
        private static readonly fp _pow2Number1;
        private static readonly fp _expNumber1;

        static fixmath() {
            _expNumber1    = fp.ParseRaw(94548);
            _pow2Number1   = fp.ParseRaw(177);
            _atan_2Number1 = fp.ParseRaw(16036);
            _atan_2Number2 = fp.ParseRaw(4345);
            _atan2Number1  = fp.ParseRaw(-883);
            _atan2Number2  = fp.ParseRaw(3767);
            _atan2Number3  = fp.ParseRaw(7945);
            _atan2Number4  = fp.ParseRaw(12821);
            _atan2Number5  = fp.ParseRaw(21822);
            _atan2Number6  = fp.ParseRaw(65536);
            _atan2Number7  = fp.ParseRaw(102943);
            _atan2Number8  = fp.ParseRaw(205887);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp Pow2(fp num) {
            if (num.value > 1638400) {
                return fp.max;
            }

            var i = num.AsInt;
            num =  Fractions(num) * _pow2Number1 + fp.one;
            num *= num;
            num *= num;
            num *= num;
            num *= num;
            num *= num;
            num *= num;
            num *= num;
            return num * num * fp.Parse(1 << i);
        }

        ///Approximate version of Exp
        /// <param name="num">[0, 24]</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp Exp_2(fp num) {
            return Pow2(num * _expNumber1);
        }

        /// <param name="num">Angle in radians</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp Sin(fp num) {
            num.value %= fp.pi2.value;
            num       *= fp.one_div_pi2;
            return new fp(fixlut.sin(num.value));
        }

        /// <param name="num">Angle in radians</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp Cos(fp num) {
            num.value %= fp.pi2.value;
            num       *= fp.one_div_pi2;
            return new fp(fixlut.cos(num.value));
        }

        /// <param name="num">Angle in radians</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp Tan(fp num) {
            num.value %= fp.pi2.value;
            num       *= fp.one_div_pi2;
            return new fp(fixlut.tan(num.value));
        }

        /// <param name="num">Cos [-1, 1]</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp Acos(fp num) {
            num.value += fixlut.ONE;
            num       *= fp.half;
            return new fp(fixlut.acos(num.value));
        }

        /// <param name="num">Sin [-1, 1]</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp Asin(fp num) {
            num.value += fixlut.ONE;
            num       *= fp.half;
            return new fp(fixlut.asin(num.value));
        }

        /// <param name="num">Tan</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp Atan(fp num) {
            return Atan2(num, fp.one);
        }

        /// <param name="num">Tan [-1, 1]</param>
        /// Max error ~0.0015
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp Atan_2(fp num) {
            var absX = Abs(num);
            return fp.pi_div_4 * num - num * (absX - fp._1) * (_atan_2Number1 + _atan_2Number2 * absX);
        }

        /// <param name="x">Denominator</param>
        /// <param name="y">Numerator</param>
        public static fp Atan2(fp y, fp x) {
            var absX = Abs(x);
            var absY = Abs(y);
            var t3   = absX;
            var t1   = absY;
            var t0   = Max(t3, t1);
            t1 = Min(t3, t1);
            t3 = fp.one / t0;
            t3 = t1 * t3;
            var t4 = t3 * t3;
            t0 = _atan2Number1;
            t0 = t0 * t4 + _atan2Number2;
            t0 = t0 * t4 - _atan2Number3;
            t0 = t0 * t4 + _atan2Number4;
            t0 = t0 * t4 - _atan2Number5;
            t0 = t0 * t4 + _atan2Number6;
            t3 = t0 * t3;
            t3 = absY > absX ? _atan2Number7 - t3 : t3;
            t3 = x < fp.zero ? _atan2Number8 - t3 : t3;
            t3 = y < fp.zero ? -t3 : t3;
            return t3;
        }

        /// <param name="num">Angle in radians</param>
        public static void SinCos(fp num, out fp sin, out fp cos) {
            num.value %= fp.pi2.value;
            num       *= fp.one_div_pi2;
            fixlut.sin_cos(num.value, out var sinVal, out var cosVal);
            sin = fp.ParseRaw(sinVal);
            cos = fp.ParseRaw(cosVal);
        }

        /// <param name="num">Angle in radians</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SinCosTan(fp num, out fp sin, out fp cos, out fp tan) {
            num.value %= fp.pi2.value;
            num       *= fp.one_div_pi2;
            fixlut.sin_cos_tan(num.value, out var sinVal, out var cosVal, out var tanVal);
            sin = fp.ParseRaw(sinVal);
            cos = fp.ParseRaw(cosVal);
            tan = fp.ParseRaw(tanVal);
        }

        public static fp Sqrt(fp num) {
            fp r;

            if (num.value == 0) {
                r.value = 0;
            }
            else {
                var b = (num.value >> 1) + 1L;
                var c = (b + (num.value / b)) >> 1;

                while (c < b) {
                    b = c;
                    c = (b + (num.value / b)) >> 1;
                }

                r.value = b << (fixlut.PRECISION >> 1);
            }

            return r;
        }

        public static fp Sqrt_2(fp num) {
            return num.value <= 0 ? fp.zero : fp.ParseRaw(fixlut.sqrt(num.value));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp Floor(fp num) {
            return fp.Parse(num.AsLong);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp Ceil(fp num) {
            var fractions = num.value & 0x000000000000FFFFL;

            if (fractions == 0) {
                return num;
            }

            var full = new fp(num.value >> fixlut.PRECISION << fixlut.PRECISION);

            return full + fp.one;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp Fractions(fp num) {
            return new fp(num.value & 0x000000000000FFFFL);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int RoundToInt(fp num) {
            var fraction = new fp(num.value & 0x000000000000FFFFL);

            if (fraction.value >= fp.half.value) {
                return num.AsInt + 1;
            }

            return num.AsInt;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp Min(fp a, fp b) {
            return a.value < b.value ? a : b;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp Max(fp a, fp b) {
            return a.value > b.value ? a : b;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp Abs(fp num) {
            return new fp(num.value < 0 ? -num.value : num.value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp Clamp(fp num, fp min, fp max) {
            if (num.value < min.value) {
                return min;
            }

            if (num.value > max.value) {
                return max;
            }

            return num;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp Clamp01(fp num) {
            if (num.value < 0) {
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
        public static fp LerpUnclamped(fp from, fp to, fp t) {
            return from + (to - from) * t;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp Sign(fp num) {
            return num.value < fixlut.ZERO ? fp.minus_one : fp.one;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsOppositeSign(fp a, fp b) {
            return ((a.value ^ b.value) < 0);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp Pow2(int power) {
            return fp.ParseRaw(fixlut.ONE << power);
        }

        public static fp Exp(fp num) {
            if (num == fp.zero) return fp.one;
            if (num == fp.one) return fp.e;
            if (num.value >= 2097152) return fp.max;
            if (num.value <= -786432) return fp.zero;

            var neg      = num.value < 0;
            if (neg) num = -num;

            var result = num + fp.one;
            var term   = num;

            for (var i = 2; i < 30; i++) {
                term   *= num / fp.Parse(i);
                result += term;

                if (term.value < 500 && ((i > 15) || (term.value < 20)))
                    break;
            }

            if (neg) result = fp.one / result;

            return result;
        }
    }
}