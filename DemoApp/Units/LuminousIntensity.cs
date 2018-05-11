using System;

namespace Celestial.Units
{
    public struct LuminousIntensity : IEquatable<LuminousIntensity>, IComparable<LuminousIntensity>
    {
        private readonly double _candelas;

        public LuminousIntensity(double candelas)
        {
            _candelas = candelas;
        }

        public double TotalCandelas => _candelas;

        public bool Equals(LuminousIntensity other)
        {
            return _candelas.Equals(other._candelas);
        }
        public int CompareTo(LuminousIntensity other)
        {
            return _candelas.CompareTo(other._candelas);
        }

        public string ToString(string format)
        {
            return $"{TotalCandelas.ToString(format)}[cd]";
        }

        public override string ToString()
        {
            return ToString("");
        }

        public double ToDouble()
        {
            return _candelas;
        }
    }

    public static class LuminousIntensityExtensions
    {
        public static LuminousIntensity Candela(this double value) => new LuminousIntensity(value);

        public static LuminousIntensity Candela(this int value) => ((double)value).Candela();
    }
}
