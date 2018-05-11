namespace Celestial.Units
{
    public struct Force
    {
        internal static readonly double NewtonPerKilonewton = 1000;
        internal static readonly double NewtonPerKilogramforce = 9.80665;

        private readonly double _newtons;

        public double Newtons => _newtons;

        public Force(double newtons)
        {
            _newtons = newtons;
        }

        public double TotalKilonewtons => _newtons / NewtonPerKilonewton;
        public double TotalKilogramforce => _newtons / NewtonPerKilogramforce;
        public double TotalNewtons => _newtons;

        public static Energy operator *(Force f, Length l) => new Energy(f.TotalNewtons*l.TotalMeters);
        public static Pressure operator /(Force f, Area a) => new Pressure(f.TotalNewtons/a.TotalSquareMeters);
        public static Power operator *(Force f, Speed s) => new Power(f.TotalNewtons*s.TotalMetersPerSecond);
        public static Power operator *(Speed s, Force f) => f*s;

        public override string ToString() => ToString("");

        internal string ToString(string format)
        {
            return $"{TotalNewtons.ToString(format)}[N]";
        }
    }

    public static class ForceExtensions
    {
        public static Force Kilonewtons(this double value) => new Force(value * Force.NewtonPerKilonewton);

        public static Force Newtons(this double value) => new Force(value);

        public static Force Kilonewtons(this int value) => ((double)value).Kilonewtons();

        public static Force Newtons(this int value) => ((double)value).Newtons();
    }
}
