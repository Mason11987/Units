namespace Celestial.Units
{
    public struct ElectricalConductance
    {
        private readonly double _siemens;

        public double Siemens => _siemens;

        public ElectricalConductance(double siemens)
        {
            _siemens = siemens;
        }

        public double TotalSiemens => _siemens;


        public ElectricalResistance Ohms() => (1/TotalSiemens).Ohms();

        public override string ToString() => $"{TotalSiemens}[S]";
    }

    public static class ElectricalConductanceExtensions
    {
        public static ElectricalConductance Siemens(this double value) => new ElectricalConductance(value);

        public static ElectricalConductance Siemens(this int value) => ((double)value).Siemens();
    }
}
