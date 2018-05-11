namespace Celestial.Units
{
    public struct Pressure
    {
        internal static readonly double KilopascalPerMegapascal = 1000;
        internal static readonly double PascalPerKilopascal = 1000;
        internal static readonly double BarPerPascal = 1E5;
        internal static readonly double AtmospherePerPascal = 1.01325E5;
        internal static readonly double PsiPerPascal = 6.8948E3;

        private readonly double _pascals;

        public double Pascals => _pascals;

        public Pressure(double pascals)
        {
            _pascals = pascals;
        }

        public double TotalMegapascal => TotalKilopascal / KilopascalPerMegapascal;
        public double TotalKilopascal => _pascals / PascalPerKilopascal;
        public double TotalPascals => _pascals;
        public double TotalBars => _pascals / BarPerPascal;
        public double TotalAtmospheres => _pascals / AtmospherePerPascal;
        public double TotalPsis => _pascals / PsiPerPascal;

        public static Energy operator *(Pressure p, Volume v) => new Energy(p.TotalPascals * v.CubicMeters);
        public static Energy operator *(Volume v, Pressure p) => p*v;

        public override string ToString()
        {
            return $"{TotalPascals}[Pa]";
        }
    }

    public static class PressureExtensions
    {
        public static Pressure Psis(this double value) => new Pressure(value * Pressure.PsiPerPascal);

        public static Pressure Atmospheres(this double value) => new Pressure(value * Pressure.AtmospherePerPascal);

        public static Pressure Bars(this double value) => new Pressure(value * Pressure.BarPerPascal);

        public static Pressure Pascals(this double value) => new Pressure(value);

        public static Pressure Psis(this int value) => ((double)value).Psis();

        public static Pressure Atmospheres(this int value) => ((double)value).Atmospheres();

        public static Pressure Bars(this int value) => ((double)value).Bars();

        public static Pressure Pascals(this int value) => ((double)value).Pascals();
    }
}
