using System;

namespace Celestial.Units
{
    public struct SpeedRate
    {
        public Speed SpeedPerSecond { get; }

        public SpeedRate(Speed speed, Time time)
        {
            SpeedPerSecond = speed * (time / 1.Seconds());
        }

        public SpeedRate(Speed speed) : this(speed, 1.Seconds())
        {
        }

        public double TotalMetersPerSecondPerSecond => SpeedPerSecond.TotalMetersPerSecond;

        public static SpeedRate operator *(double a, SpeedRate r) => (a * r.TotalMetersPerSecondPerSecond).MetersPerSecondPerSecond();
        public static SpeedRate operator *(SpeedRate r, double a) => a*r;
        public static SpeedSquared operator *(SpeedRate a, Length b) => (a.TotalMetersPerSecondPerSecond * b.TotalMeters).MetersSquaredPerSecondSquared();
        public static Speed operator *(SpeedRate r, Time t) => (r.TotalMetersPerSecondPerSecond * t.TotalSeconds).MetersPerSecond();
        public static Speed operator *(Time t, SpeedRate r) => r*t;
        public static SpeedRate operator -(SpeedRate x) => new SpeedRate(-x.SpeedPerSecond);
        public static Time operator /(SpeedRate speedRate, Speed speed)
            => (speed.TotalMetersPerSecond / speedRate.TotalMetersPerSecondPerSecond).Seconds();

        public static bool operator ==(SpeedRate a, SpeedRate b) => Math.Abs(a.TotalMetersPerSecondPerSecond - b.TotalMetersPerSecondPerSecond) < UnitConstants.EqualityComparisonEpsilon;

        public static bool operator !=(SpeedRate a, SpeedRate b) => !(a == b);
        public static bool operator >(SpeedRate a, SpeedRate b) => a.TotalMetersPerSecondPerSecond > b.TotalMetersPerSecondPerSecond;
        public static bool operator <(SpeedRate a, SpeedRate b) => a.TotalMetersPerSecondPerSecond < b.TotalMetersPerSecondPerSecond;

        public override bool Equals(object obj)
        {
            if (!(obj is SpeedRate))
                return false;

            return Equals((SpeedRate)obj);
        }

        public override int GetHashCode()
        {
            return TotalMetersPerSecondPerSecond.GetHashCode();
        }

        public bool Equals(SpeedRate other) => Math.Abs(TotalMetersPerSecondPerSecond - other.TotalMetersPerSecondPerSecond) < UnitConstants.EqualityComparisonEpsilon;

        public int CompareTo(SpeedRate other) => TotalMetersPerSecondPerSecond.CompareTo(other.TotalMetersPerSecondPerSecond);


        public override string ToString()
        {
            return $"{TotalMetersPerSecondPerSecond}[m/s2]";
        }

    }

    public static class SpeedRateExtensions
    {
        public static SpeedRate MetersPerSecondPerSecond(this double value) => new SpeedRate(value.MetersPerSecond(), 1.Seconds());

        public static SpeedRate MetersPerSecondPerSecond(this int value) => ((double)value).MetersPerSecondPerSecond();
    }
}
