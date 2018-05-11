using System;

namespace Celestial.Units
{
    public struct ElectricalCapacitance
    {
        private readonly double _farads;

        public double Farads => _farads;

        public ElectricalCapacitance(double farads)
        {
            _farads = farads;
        }

        public ElectricalCapacitance(ElectricCurrent a, Time t, Voltage v)
        {
            _farads = a.TotalAmperes * t.TotalSeconds / v.TotalVolts;
        }

        public ElectricalCapacitance(Energy e, Voltage v)
        {
            _farads = e.TotalJoules / (v.TotalVolts * v.TotalVolts);
        }

        public ElectricalCapacitance(Power p, Time t, Voltage v)
        {
            _farads = p.TotalWatts * t.TotalSeconds / (v.TotalVolts * v.TotalVolts);
        }

        public ElectricalCapacitance(ElectricCharge c, Energy e)
        {
            _farads = c.TotalCoulombs * c.TotalCoulombs / e.TotalJoules;
        }

        public ElectricalCapacitance(ElectricCharge c, Force f, Length l)
        {
            _farads = c.TotalCoulombs * c.TotalCoulombs / (f.TotalNewtons * l.TotalMeters);
        }

        public ElectricalCapacitance(ElectricCharge c, Time t, Area a, Mass m)
        {
            _farads = c.TotalCoulombs * c.TotalCoulombs * t.TotalSeconds * t.TotalSeconds /
                      (a.TotalSquareMeters * m.TotalKilograms);
        }

        public ElectricalCapacitance(ElectricCurrent a, Time t, Area area, Mass m)
        {
            _farads = a.TotalAmperes * a.TotalAmperes * Math.Pow(t.TotalSeconds, 4) /
                      (area.TotalSquareMeters * m.TotalKilograms);
        }

        public ElectricalCapacitance(Time t, ElectricalInduction h)
        {
            _farads = t.TotalSeconds*t.TotalSeconds/h.TotalHenrys;
        }



        public double TotalFarads => _farads;

        public static ElectricCharge operator *(Voltage v, ElectricalCapacitance f) => new ElectricCharge(v.TotalVolts * f.TotalFarads);
        public static ElectricCharge operator *(ElectricalCapacitance f, Voltage v) => v *f;
        public static ElectricalResistance operator /(Time t, ElectricalCapacitance f) => new ElectricalResistance(t.TotalSeconds / f.TotalFarads);

        public override string ToString() => $"{TotalFarads}[F]";

    }

    public static class ElectricalCapacitanceExtensions
    {
        public static ElectricalCapacitance Farads(this double value) => new ElectricalCapacitance(value);

        public static ElectricalCapacitance Farads(this int value) => ((double)value).Farads();
    }
}
