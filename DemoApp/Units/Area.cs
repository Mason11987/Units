namespace Celestial.Units
{
    public struct Area
    {
        internal static readonly double SquaremeterPerSquareKilometer = 1000000;
        internal static readonly double SquaremeterPerAcre = 4046.86;

        private readonly double _squareMeters;

        public double SquareMeters => _squareMeters;

        public Area(double squareMeters)
        {
            if (squareMeters < 0)
                throw new NegativeUnitException();
            _squareMeters = squareMeters;
        }

        public double TotalSquareKilometer => _squareMeters / SquaremeterPerSquareKilometer;
        public double TotalAcre => _squareMeters / SquaremeterPerAcre;
        public double TotalSquareMeters => _squareMeters;

        public static Volume operator *(Area a, Length b) => new Volume(a.TotalSquareMeters * b.TotalMeters);
        public static Volume operator *(Length a, Area b) => b*a;


        public override string ToString()
        {
            return $"{TotalSquareMeters}[m2]";
        }
    }

    public static class AreaExtensions
    {
        public static Area SquareMeters(this double value) => new Area(value);

        public static Area SquareMeters(this int value) => ((double)value).SquareMeters();
    }
}
