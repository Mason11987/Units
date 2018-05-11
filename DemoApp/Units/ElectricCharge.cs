namespace Celestial.Units
{
    public struct ElectricCharge
    {
        internal static readonly double ElectronchargePercoulomb = 6.242E18;
        internal static readonly double PlankchargePercoulomb = 5.332E17;

        private readonly double _coulombs;

        public double Coulombs => _coulombs;

        public ElectricCharge(double coulombs)
        {
            _coulombs = coulombs;
        }

        public double TotalCoulombs => _coulombs;
        public double TotalElectronCharge => _coulombs * ElectronchargePercoulomb;
        public double TotalPlankcharge => _coulombs * PlankchargePercoulomb;

        public static Energy operator *(ElectricCharge c, Voltage v) => new Energy(c.TotalCoulombs * v.TotalVolts);
        public static Energy operator *(Voltage v, ElectricCharge c) => c*v;
        public static ElectricalCapacitance operator /(ElectricCharge c, Voltage v) => new ElectricalCapacitance(c.TotalCoulombs / v.TotalVolts);

        public override string ToString() => $"{TotalCoulombs}[C]";

    }

    public static class ChargeExtensions
    {
        public static ElectricCharge Coulombs(this double value) => new ElectricCharge(value);

        public static ElectricCharge Coulombs(this int value) => ((double)value).Coulombs();
    }
}
