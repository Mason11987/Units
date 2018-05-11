using System;

namespace Celestial.Units
{
    public class Rate<T> where T : struct, IUnitConvertable
    {
        public T Units { get; }

        public Time Time { get; }

        public Rate(T unit, Time time)
        {
            Units = unit;
            Time = time;
        }

        public Rate(double unit, Time time)
        {
            Units = (T)Convert.ChangeType(unit.Units(), typeof(T)); 
            Time = time;
        }

        public override string ToString()
        {
            return $"{Units}/{Time}";
        }
    }

    internal static class RateExtensions
    {
        public static Rate<DoubleUnit> PerYear(this double value) => value / 1.Years();

        public static Rate<DoubleUnit> PerSecond(this double value) => value / 1.Seconds();
    }
}
