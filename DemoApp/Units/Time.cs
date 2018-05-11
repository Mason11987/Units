using System;

namespace Celestial.Units
{
    public class Time : IEquatable<Time>, IComparable<Time>
    {
        internal static readonly double MonthPerYear = 12;
        internal static readonly double DayPerWeek = 7;
        internal static readonly double DayPerMonth = 28;
        internal static readonly double HourPerDay = 24;
        internal static readonly double MinutePerHour = 60.0;
        internal static readonly double SecondPerMinute = 60.0;
        internal static readonly double MillisecondPerSecond = 1000.0;
        internal static readonly double PlanktimePerSecond = 1.855E43;
        internal static readonly double SecondPerYear = SecondPerMinute * MinutePerHour* HourPerDay * DayPerMonth* MonthPerYear;
        internal static readonly double SecondPerMonth = SecondPerMinute * MinutePerHour * HourPerDay * DayPerMonth;
        internal static readonly double SecondPerDay = SecondPerMinute * MinutePerHour * HourPerDay;
        internal static readonly double SecondPerHour = SecondPerMinute * MinutePerHour;

        private readonly double _seconds;

        public Time(double seconds)
        {
            _seconds = seconds;
        }

        public double TotalYears => TotalMonths / MonthPerYear;

        public double TotalMonths => TotalDays / DayPerMonth;

        public double TotalWeeks => TotalDays / DayPerWeek;

        public double TotalDays => TotalHours / HourPerDay;

        public double TotalHours => TotalMinutes / MinutePerHour;

        public double TotalMinutes => TotalSeconds / SecondPerMinute;

        public double TotalSeconds => _seconds;

        public double TotalMilliseconds => _seconds * MillisecondPerSecond;

        public double TotalPlanktime => _seconds * PlanktimePerSecond;

        public static readonly Time Zero = new Time(0);

        public static Time operator +(Time a, Time b) => new Time(a._seconds + b._seconds);

        public static Time operator -(Time a, Time b) => new Time(a._seconds - b._seconds);

        public static double operator /(Time a, Time b) => a._seconds / b._seconds;

        public static Time operator /(Time a, double b) => new Time(a._seconds / b);

        public static Time operator *(double a, Time b) => (a * b._seconds).Seconds();

        public static Time operator *(Time a, double b) => (a._seconds * b).Seconds();

        public static bool operator <(Time a, Time b) => a._seconds < b._seconds;

        public static bool operator <=(Time a, Time b) => a._seconds < b._seconds || a._seconds == b._seconds;

        public static bool operator >(Time a, Time b) => a._seconds > b._seconds;

        public static bool operator >=(Time a, Time b) => a._seconds > b._seconds || a._seconds == b._seconds;

        public static bool operator ==(Time a, Time b)
        {
            if (ReferenceEquals(a, b)) return true;

            if (ReferenceEquals(a, null) || ReferenceEquals(b, null)) return false;

            return Math.Abs(a._seconds - b._seconds) < UnitConstants.EqualityComparisonEpsilon;
        }


        public static bool operator !=(Time a, Time b) => !(a == b);

        public static Speed operator /(Length length, Time time) => new Speed(length, time);

        public static Rate<DoubleUnit> operator /(double unit, Time time) => new Rate<DoubleUnit>(unit, time);

        public static Rate<Mass> operator /(Mass a, Time b) => new Rate<Mass>(a, b);
        public static Mass operator *(Rate<Mass> a, Time b) => (a.Units.TotalKilograms * (b.TotalSeconds / a.Time.TotalSeconds)).Kilograms();
        public static Mass operator *(Time b, Rate<Mass> a) => a*b;
        public static Length3 operator *(Time a, Velocity b) => new Length3(b.X * a, a * b.Y, a * b.Z);
        public static Length3 operator *(Velocity a, Time b) => b * a;

        public static DoubleUnit operator *(Rate<DoubleUnit> unit, Time time) => (unit.Units.ToDouble() * (time.TotalSeconds /unit.Time.TotalSeconds)).Units();
        public static DoubleUnit operator *(Time time, Rate<DoubleUnit> unit) => unit*time;

        public override bool Equals(object obj)
        {
            if (!(obj is Time))
                return false;

            return Equals((Time)obj);
        }

        public bool Equals(Time other)
        {
            return other != null && Math.Abs(_seconds - other._seconds) < UnitConstants.EqualityComparisonEpsilon;
        }

        public int CompareTo(Time other) => _seconds.CompareTo(other._seconds);

        public override int GetHashCode() => _seconds.GetHashCode();

        public double ToDouble()
        {
            return _seconds;
        }

        public string ToDateString()
        {
            return $"{DateString()} {TimeString()}";
        }

        private string DateString()
        {
            return $"{MonthComponent + 1}/{DayComponent + 1}/{YearComponent}";
        }

        private string TimeString()
        {
            return $"{HourComponent}:{MinuteComponent:00}:{SecondComponent:00.###}";
        }

        public int YearComponent => (int)(TotalSeconds / SecondPerYear);
        public int MonthComponent => (int) ((TotalSeconds 
            - (YearComponent*SecondPerYear))
            / SecondPerMonth);
        public int DayComponent => (int)((TotalSeconds 
            - (YearComponent * SecondPerYear) 
            - (MonthComponent * SecondPerMonth)) 
            / SecondPerDay);

        public int HourComponent => (int)((TotalSeconds
            - (YearComponent * SecondPerYear)
            - (MonthComponent * SecondPerMonth)
            - (DayComponent * SecondPerDay))
            / SecondPerHour);
        public int MinuteComponent => (int)((TotalSeconds
            - (YearComponent * SecondPerYear)
            - (MonthComponent * SecondPerMonth)
            - (DayComponent * SecondPerDay)
            - (HourComponent * SecondPerHour))
            / SecondPerMinute);
        public double SecondComponent => (int)(TotalSeconds
            - (YearComponent * SecondPerYear)
            - (MonthComponent * SecondPerMonth)
            - (DayComponent * SecondPerDay)
            - (HourComponent * SecondPerHour)
            - (MinuteComponent * SecondPerMinute));

        // ReSharper disable once CompareOfFloatsByEqualityOperator
        public bool IsZero => _seconds == 0;

        public string ToString(string format)
        {
            if (Math.Abs(TotalSeconds) < 1)
                return $"{TotalMilliseconds.ToString(format)}[ms]";
            else if (Math.Abs(TotalMinutes) < 1)
                return $"{TotalSeconds.ToString(format)}[s]";
            else if (Math.Abs(TotalHours) < 1)
                return $"{TotalMinutes.ToString(format)}[min]";
            else if (Math.Abs(TotalDays) < 1)
                return $"{TotalHours.ToString(format)}[hr]";
            else if (Math.Abs(TotalMonths) < 1)
                return $"{TotalDays.ToString(format)}[d]";
            else if (Math.Abs(TotalYears) < 1)
                return $"{TotalMonths.ToString(format)}[mth]";
            else
                return $"{TotalYears.ToString(format)}[yr]";
        }

        public override string ToString()
        {
            return ToString("");
        }
    }

    public static class TimeExtensions
    {
        public static Time Years(this double value) => (value * Time.MonthPerYear).Months();

        public static Time Months(this double value) => (value*Time.DayPerMonth).Days();

        public static Time Weeks(this double value) => (value * Time.DayPerWeek).Days();

        public static Time Days(this double value) => (value * Time.HourPerDay).Hours();

        public static Time Hours(this double value) => (value * Time.MinutePerHour).Minutes();

        public static Time Minutes(this double value) => (value*Time.SecondPerMinute).Seconds();

        public static Time Seconds(this double value) => new Time(value);

        public static Time Milliseconds(this double value) => new Time(value / Time.MillisecondPerSecond);


        public static Time Years(this int value) => ((double)value).Years();

        public static Time Months(this int value) => ((double)value).Months();

        public static Time Weeks(this int value) => ((double)value).Weeks();

        public static Time Days(this int value) => ((double)value).Days();

        public static Time Hours(this int value) => ((double)value).Hours();

        public static Time Minutes(this int value) => ((double)value).Minutes();

        public static Time Seconds(this int value) => ((double)value).Seconds();

        public static Time Milliseconds(this int value) => ((double)value).Milliseconds();
    }
}