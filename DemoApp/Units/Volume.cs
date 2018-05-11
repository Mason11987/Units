namespace Celestial.Units
{
    public struct Volume
    {
        private readonly double _cubicMeters;

        public double CubicMeters => _cubicMeters;

        public Volume(double cubicMeters)
        {
            _cubicMeters = cubicMeters;
        }

        public double TotalCubicMeters => _cubicMeters;

        public override string ToString()
        {
            return $"{TotalCubicMeters}[m3]";
        }

    }

    public static class VolumeExtensions
    {
        public static Volume CubicMeters(this double value) => new Volume(value);

        public static Volume CubicMeters(this int value) => ((double)value).CubicMeters();
    }
}
