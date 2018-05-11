namespace Celestial.Units
{
    public struct ElectricalInduction
    {
        private readonly double _henrys;

        public double Henrys => _henrys;

        public ElectricalInduction(double henrys)
        {
            _henrys = henrys;
        }

        public static MagneticFlux operator *(ElectricalInduction h, ElectricCurrent a) => new MagneticFlux(h.TotalHenrys * a.TotalAmperes);
        public static MagneticFlux operator *(ElectricCurrent a, ElectricalInduction h) => h*a;

        public double TotalHenrys => _henrys;

        public override string ToString() => $"{TotalHenrys}[H]";

    }

    public static class ElectricalnductionExtensions
    {
        public static ElectricalInduction Henrys(this double value) => new ElectricalInduction(value);

        public static ElectricalInduction Henrys(this int value) => ((double)value).Henrys();
    }
}
