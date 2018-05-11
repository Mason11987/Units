namespace Celestial.Units
{
    public struct Angle2
    {

        public Angle Horizontal { get; }
        public Angle Vertical { get; }


        public static bool operator ==(Angle2 a, Angle2 b) => a.Horizontal == b.Horizontal && a.Vertical == b.Vertical;

        public static bool operator !=(Angle2 a, Angle2 b) => !(a == b);

        public bool Equals(Angle2 other)
        {
            return Horizontal.Equals(other.Horizontal) && Vertical.Equals(other.Vertical);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Angle2 && Equals((Angle2)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Horizontal.GetHashCode() * 397) ^ Vertical.GetHashCode();
            }
        }

        public Angle2(Angle horiztonal, Angle vertical)
        {
            Horizontal = horiztonal;
            Vertical = vertical;
        }

        public Angle2(double horiztonal, double vertical)
            : this(new Angle(horiztonal), new Angle(vertical))
        {

        }

        public override string ToString() => $"({Horizontal}, {Vertical})";

        public Angle2 Invert()
        {
            return new Angle2(Horizontal.Invert(), Vertical.Invert());
        }
    }
}
