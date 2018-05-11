namespace Celestial.Units
{
    public struct ElectricalResistance
    {
        private readonly double _ohms;

        public double Ohms => _ohms;

        public ElectricalResistance(double ohms)
        {
            _ohms = ohms;
        }

        public ElectricalResistance(Power p, ElectricCurrent a)
        {
            _ohms = p.TotalWatts / (a.TotalAmperes * a.TotalAmperes);
        }

        public ElectricalResistance(Voltage v, Power p)
        {
            _ohms = v.TotalVolts * v.TotalVolts / (p.TotalWatts);
        }

        public ElectricalResistance(Energy e, Time t, ElectricCharge c)
        {
            _ohms = e.TotalJoules * t.TotalSeconds / (c.TotalCoulombs * c.TotalCoulombs);
        }

        public ElectricalResistance(Energy e, Time t, ElectricCurrent a) : this(e / t, a)
        { }



        public double TotalOhms => _ohms;

        public static ElectricalCapacitance operator /(Time t, ElectricalResistance o) => new ElectricalCapacitance(t.TotalSeconds / o.TotalOhms);
        public static ElectricalInduction operator *(ElectricalResistance o, Time t) => new ElectricalInduction(o.TotalOhms * t.TotalSeconds);
        public static ElectricalInduction operator *(Time t, ElectricalResistance o) => o*t;

        public ElectricalConductance Siemens() => (1 / TotalOhms).Siemens();


        public override string ToString() => $"{TotalOhms}[Ohm]";

    }

    public static class ElectricalResistanceExtensions
    {
        public static ElectricalResistance Ohms(this double value) => new ElectricalResistance(value);

        public static ElectricalResistance Ohms(this int value) => ((double)value).Ohms();
    }
}
