using System;

namespace Celestial.Units
{
    public class Power : IEquatable<Power>, IComparable<Power>
    {
        internal static readonly double WattsPerSolarluminosity = 3.828E26;
        internal static readonly double WattsPerHorsepower = 746;

        private readonly double _watts;


        public Power(double watts)
        {
            _watts = watts;
        }

        public Power(Mass mass, Acceleration accel, Speed speed)
        {
            _watts = mass.TotalKilograms * accel.TotalMetersPerSecondPerSecond * speed.TotalMetersPerSecond;
        }

        public Power(Voltage voltage, ElectricalResistance ohms)
        {
            _watts = voltage.TotalVolts * voltage.TotalVolts / ohms.TotalOhms;
        }

        public Power(ElectricCurrent current, ElectricalResistance ohms)
        {
            _watts = current.TotalAmperes * current.TotalAmperes * ohms.TotalOhms;
        }


        public double TotalWatts => _watts;
        public double TotalHorsepower => _watts / WattsPerHorsepower;
        public double TotalSolarluminosities => _watts / WattsPerSolarluminosity;

        public static Voltage operator /(Power p, ElectricCurrent a) => new Voltage(p.TotalWatts / a.TotalAmperes);
        public static Energy operator *(Power p, Time t) => new Energy(p.TotalWatts*t.TotalSeconds);
        public static Energy operator *(Time t, Power p) => p * t;

        public bool Equals(Power other)
        {
            return other != null && _watts.Equals(other._watts);
        }
        public int CompareTo(Power other)
        {
            return _watts.CompareTo(other._watts);
        }

        public string ToString(string format)
        {
            if (Math.Abs(TotalSolarluminosities) < 0.0001)
                return $"{TotalWatts.ToString(format)}[W]";
            else
                return $"{TotalSolarluminosities.ToString(format)}[SL]";
        }

        public override string ToString()
        {
            return ToString("");
        }

        public double ToDouble()
        {
            return _watts;
        }
    }

    public static class PowerExtensions
    {
        public static Power Watts(this double value) => new Power(value);

        public static Power SolarLuminosity(this double value) => new Power(value * Power.WattsPerSolarluminosity);

        public static Power Watts(this int value) => ((double)value).Watts();

        public static Power SolarLuminosity(this int value) => ((double) value).SolarLuminosity();
    }
}
