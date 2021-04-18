namespace LUTGenerator {
    internal class Generator {
        public static void Main(string[] args) {
            Writer.Write(Data.SinLut,     "SinLut");
            Writer.Write(Data.SinCosLut,  "SinCosLut");
            Writer.Write(Data.TanLut,     "TanLut");
            Writer.Write(BigData.AsinLut, "AsinLut");
        }
    }
}