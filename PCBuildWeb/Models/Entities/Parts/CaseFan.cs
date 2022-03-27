using PCBuildWeb.Models.Entities.Bases;

namespace PCBuildWeb.Models.Entities.Parts
{
    public class CaseFan : ComputerPart
    {
        public double AirFlow { get; set; }
        public int Size { get; set; }
        public double AirPressure { get; set; }

    }
}
