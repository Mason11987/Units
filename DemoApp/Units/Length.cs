using System;

namespace Celestial.Units
{
    public struct Length : IEquatable<Length>, IComparable<Length>
    {
        internal static readonly double MegalightyearPerGegalightyear = 1000.0;
        internal static readonly double KilolightyearPerMegalightyear = 1000.0;
        internal static readonly double LightyearPerKilolightyear = 1000.0;
        internal static readonly double ParsecPerKiloparsec = 1000.0;
        internal static readonly double LightyearPerParsec = 3.26156;

        internal static readonly double KilometerPerLightyear = 8703094972723.2;
        internal static readonly double KilometerPerAstronomicslUnit = 1.496E8;
        internal static readonly double KilometerPerSolarRadius = 6.957E5;
        internal static readonly double KilometerPerJupiterRadius = 71492;
        internal static readonly double MetersPerKilometer = 1000.0;
        internal static readonly double CentimetersPerMeter = 100.0;
        internal static readonly double PlancklengthPerMeter = 6.25e+34;

        private readonly double _meters;

        public Length(double meters)
        {
            _meters = meters;
        }

        public double TotalGigalightyears => TotalMegalightyears / MegalightyearPerGegalightyear;

        public double TotalMegalightyears => TotalKilolightyears / KilolightyearPerMegalightyear;

        public double TotalKilolightyears => TotalLightyears / LightyearPerKilolightyear;

        public double TotalLightyears => TotalKilometers / KilometerPerLightyear;

        public double TotalAstronomicalUnits => TotalKilometers / KilometerPerAstronomicslUnit;

        public double TotalSolarRadii => TotalKilometers / KilometerPerSolarRadius;

        public double TotalJupiterRadii => TotalKilometers / KilometerPerJupiterRadius;

        public double TotalKilometers => _meters / MetersPerKilometer;

        public double TotalMeters => _meters;

        public double TotalCentimeters => _meters * CentimetersPerMeter;

        public double TotalPlanklength => _meters * PlancklengthPerMeter;


        public static Length Zero = new Length(0);

        public static Length operator +(Length a, Length b) => new Length(a._meters + b._meters);

        public static Length operator -(Length a, Length b) => new Length(a._meters - b._meters);

        public static Length operator -(Length a) => new Length(-a._meters);

        public static double operator /(Length a, Length b) => a._meters / b._meters;

        public static Length operator /(Length a, double b) => new Length(a._meters / b);

        public static Length operator *(double a, Length b) => (a * b._meters).Meters();

        public static Length operator *(Length a, double b) => (a._meters * b).Meters();

        public static Length operator /(float a, Length b) => new Length(a / b._meters);

        public static Area operator *(Length a, Length b) => new Area(a.TotalMeters * b.TotalMeters);

        public static bool operator <(Length a, Length b) => a._meters < b._meters;

        public static bool operator >(Length a, Length b) => a._meters > b._meters;

        public static bool operator >=(Length a, Length b) => a._meters >= b._meters;

        public static bool operator <=(Length a, Length b) => a._meters <= b._meters;

        public static bool operator ==(Length a, Length b) => Math.Abs(a._meters - b._meters) < UnitConstants.EqualityComparisonEpsilon;

        public static bool operator !=(Length a, Length b) => !(a == b);

        public override bool Equals(object obj)
        {
            if (!(obj is Length))
                return false;

            return Equals((Length)obj);
        }

        public bool Equals(Length other) => Math.Abs(_meters - other._meters) < UnitConstants.EqualityComparisonEpsilon;

        public int CompareTo(Length other) => _meters.CompareTo(other._meters);

        public override int GetHashCode() => _meters.GetHashCode();

        public string ToString(string format)
        {
            if (Math.Abs(TotalMeters) < 1)
                return $"{TotalCentimeters.ToString(format)}[cm]";
            else if (Math.Abs(TotalKilometers) < 1)
                return $"{TotalMeters.ToString(format)}[m]";
            else if (Math.Abs(TotalSolarRadii) < 0.01 || Math.Abs(TotalKilometers) <= 10000)
                return $"{TotalKilometers.ToString(format)}[km]";
            else if (Math.Abs(TotalAstronomicalUnits) < 1)
                return $"{TotalSolarRadii.ToString(format)}[Sr]";
            else if (Math.Abs(TotalLightyears) < 1)
                return $"{TotalAstronomicalUnits.ToString(format)}[Au]";
            else if (Math.Abs(TotalKilolightyears) < 1)
                return $"{TotalLightyears.ToString(format)}[Lyr]";
            else if (Math.Abs(TotalMegalightyears) < 1)
                return $"{TotalKilolightyears.ToString(format)}[KLyr]";
            else if (Math.Abs(TotalGigalightyears) < 1)
                return $"{TotalMegalightyears.ToString(format)}[MLyr]";
            else
                return $"{TotalGigalightyears.ToString(format)}[GLyr]";
        }

        public override string ToString()
        {
            return ToString("");
        }

        public double ToDouble()
        {
            return _meters;
        }
    }

    public static class LengthExtensions
    {
        public static Length Gigalightyears(this double value) => new Length(value * Length.MetersPerKilometer * Length.KilometerPerLightyear * Length.LightyearPerKilolightyear * Length.KilolightyearPerMegalightyear * Length.MegalightyearPerGegalightyear);

        public static Length Kiloparsecs(this double value) => new Length(value * Length.MetersPerKilometer * Length.KilometerPerLightyear * Length.LightyearPerParsec * Length.ParsecPerKiloparsec);

        public static Length Lightyears(this double value) => new Length(value * Length.MetersPerKilometer * Length.KilometerPerLightyear);

        public static Length AstronomicalUnits(this double value) => new Length(value * Length.MetersPerKilometer * Length.KilometerPerAstronomicslUnit);

        public static Length SolarRadii(this double value) => new Length(value * Length.MetersPerKilometer * Length.KilometerPerSolarRadius);

        public static Length JupiterRadii(this double value) => new Length(value * Length.MetersPerKilometer * Length.KilometerPerJupiterRadius);

        public static Length Kilometers(this double value) => new Length(value * Length.MetersPerKilometer);

        public static Length Meters(this double value) => new Length(value);

        public static Length Centimeters(this double value) => new Length(value / Length.CentimetersPerMeter);


        public static Length Gigalightyears(this int value) => ((double) value).Gigalightyears();

        public static Length Kiloparsecs(this int value) => ((double) value).Kiloparsecs();

        public static Length Lightyears(this int value) => ((double)value).Lightyears();

        public static Length AstronomicalUnits(this int value) => ((double)value).AstronomicalUnits();

        public static Length SolarRadii(this int value) => ((double)value).SolarRadii();

        public static Length JupiterRadii(this int value) => ((double)value).JupiterRadii();

        public static Length Kilometers(this int value) => ((double)value).Kilometers();

        public static Length Meters(this int value) => ((double)value).Meters();

        public static Length Centimeters(this int value) => ((double)value).Centimeters();
    }
}
