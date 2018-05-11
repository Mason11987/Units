namespace Celestial.Units
{
    public struct Voltage
    {
        private readonly double _volts;

        public double Volts => _volts;

        public Voltage(double volts)
        {
            _volts = volts;
        }

        public Voltage(Force force, Length length, ElectricCharge charge)
        {
            _volts = force.TotalNewtons * length.TotalMeters / charge.TotalCoulombs;
        }

        public Voltage(Mass mass, Acceleration accel, Speed speed, ElectricCurrent current)
        {
            _volts = mass.TotalKilograms*accel.TotalMetersPerSecondPerSecond*speed.TotalMetersPerSecond/
                     current.TotalAmperes;
        }

        public double TotalVolts => _volts;

        public static ElectricalConductance operator /(ElectricCurrent a, Voltage v) => new ElectricalConductance(a.TotalAmperes / v.TotalVolts);
        public static ElectricalResistance operator /(Voltage v,  ElectricCurrent a) => new ElectricalResistance(v.TotalVolts / a.TotalAmperes);
        public static Power operator *(Voltage v, ElectricCurrent a) => new Power(v.TotalVolts*a.TotalAmperes);
        public static Power operator *(ElectricCurrent a, Voltage v) => v*a;
        public static MagneticFlux operator *(Voltage v, Time t) => new MagneticFlux(v.TotalVolts * t.TotalSeconds);
        public static MagneticFlux operator *(Time t, Voltage v) => v*t;

        public override string ToString() => $"{TotalVolts}[V]";

    }

    public static class VoltageExtensions
    {
        public static Voltage Volts(this double value) => new Voltage(value);

        public static Voltage Volts(this int value) => ((double)value).Volts();
    }
}
