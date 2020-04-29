namespace LUTGenerator {
    internal class Generator {
        public static void Main(string[] args) {
            Writer.Write(Data.SinLut, "SinLut", "Sin");
            Writer.Write(Data.SinCosLut, "SinCosLut", "Sin");
            Writer.Write(Data.TanLut, "TanLut", "Sin");
            Writer.Write(Data.AcosLut, "AcosLut", "Sin");
            Writer.Write(Data.AsinLut, "AsinLut", "Sin");
            Writer.Write(Data.SqrtLut16, "SqrtLut16", "Sqrt16");
            Writer.Write(Data.SqrtLut256, "SqrtLut256", "Sqrt256");
            Writer.Write(Data.SqrtLut65536, "SqrtLut65536", "Sqrt65536");
            Writer.Write(Data.SqrtLut36Mil, "SqrtLut36Mil", "Sqrt36Mil");
        }
    }
}