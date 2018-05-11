namespace Celestial.Units
{
    public struct MagneticFieldStrength
    {
        private readonly double _teslas;

        public double Teslas => _teslas;

        public MagneticFieldStrength(double teslas)
        {
            _teslas = teslas;
        }

        public MagneticFieldStrength(Voltage v, Time t, Area a)
        {
            _teslas = v.TotalVolts * t.TotalSeconds / a.TotalSquareMeters;
        }

        public MagneticFieldStrength(Force f, ElectricCurrent a, Length l)
        {
            _teslas = f.TotalNewtons / (a.TotalAmperes * l.TotalMeters);
        }

        public MagneticFieldStrength(ElectricalInduction h, ElectricCurrent c, Area a)
        {
            _teslas = h.TotalHenrys * c.TotalAmperes / a.TotalSquareMeters;
        }

        public MagneticFieldStrength(MagneticFlux w, Area a)
        {
            _teslas = w.TotalWebers / a.TotalSquareMeters;
        }

        public MagneticFieldStrength(Mass m, ElectricCharge c, Time t)
        {
            _teslas = m.TotalKilograms / (c.TotalCoulombs * t.TotalSeconds);
        }

        public MagneticFieldStrength(Force f, ElectricCharge c, Time t, Length l)
        {
            _teslas = f.TotalNewtons * t.TotalSeconds / (c.TotalCoulombs * l.TotalMeters);
        }

        public MagneticFieldStrength(Mass m, ElectricCurrent a, Time t)
        {
            _teslas = m.TotalKilograms/(a.TotalAmperes*t.TotalSeconds*t.TotalSeconds);
        }


        public double TotalTeslas => _teslas;

        public static MagneticFlux operator *(MagneticFieldStrength t, Area a) => new MagneticFlux(t.TotalTeslas * a.TotalSquareMeters);
        public static MagneticFlux operator *(Area a, MagneticFieldStrength t) => t*a;

        public override string ToString() => $"{TotalTeslas}[T]";

    }

    public static class MagneticFieldStrengthExtensions
    {
        public static MagneticFieldStrength Teslas(this double value) => new MagneticFieldStrength(value);

        public static MagneticFieldStrength Teslas(this int value) => ((double)value).Teslas();
    }
}
