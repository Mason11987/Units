namespace Celestial.Units
{
    public struct DoubleUnit: IUnitConvertable
    {
        private readonly double _units;
        private readonly string _display;

        public double Units => _units;

        public DoubleUnit(double units, string displayString = "")
        {
            _units = units;
            _display = displayString;
        }


        public double TotalUnits => _units;

        public override string ToString()
        {
            return $"{TotalUnits}{(_display == "" ? "" : "[" + _display + "")}";
        }

        public double ToDouble()
        {
            return _units;
        }
    }

    public static class BuildProgressExtensions
    {
        public static DoubleUnit Units(this double value) => new DoubleUnit(value);
        public static DoubleUnit Units(this double value, string displayString) => new DoubleUnit(value);
    }
}
