using System;

namespace Celestial.Units
{
    public struct Velocity : IEquatable<Velocity>, IComparable<Velocity>
    {
        public Speed Speed { get; }
        public Angle2 Angle { get; }

        public Speed X => (Speed.TotalMetersPerSecond
                            * Math.Cos(Angle.Vertical.TotalRadians)
                            * Math.Cos(Angle.Horizontal.TotalRadians)).MetersPerSecond();
        public Speed Y => (Speed.TotalMetersPerSecond
                            * Math.Cos(Angle.Vertical.TotalRadians)
                            * Math.Sin(Angle.Horizontal.TotalRadians)).MetersPerSecond();
        public Speed Z => (Speed.TotalMetersPerSecond
                           * Math.Sin(Angle.Vertical.TotalRadians)).MetersPerSecond();

        public static Velocity operator +(Velocity a, Velocity b) => new Velocity(a.X + b.X, a.Y + b.Y, a.Z + b.Z);

        public static Velocity operator /(Velocity a, int b) => new Velocity(a.X / b, a.Y / b, a.Z / b);

        public static bool operator ==(Velocity a, Velocity b) => a.Speed == b.Speed && a.Angle == b.Angle;

        public static bool operator !=(Velocity a, Velocity b) => !(a == b);

        public double TotalMetersPerSecond => Speed.TotalMetersPerSecond;
        public static Velocity Zero => new Velocity(0.MetersPerSecond(), 0.MetersPerSecond(), 0.MetersPerSecond());

        public Velocity(Speed speed, Angle2 angle)
        {
            Speed = speed;
            Angle = angle;
        }

        public Velocity(Speed x, Speed y, Speed z)
        {
            var xyx = Math.Sqrt(Math.Pow(x.TotalMetersPerSecond, 2) 
                + Math.Pow(y.TotalMetersPerSecond, 2) 
                + Math.Pow(z.TotalMetersPerSecond, 2));
            var xy = Math.Sqrt(Math.Pow(x.TotalMetersPerSecond, 2)
                               + Math.Pow(y.TotalMetersPerSecond, 2));
            Speed = xyx.MetersPerSecond();
            var angleH = Math.Atan2(y.TotalMetersPerSecond, x.TotalMetersPerSecond);
            var angleV = Math.Atan2(z.TotalMetersPerSecond, xy);

            Angle = new Angle2(angleH, angleV);

            
        }

        public Velocity(double metersPerSecond, Angle2 angle) 
            : this(metersPerSecond.MetersPerSecond(),angle)
        {

        }

        public Velocity(double metersPerSecond, Angle horizontalAngle, Angle veriticalAngle)
            : this(metersPerSecond, new Angle2(horizontalAngle, veriticalAngle))
        {

        }

        public Velocity(Speed speed, Angle horizontalAngle, Angle veriticalAngle)
            : this(speed, new Angle2(horizontalAngle, veriticalAngle))
        {

        }

        public Velocity(double metersPerSecond, double horizontalAngle = 0, double veriticalAngle = 0)
            : this(metersPerSecond, new Angle2(horizontalAngle, veriticalAngle))
        {

        }

        public static Length3 operator *(Velocity v, Time t) => new Length3(v.X*t, v.Y*t, v.Z*t);

        public static Acceleration operator /(Velocity v, Time t)
            => new Acceleration(v.TotalMetersPerSecond/t.TotalSeconds, v.Angle);

        public override bool Equals(object obj)
        {
            if (!(obj is Velocity))
                return false;

            return Equals((Velocity)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Speed.GetHashCode() * 397) ^ Angle.GetHashCode();
            }
        }

        public bool Equals(Velocity other) => Speed == other.Speed;

        public int CompareTo(Velocity other) => Speed.CompareTo(other.Speed);


        public string ToString(string format) => $"({X.ToString(format)}, {Y.ToString(format)}, {Z.ToString(format)})";

        public override string ToString() => ToString("");

    }

    public static class VelocityExtensions
    {
        public static Velocity MetersPerSecond(this double value, Angle angleH, Angle angleV) => new Velocity(value, angleH, angleV);

        public static Velocity MetersPerSecond(this double value, double angleH, double angleV) => new Velocity(value, angleH, angleV);

        public static Velocity MetersPerSecond(this int value, Angle angleH, Angle angleV) => ((double)value).MetersPerSecond(angleH, angleV);

        public static Velocity MetersPerSecond(this int value, double angleH, double angleV) => ((double)value).MetersPerSecond(angleH, angleV);

        internal static Velocity AtAngle(this Speed speed, Angle2 angle)
        {
            return new Velocity(speed, angle);
        }
    }
}
