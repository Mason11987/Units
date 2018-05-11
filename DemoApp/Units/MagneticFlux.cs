namespace Celestial.Units
{
    public struct MagneticFlux
    {
        internal static readonly double WebersPerMaxwell = 1E8;
        private readonly double _webers;

        public double Webers => _webers;

        public MagneticFlux(double webers)
        {
            _webers = webers;
        }

        public double TotalMaxwells => _webers / WebersPerMaxwell;
        public double TotalWebers => _webers;

        public static MagneticFieldStrength operator /(MagneticFlux w, Area a) => new MagneticFieldStrength(w.TotalWebers / a.TotalSquareMeters);
        public static ElectricalInduction operator /(MagneticFlux w, ElectricCurrent a) => new ElectricalInduction(w.TotalWebers / a.TotalAmperes);

        public override string ToString() => $"{TotalWebers}[Wb]";
    }

    public static class MagneticFluxExtensions
    {
        public static MagneticFlux Webers(this double value) => new MagneticFlux(value);

        public static MagneticFlux Webers(this int value) => ((double)value).Webers();
    }
}
