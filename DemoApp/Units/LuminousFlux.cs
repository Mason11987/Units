namespace Celestial.Units
{
    public struct LuminousFlux
    {
        private readonly double _lumens;

        public double Lumens => _lumens;

        public LuminousFlux(double lumens)
        {
            _lumens = lumens;
        }

        public double TotalLumens => _lumens;

        public static Illuminance operator /(LuminousFlux l, Area a) => new Illuminance(l.TotalLumens / a.TotalSquareMeters);

        public override string ToString() => $"{TotalLumens}[lm]";

    }

    public static class LuminousFluxExtensions
    {
        public static LuminousFlux Lumens(this double value) => new LuminousFlux(value);

        public static LuminousFlux Lumens(this int value) => ((double)value).Lumens();
    }
}
