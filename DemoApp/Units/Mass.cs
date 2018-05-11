using System;

namespace Celestial.Units
{
    public struct Mass : IEquatable<Mass>, IComparable<Mass>, IUnitConvertable
    {
        internal static readonly double SolarMassPerMilkyWayMass = 8.5E11;
        internal static readonly double KilogramPerSolarmass = 1.989E30;
        internal static readonly double KilogramPerJupitermass = 1.898E27;
        internal static readonly double KilogramPerEarthmass = 5.9742E24;

        internal static readonly double KilogramPerTon = 1000.0;
        private readonly double _kilograms;
        internal static readonly double PlankmassPerKilogram = 4.595E7;
        internal static readonly double DaltonPerKilogram = 6.022E26;
        internal static readonly double ElectronrestmassPerKilogram = 1.098E30;

        public Mass(double kilograms)
        {
            if (kilograms < 0)
            {
                if (kilograms > -UnitConstants.EqualityComparisonEpsilon)
                    kilograms = 0;
                else
                    throw new NegativeUnitException();
            }
            _kilograms = kilograms;
            
        }


        public double TotalMilkyWayMasses => TotalSolarmasses / SolarMassPerMilkyWayMass;
        public double TotalSolarmasses => _kilograms / KilogramPerSolarmass;
        public double TotalJupiterMasses => _kilograms / KilogramPerJupitermass;
        public double TotalEarthMasses => _kilograms / KilogramPerEarthmass;
        public double TotalTons => _kilograms / KilogramPerTon;
        public double TotalKilograms => _kilograms;
        public double TotalPlankmass => _kilograms * PlankmassPerKilogram;
        public double TotalDaltons => _kilograms * DaltonPerKilogram;
        public double TotalElectronRestMass => _kilograms * ElectronrestmassPerKilogram;



        public static Mass operator *(double a, Mass b) => (a * b._kilograms).Kilograms();

        public static Mass operator *(Mass a, double b) => (a._kilograms * b).Kilograms();

        public static Mass operator /(Mass a, double b) => (a._kilograms / b).Kilograms();
        public static Time operator /(Mass a, Rate<Mass> b) => (a.TotalKilograms / b.Units.TotalKilograms) * b.Time;

        public static double operator /(Mass a, Mass b) => a._kilograms/b._kilograms;
        
        public static Mass operator +(Mass a, Mass b) => new Mass(a._kilograms + b._kilograms);

        public static Mass operator -(Mass a, Mass b) => new Mass(a._kilograms - b._kilograms);

        public static Force operator *(Acceleration a, Mass m) => new Force(a.TotalMetersPerSecondPerSecond * m.TotalKilograms);
        public static Force operator *(Mass m, Acceleration a) => a * m;
        public static Force operator *(SpeedRate s, Mass m) => new Force(s.TotalMetersPerSecondPerSecond * m.TotalKilograms);
        public static Force operator *(Mass m, SpeedRate s) => s * m;
        public static SpeedRate operator /(Force f, Mass m) => new Acceleration(f.TotalNewtons / m.TotalKilograms).SpeedRate;

        public static bool operator <(Mass a, Mass b) => a._kilograms < b._kilograms;
        public static bool operator <=(Mass a, Mass b) => a._kilograms <= b._kilograms;

        public static bool operator >(Mass a, Mass b) => a._kilograms > b._kilograms;
        public static bool operator >=(Mass a, Mass b) => a._kilograms >= b._kilograms;

        public static bool operator ==(Mass a, Mass b) => a._kilograms == b._kilograms;

        public static bool operator !=(Mass a, Mass b) => !(a == b);

        public override bool Equals(object obj)
        {
            if (!(obj is Mass))
                return false;

            return Equals((Mass)obj);
        }

        public override int GetHashCode()
        {
            return _kilograms.GetHashCode();
        }

        public bool Equals(Mass other) => Math.Abs(_kilograms - other._kilograms) < UnitConstants.EqualityComparisonEpsilon;

        public int CompareTo(Mass other) => _kilograms.CompareTo(other._kilograms);

        public string ToString(string format)
        {
            if (Math.Abs(TotalEarthMasses) < 0.001)
                return $"{TotalKilograms.ToString(format)}[kg]";
            else if (Math.Abs(TotalSolarmasses) < 1)
                return $"{TotalEarthMasses.ToString(format)}[Em]";
            else if (Math.Abs(TotalMilkyWayMasses) < 0.001)
                return $"{TotalSolarmasses.ToString(format)}[Sm]";
            else
                return $"{TotalMilkyWayMasses.ToString(format)}[MWm]";
        }

        public override string ToString()
        {
            return ToString("");
        }

        public double ToDouble()
        {
            return _kilograms;
        }
    }

    public static class MassExtensions
    {
        public static Mass SolarMasses(this double value) => new Mass(value * Mass.KilogramPerSolarmass);

        public static Mass JupiterMass(this double value) => new Mass(value * Mass.KilogramPerJupitermass);

        public static Mass EarthMass(this double value) => new Mass(value * Mass.KilogramPerEarthmass);

        public static Mass Tons(this double value) => new Mass(value * Mass.KilogramPerTon);

        public static Mass Kilograms(this double value) => new Mass(value);

        public static Mass SolarMasses(this int value) => ((double)value).SolarMasses();

        public static Mass JupiterMass(this int value) => ((double)value).JupiterMass();

        public static Mass EarthMass(this int value) => ((double)value).EarthMass();

        public static Mass Tons(this int value) => ((double)value).Tons();

        public static Mass Kilograms(this int value) => ((double)value).Kilograms();

    }
}
