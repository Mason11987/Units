using System;

namespace Celestial.Units
{
    public struct Acceleration
    {
        public SpeedRate SpeedRate { get; }
        public Angle2 Angle { get; }

        public SpeedRate X => (SpeedRate.TotalMetersPerSecondPerSecond
                            * Math.Cos(Angle.Vertical.TotalRadians)
                            * Math.Cos(Angle.Horizontal.TotalRadians)).MetersPerSecondPerSecond();
        public SpeedRate Y => (SpeedRate.TotalMetersPerSecondPerSecond
                            * Math.Cos(Angle.Vertical.TotalRadians)
                            * Math.Sin(Angle.Horizontal.TotalRadians)).MetersPerSecondPerSecond();
        public SpeedRate Z => (SpeedRate.TotalMetersPerSecondPerSecond
                           * Math.Sin(Angle.Vertical.TotalRadians)).MetersPerSecondPerSecond();

        public double TotalMetersPerSecondPerSecond => SpeedRate.TotalMetersPerSecondPerSecond;

        public static Acceleration operator -(Acceleration x) => new Acceleration(-x.SpeedRate, x.Angle);
        public static Acceleration operator *(double a, Acceleration b) => (a * b.TotalMetersPerSecondPerSecond).MetersPerSecondPerSecond(b.Angle.Horizontal, b.Angle.Vertical);

        public Acceleration(SpeedRate speedrate, Angle2 angle)
        {
            SpeedRate = speedrate;
            Angle = angle;
        }

        public Acceleration(double metersPerSecondPerSecond, double angleH, double angleV)
            : this(metersPerSecondPerSecond.MetersPerSecondPerSecond(), new Angle2(angleH, angleV))
        {
        }

        public Acceleration(double metersPerSecondPerSecond, Angle angleH, Angle angleV)
            : this(metersPerSecondPerSecond.MetersPerSecondPerSecond(), new Angle2(angleH, angleV))
        {
        }

        public Acceleration(SpeedRate speedRate, Angle angleH, Angle angleV)
            : this(speedRate, new Angle2(angleH, angleV))
        {
        }

        public Acceleration(double metersPerSecondPerSecond, Angle2 angle)
            : this(metersPerSecondPerSecond.MetersPerSecondPerSecond(), angle)
        {
        }


        public Acceleration(SpeedRate x, SpeedRate y, SpeedRate z)
        {
            var xyx = Math.Sqrt(Math.Pow(x.TotalMetersPerSecondPerSecond, 2)
                + Math.Pow(y.TotalMetersPerSecondPerSecond, 2)
                + Math.Pow(z.TotalMetersPerSecondPerSecond, 2));
            var xy = Math.Sqrt(Math.Pow(x.TotalMetersPerSecondPerSecond, 2)
                               + Math.Pow(y.TotalMetersPerSecondPerSecond, 2));
            SpeedRate = xyx.MetersPerSecondPerSecond();
            var angleH = Math.Atan2(y.TotalMetersPerSecondPerSecond, x.TotalMetersPerSecondPerSecond);
            var angleV = Math.Atan2(z.TotalMetersPerSecondPerSecond, xy);

            Angle = new Angle2(angleH, angleV);
        }

        public Acceleration(double a) : this(a.MetersPerSecondPerSecond(), new Angle2(0, 0))
        {
            
        }

        public static Velocity operator *(Acceleration a, Time t) => new Velocity(a.TotalMetersPerSecondPerSecond * t.TotalSeconds,a.Angle);

        public override string ToString() => $"({X}, {Y}, {Z})";

    }

    public static class AccelerationExtensions
    {
        public static Acceleration MetersPerSecondPerSecond(this double value, Angle angleH, Angle angleV) => new Acceleration(value, angleH, angleV);

        public static Acceleration MetersPerSecondPerSecond(this double value, double angleH, double angleV) => new Acceleration(value, angleH, angleV);

        public static Acceleration MetersPerSecondPerSecond(this int value, Angle angleH, Angle angleV) => ((double)value).MetersPerSecondPerSecond(angleH, angleV);

        public static Acceleration MetersPerSecondPerSecond(this int value, double angleH, double angleV) => ((double)value).MetersPerSecondPerSecond(angleH, angleV);

        internal static Acceleration AtAngle(this SpeedRate speedRate, Angle2 angle)
        {
            return new Acceleration(speedRate, angle);
        }
    }

}
