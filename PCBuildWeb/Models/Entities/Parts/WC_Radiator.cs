using PCBuildWeb.Models.Entities.Bases;
using PCBuildWeb.Models.Entities.Properties;

namespace PCBuildWeb.Models.Entities.Parts
{
    public class WC_Radiator : ComputerPart
    {
        public int AirFlow { get; set; }
        public int RadiatorSize { get; set; }
        public int RadiatorSlots { get; set; }
        public double RadiatorThickness { get; set; }
        public double AirPresure { get; set; }
    }
}
