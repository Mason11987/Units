using System;
using Microsoft.Xna.Framework;

namespace Celestial.Units
{
    public struct Angle
    {
        internal static readonly double RadiansPerDegree = MathHelper.TwoPi/360;

        private readonly double _radians;

        public double Radians => _radians;

        public static bool operator ==(Angle a, Angle b) => Math.Abs(a.TotalRadians - b.TotalRadians) < UnitConstants.EqualityComparisonEpsilon;

        public static bool operator !=(Angle a, Angle b) => !(a == b);

        public bool Equals(Angle other)
        {
            return _radians.Equals(other._radians);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Angle && Equals((Angle)obj);
        }

        public override int GetHashCode()
        {
            return _radians.GetHashCode();
        }

        public Angle(double radians)
        {
            if (radians < 0)
                radians = radians + 2 * Math.PI;
            if (radians > 2 * Math.PI)
                radians = radians % (2 * Math.PI);
            _radians = radians;
        }

        public double TotalRadians => _radians;

        public double TotalAngles => _radians / RadiansPerDegree;

        public static Angle operator +(Angle a, Angle b) => new Angle(a._radians + b._radians);

        public static Angle operator -(Angle a, Angle b) => new Angle(a._radians - b._radians);

        public static Angle operator /(Angle a, double b) => new Angle(a._radians / 2);
        public static Angle operator *(Angle a, double b) => new Angle(a._radians * 2);
        public static bool operator <(Angle a, Angle b) => a._radians < b._radians;

        public static bool operator >(Angle a, Angle b) => a._radians > b._radians;

        public static bool operator >=(Angle a, Angle b) => a._radians >= b._radians;

        public static bool operator <=(Angle a, Angle b) => a._radians <= b._radians;

        public string ToString(string format)
        {
            return $"{TotalAngles.ToString(format)}[°]";
        }

        public override string ToString()
        {
            return ToString("");
        }

        public Angle Invert()
        {
            return (TotalRadians + Math.PI).Radians();
        }
    }

    public static class AngleExtensions
    {
        public static Angle Radians(this double value) => new Angle(value);

        public static Angle Degrees(this double value) => new Angle(value * Angle.RadiansPerDegree);

        public static Angle Radians(this float value) => new Angle(value);

        public static Angle Degrees(this float value) => new Angle(value * Angle.RadiansPerDegree);

        public static Angle Radians(this int value) => ((double)value).Radians();

        public static Angle Degrees(this int value) => ((double)value).Degrees();

    }
}
