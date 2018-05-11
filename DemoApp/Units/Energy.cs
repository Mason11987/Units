namespace Celestial.Units
{
    public struct Energy
    {
        internal static readonly double ElectronvoltPerJoule = 6.242E16;
        internal static readonly double JoulePerKilocalorie = 4184;
        internal static readonly double JoulePerBtu = 1055;

        private readonly double _joules;

        public double Joules => _joules;

        public Energy(double joules)
        {
            _joules = joules;
        }

        public double TotalBtu => _joules / JoulePerBtu;
        public double TotalKilocalorie => _joules / JoulePerKilocalorie;
        public double TotalElectronvolt => _joules * ElectronvoltPerJoule;
        public double TotalJoules => _joules;

        public static Power operator /(Energy a, Time b) => new Power(a.TotalJoules / b.TotalSeconds);
        public static MagneticFlux operator /(Energy e, ElectricCurrent a) => new MagneticFlux(e.TotalJoules / a.TotalAmperes);
        public static Voltage operator /(Energy e, ElectricCharge c) => new Voltage(e.TotalJoules/c.TotalCoulombs);

        public override string ToString()
        {
            return $"{TotalJoules}[J]";
        }
    }

    public static class EnergyExtensions
    {
        public static Energy Joules(this double value) => new Energy(value);

        public static Energy Joules(this int value) => ((double)value).Joules();
    }
}
