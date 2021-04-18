using System.Runtime.InteropServices;

namespace FixedPoint {
    [StructLayout(LayoutKind.Explicit, Size = SIZE)]
    // ReSharper disable once InconsistentNaming
    public struct fbool {
        public const int SIZE = 2;
        
        [FieldOffset(0)]
        public ushort Value;

        public static implicit operator bool(fbool b)
        {
            return b.Value > 0;
        }

        public static implicit operator fbool(bool b)
        {
            fbool fbool;
            fbool.Value = (ushort) (b ? 1 : 0);
            return fbool;
        }

        public override int GetHashCode() {
            return Value;
        }

        public override string ToString() {
            return ((bool) this).ToString();
        }
    }
}