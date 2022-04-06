using PCBuildWeb.Models.Entities.Bases;
using PCBuildWeb.Models.Entities.Parts;

namespace PCBuildWeb.Models.Entities.Properties
{
    public class CPUSocket : PartProperty, ICloneable
    {
        public CPUSocket()
        {
        }

        public List<CPUCooler> CPUCoolers { get; set; } = new List<CPUCooler>();
        public List<WC_CPU_Block> WC_CPU_Blocks { get; set; } = new List<WC_CPU_Block>();
        public List<CPU> CPUs { get; set; } = new List<CPU>();
        public List<Motherboard> Motherboards { get; set; } = new List<Motherboard>();

        public object Clone()
        {
            var cpuSeries = (CPUSeries)MemberwiseClone();
            return cpuSeries;
        }
    }
}
