using System;

namespace Celestial.Units
{
    public class Temperature : IEquatable<Temperature>, IComparable<Temperature>
    {
        internal static readonly double KelvinPerPlanktemperature = 1.41683385E32;

        private readonly double _kelvin;


        public Temperature(double watts)
        {
            _kelvin = watts;
        }


        public double TotalPlanktemperature => _kelvin * KelvinPerPlanktemperature;

        public double TotalKelvin => _kelvin;

        public double TotalCelcius => _kelvin - 273.15;

        public double TotalFarenheit => TotalCelcius * 9.0/5.0 + 32;

        public bool Equals(Temperature other)
        {
            return other != null && _kelvin.Equals(other._kelvin);
        }
        public int CompareTo(Temperature other)
        {
            return _kelvin.CompareTo(other._kelvin);
        }

        public string ToString(string format)
        {
            return $"{TotalKelvin.ToString(format)}[K]";
        }

        public override string ToString()
        {
            return ToString("");
        }

        public double ToDouble()
        {
            return _kelvin;
        }
    }

    public static class TemperatureExtensions
    {
        public static Temperature Kelvin(this double value) => new Temperature(value);

        public static Temperature Celcius(this double value) => new Temperature(value + 273.15);

        public static Temperature Farenheit(this double value) => new Temperature((value + 459.67) * 5.0/9.0);

        public static Temperature Kelvin(this int value) => ((double)value).Kelvin();

        public static Temperature Celcius(this int value) => ((double)value).Celcius();

        public static Temperature Farenheit(this int value) => ((double)value).Farenheit();
    }
}
