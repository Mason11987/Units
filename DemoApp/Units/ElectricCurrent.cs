using System;

namespace Celestial.Units
{
    public struct ElectricCurrent : IEquatable<ElectricCurrent>, IComparable<ElectricCurrent>
    {
        private readonly double _amperes;

        public ElectricCurrent(double amperes)
        {
            _amperes = amperes;
        }


        public double TotalAmperes => _amperes;

        public static ElectricCharge operator *(Time t, ElectricCurrent a) => new ElectricCharge(t.TotalSeconds * a.TotalAmperes);
        public static ElectricCharge operator *(ElectricCurrent a, Time t) => t * a;
        public static Voltage operator *(ElectricCurrent a, ElectricalResistance o) => new Voltage(a.TotalAmperes * o.TotalOhms);
        public static Voltage operator *(ElectricalResistance o, ElectricCurrent a) => a*o;

        public bool Equals(ElectricCurrent other)
        {
            return _amperes.Equals(other._amperes);
        }
        public int CompareTo(ElectricCurrent other)
        {
            return _amperes.CompareTo(other._amperes);
        }

        public string ToString(string format)
        {
            return $"{TotalAmperes.ToString(format)}[A]";
        }

        public override string ToString()
        {
            return ToString("");
        }

        public double ToDouble()
        {
            return _amperes;
        }
    }

    public static class ElectricCurrentExtensions
    {
        public static ElectricCurrent Amperes(this double value) => new ElectricCurrent(value);

        public static ElectricCurrent Amperes(this int value) => ((double)value).Amperes();
    }
}
