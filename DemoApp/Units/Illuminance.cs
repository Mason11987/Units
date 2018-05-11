namespace Celestial.Units
{
    public struct Illuminance
    {
        private readonly double _lux;

        public double Lux => _lux;

        public Illuminance(double lux)
        {
            _lux = lux;
        }

        public double TotalLux => _lux;

        public override string ToString() => $"{TotalLux}[lx]";
    }

    public static class IlluminanceExtensions
    {
        public static Illuminance Lux(this double value) => new Illuminance(value);

        public static Illuminance Lux(this int value) => ((double)value).Lux();
    }
}
