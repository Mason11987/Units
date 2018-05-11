using Celestial.Units;
using System;

namespace Celestial.Units
{
    internal class TravelPath
    {
        private double totalMetersPerSecond;

        public Length3 From { get; private set; }
        public Length3 To { get; private set; }

        private Length3 OriginVector { get; set; }
        public Length Length { get; private set; }
        public Angle2 Angle { get; private set; }

        public TravelPath(Length3 from, Length3 to)
        {
            From = from;
            To = to;
            OriginVector = To - From;
            Length = OriginVector.XYLength;
            Angle = OriginVector.Angle;
        }
    }
}