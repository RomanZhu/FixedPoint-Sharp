namespace LUTGenerator {
    internal class Generator {
        public static void Main(string[] args) {
            Writer.Write(Data.SinLut, "SinLut", "Sin");
            Writer.Write(Data.SinCosLut, "SinCosLut", "Sin");
            Writer.Write(Data.TanLut, "TanLut", "Sin");
            Writer.Write(Data.AcosLut, "AcosLut", "Sin");
            Writer.Write(Data.AsinLut, "AsinLut", "Sin");
        }
    }
}