using System;

namespace Celestial.Units
{
    public struct SpeedSquared
    {

        private readonly double _metersSquaredPerSecondSquared;

        public SpeedSquared(double metersSquaredPerSecondSquared) : this()
        {
            _metersSquaredPerSecondSquared = metersSquaredPerSecondSquared;
        }

        public double TotalMetersSquaredPerSecondSquared => _metersSquaredPerSecondSquared;

        public static SpeedSquared operator +(SpeedSquared a, SpeedSquared b) => (a.TotalMetersSquaredPerSecondSquared + b.TotalMetersSquaredPerSecondSquared).MetersSquaredPerSecondSquared();
        public static SpeedSquared operator -(SpeedSquared a, SpeedSquared b) => (a.TotalMetersSquaredPerSecondSquared - b.TotalMetersSquaredPerSecondSquared).MetersSquaredPerSecondSquared();


        public static Length operator /(SpeedSquared a, SpeedRate b) => (a.TotalMetersSquaredPerSecondSquared / b.TotalMetersPerSecondPerSecond).Meters();

        public static bool operator ==(SpeedSquared a, SpeedSquared b) => Math.Abs(a.TotalMetersSquaredPerSecondSquared - b.TotalMetersSquaredPerSecondSquared) < UnitConstants.EqualityComparisonEpsilon;

        public static bool operator !=(SpeedSquared a, SpeedSquared b) => !(a == b);
        public static SpeedSquared operator -(SpeedSquared x) => new SpeedSquared(-x.TotalMetersSquaredPerSecondSquared);
        public static bool operator >(SpeedSquared a, SpeedSquared b) => a.TotalMetersSquaredPerSecondSquared > b.TotalMetersSquaredPerSecondSquared;
        public static bool operator <(SpeedSquared a, SpeedSquared b) => a.TotalMetersSquaredPerSecondSquared < b.TotalMetersSquaredPerSecondSquared;



        public override bool Equals(object obj)
        {
            if (!(obj is SpeedSquared))
                return false;

            return Equals((SpeedSquared)obj);
        }

        public override int GetHashCode()
        {
            return TotalMetersSquaredPerSecondSquared.GetHashCode();
        }

        public bool Equals(SpeedSquared other) => Math.Abs(TotalMetersSquaredPerSecondSquared - other.TotalMetersSquaredPerSecondSquared) < UnitConstants.EqualityComparisonEpsilon;

        public int CompareTo(SpeedSquared other) => TotalMetersSquaredPerSecondSquared.CompareTo(other.TotalMetersSquaredPerSecondSquared);

        public override string ToString() => ToString("");

        internal string ToString(string format)
        {
            return $"{TotalMetersSquaredPerSecondSquared.ToString(format)}[m^2/s^2]";
        }

        internal Speed Sqrt() => Math.Sqrt(TotalMetersSquaredPerSecondSquared).MetersPerSecond();
    }

    public static class SpeedSquaredExtensions
    {
        public static SpeedSquared MetersSquaredPerSecondSquared(this double value) => new SpeedSquared(value);

        public static SpeedSquared MetersSquaredPerSecondSquared(this int value) => ((double)value).MetersSquaredPerSecondSquared();

    }
}
