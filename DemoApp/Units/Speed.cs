using System;

namespace Celestial.Units
{
    public struct Speed
    {
        internal static readonly double MetersPerSecondPerSpeedOfLight = 299792458;
        internal static readonly double MetersPerSecondPerKilometerPerSecond = 1000;
        internal static readonly double MetersPerSecondPerSpeedOfSound = 343.2;

        private readonly Length _lengthPerSecond;

        public Length LengthPerSecond => _lengthPerSecond;

        public Speed(Length lengthPerSecond)
        {
            _lengthPerSecond = lengthPerSecond;
        }

        public Speed(Length length, Time time)
        {
            _lengthPerSecond = length / (time / 1.Seconds());
        }

        public Speed(double metersPerSecond)
        {
            if (metersPerSecond == double.NaN)
                throw new NotFiniteNumberException();
            _lengthPerSecond = new Length(metersPerSecond);
        }

        public double TotalC => TotalMetersPerSecond/MetersPerSecondPerSpeedOfLight;
        public double TotalKilometersPerSecond => TotalMetersPerSecond / MetersPerSecondPerKilometerPerSecond;
        public double TotalMach => TotalMetersPerSecond/MetersPerSecondPerSpeedOfSound;
        public double TotalMetersPerSecond => _lengthPerSecond.TotalMeters;

        public static Speed operator +(Speed a, Speed b) => (a.TotalMetersPerSecond + b.TotalMetersPerSecond).MetersPerSecond();
        public static Speed operator -(Speed a, Speed b) => (a.TotalMetersPerSecond - b.TotalMetersPerSecond).MetersPerSecond();
        public static SpeedSquared operator *(Speed a, Speed b) => (a.TotalMetersPerSecond * b.TotalMetersPerSecond).MetersSquaredPerSecondSquared();

        public static Speed operator /(Speed a, int b) => new Speed(a.TotalMetersPerSecond / b);
        public static Speed operator *(double a, Speed b) => (a * b.TotalKilometersPerSecond).MetersPerSecond();

        public static Length operator *(Speed s, Time t) => (s.TotalMetersPerSecond * t.TotalSeconds).Meters();
        public static Length operator *(Time t, Speed s) => s * t;

        public static Time operator /(Length l, Speed s) => (l.TotalMeters / s.TotalMetersPerSecond).Seconds();

        public static Time operator /(Speed s, SpeedRate r) => (s.TotalMetersPerSecond / r.TotalMetersPerSecondPerSecond).Seconds();

        public static Speed operator *(Speed s, double t) => (s.TotalMetersPerSecond * t).MetersPerSecond();

        public static bool operator ==(Speed a, Speed b) => Math.Abs(a.TotalMetersPerSecond - b.TotalMetersPerSecond) < UnitConstants.EqualityComparisonEpsilon;

        public static bool operator !=(Speed a, Speed b) => !(a == b);
        public static Speed operator -(Speed x) => new Speed(-x.LengthPerSecond);
        public static bool operator >(Speed a, Speed b) => a.TotalMetersPerSecond > b.TotalMetersPerSecond;
        public static bool operator <(Speed a, Speed b) => a.TotalMetersPerSecond < b.TotalMetersPerSecond;


        public static SpeedRate operator /(Speed s, Time t)
                     => new SpeedRate(s, t);


        public override bool Equals(object obj)
        {
            if (!(obj is Speed))
                return false;

            return Equals((Speed)obj);
        }

        public override int GetHashCode()
        {
            return _lengthPerSecond.GetHashCode();
        }

        public bool Equals(Speed other) => Math.Abs(TotalMetersPerSecond - other.TotalMetersPerSecond) < UnitConstants.EqualityComparisonEpsilon;

        public int CompareTo(Speed other) => TotalMetersPerSecond.CompareTo(other.TotalMetersPerSecond);

        public override string ToString() => ToString("");

        internal string ToString(string format)
        {
            if (Math.Abs(TotalC) >= 0.01)
                return $"{TotalC.ToString(format)}[c]";
            if (Math.Abs(TotalKilometersPerSecond) > 1)
                return $"{TotalKilometersPerSecond.ToString(format)}[km/s]";
            if (Math.Abs(TotalMach) > 1)
                return $"{TotalMach.ToString(format)}[mach]";

            return $"{TotalMetersPerSecond.ToString(format)}[m/s]";
        }
    }

    public static class SpeedExtensions
    {
        public static Speed c(this double value) => value.MetersPerSecond()*Speed.MetersPerSecondPerSpeedOfLight;

        public static Speed Mach(this double value) => value.MetersPerSecond()*Speed.MetersPerSecondPerSpeedOfSound;

        public static Speed MetersPerSecond(this double value) => new Speed(value.Meters(), 1.Seconds());

        public static Speed c(this int value) => ((double)value).c();

        public static Speed Mach(this int value) => ((double)value).Mach();

        public static Speed MetersPerSecond(this int value) => ((double)value).MetersPerSecond();

    }
}
