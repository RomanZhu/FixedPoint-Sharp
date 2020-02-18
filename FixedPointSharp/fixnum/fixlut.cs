namespace FixedPoint
{
    public static partial class fixlut
    {
        public const int  FRACTIONS_COUNT = 5;
        public const int  PRECISION = 16;
        public const long PI        = 205887L;
        public const long ONE       = 1 << PRECISION;
        public const long ZERO      = 0;


        public static long sqrt_aprox(long value)
        {
            return sqrt_aprox_lut[value];
        }

        public static long asin(long value)
        {
            if ((value < -65536) || (value > 65536))
            {
                return long.MinValue;
            }

            return asin_lut[value + 65536];
        }
        
        public static long acos(long value)
        {
            if ((value < -65536) || (value > 65536))
            {
                return long.MinValue;
            }

            return acos_lut[value + 65536];
        }
        

        public static long atan(long value)
        {
            if ((value < -65536) || (value > 65536))
            {
                return long.MinValue;
            }

            return atan_lut[value + 65536];
        }

        public static long sin(long value)
        {
            if (value < -205887)
            {
                value %= -205887;
            }

            if (value > 205887)
            {
                value %= 205887;
            }

            return sin_lut[value + 205887];
        }
        
        public static long cos(long value)
        {
            if (value < -205887)
            {
                value %= -205887;
            }

            if (value > 205887)
            {
                value %= 205887;
            }

            return cos_lut[value + 205887];
        }

        public static long tan(long value)
        {
            if (value < -205887)
            {
                value %= -205887;
            }

            if (value > 205887)
            {
                value %= 205887;
            }

            return tan_lut[value + 205887];
        }
    }
}