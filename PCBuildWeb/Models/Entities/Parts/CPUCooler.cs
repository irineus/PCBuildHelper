using PCBuildWeb.Models.Entities.Bases;
using PCBuildWeb.Models.Entities.Properties;

namespace PCBuildWeb.Models.Entities.Parts
{
    public class CPUCooler : ComputerPart
    {
        public bool WaterCooler { get; set; }
        public bool Passive { get; set; }
        public double AirFlow { get; set; }
        public List<CPUSocket> CompatibleSockets { get; set; }
        public int Height { get; set; }
        public int RadiatorSize { get; set; }
        public int RadiatorSlots { get; set; }
        public double RadiatorThickness { get; set; }
        public double AirPressure { get; set; }
    }
}
