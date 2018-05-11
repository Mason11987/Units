using System;
using Celestial.KDTree;
using Microsoft.Xna.Framework;

namespace Celestial.Units
{
    public struct Length3 : IEquatable<Length3>, IPositionable
    {
        private readonly Length _x;
        private readonly Length _y;
        private readonly Length _z;

        public Length X => _x;
        public Length Y => _y;
        public Length Z => _z;

        public Length Length
            =>
                Math.Sqrt(Math.Pow(_x.TotalMeters, 2) + Math.Pow(_y.TotalMeters, 2) + Math.Pow(_z.TotalMeters, 2))
                    .Meters();

        public Length3(Length x, Length y, Length z)
        {
            _x = x;
            _y = y;
            _z = z;
        }

        public Length3(double x, double y, double z)
        {
            _x = new Length(x);
            _y = new Length(y);
            _z = new Length(z);
        }

        public Length3(Length val) : this(val, val, val)
        {

        }

        public static Length3 Zero = new Length3(Length.Zero);

        public static Vector3 operator /(Length3 a, Length b) => new Vector3((float)(a.X / b), (float)(a.Y / b), (float)(a.Z / b));
        public static Vector3 operator /(Length3 a, Length3 b) => new Vector3((float)(a.X / b.X), (float)(a.Y / b.Y), (float)(a.Z / b.Z));
        public static Length3 operator /(Vector3 a, Length3 b) => new Length3(a.X / b.X, a.Y / b.Y, a.Z / b.Z);
        public static Length3 operator *(Length3 a, Vector3 b) => new Length3(a.X * b.X, a.Y * b.Y, a.Z * b.Z);
        public static Length3 operator /(Length3 a, Vector3 b) => new Length3(a.X / b.X, a.Y / b.Y, a.Z / b.Z);
        public static Length3 operator /(Length3 a, float b) => new Length3(a.X / b, a.Y / b, a.Z / b);
        public static Length3 operator *(Length3 a, float b) => new Length3(a.X * b, a.Y * b, a.Z * b);

        public static Length3 operator +(Length3 a, Length3 b) => new Length3(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        public static Length3 operator -(Length3 a, Length3 b) => new Length3(a.X - b.X, a.Y - b.Y, a.Z - b.Z);

        public Vector3 TotalKilometers()
        {
            return new Vector3((float)X.TotalKilometers, (float)Y.TotalKilometers, (float)Z.TotalKilometers);
        }


        public bool Equals(Length3 other)
        {
            return X == other.X && Y == other.Y && Z == other.Z;
        }

        public static bool operator ==(Length3 a, Length3 b) => a.Equals(b);
        public static bool operator !=(Length3 a, Length3 b) => !a.Equals(b);
        public static Length3 operator -(Length3 a) => new Length3(-a.X, -a.Y, -a.Z);

        public string ToString(string format) => $"({X.ToString(format)}, {Y.ToString(format)}, {Z.ToString(format)})";
        public override string ToString() => $"({X}, {Y}, {Z})";

        public Angle2 Angle => new Angle2(new Angle(Math.Atan2(Y.TotalMeters, X.TotalMeters)), new Angle(0));
        public Length XYLength => Math.Sqrt(X.TotalMeters * X.TotalMeters + Y.TotalMeters * Y.TotalMeters).Meters();

        public Vector2 Position => ToVector2();

        public Vector2 ToVector2()
        {
            return new Vector2((float)X.TotalMeters, (float)Y.TotalMeters);
        }

        public Length DistanceTo(Length3 loc)
        {
            return Math.Sqrt(Math.Pow(loc.X.TotalMeters - X.TotalMeters,2) + Math.Pow(loc.Y.TotalMeters - Y.TotalMeters,2)).Meters();
        }
    }

    static class Distance3Extensions
    {
        public static Length3 Kilometers(this Vector3 value) => new Length3(((double)value.X).Kilometers(), ((double)value.Y).Kilometers(), ((double)value.Z).Kilometers());

    }
}
