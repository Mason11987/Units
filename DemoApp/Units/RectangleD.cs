using Microsoft.Xna.Framework;

namespace Celestial.Units
{
    public struct RectangleD
    {
        private readonly double _x;
        private readonly double _y;
        private readonly double _width;
        private readonly double _height;

        public double Top => _y;
        public double Left => _x;
        public double Bottom => _y + _height;
        public double Right => _x + _width;

        public double Width => _width;
        public double Height => _height;

        public RectangleD(double x, double y, double width, double height)
        {
            _x = x;
            _y = y;
            _width = width;
            _height = height;
        }

        public bool Contains(Vector3 pt)
        {
            return Contains(new Vector2(pt.X, pt.Y));
        }

        internal bool Contains(Length3 pt)
        {
            return Contains(new Vector2((float)pt.X.TotalMeters, (float)pt.Y.TotalMeters));
        }

        private bool Contains(Vector2 pt)
        {
            return pt.X > Left && pt.X < Right && pt.Y > Top && pt.Y < Bottom;
        }

        internal RectangleD Shrink(double amt)
        {
            return new RectangleD(_x + amt/2.0, _y + amt/2.0, _width - amt, _height - amt);
        }

        public override string ToString()
        {
            return $"X={Left},Y={Top},Width={Width},Height={Height}";
        }
    }
}
