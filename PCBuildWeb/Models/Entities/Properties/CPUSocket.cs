using PCBuildWeb.Models.Entities.Bases;
using PCBuildWeb.Models.Entities.Parts;

namespace PCBuildWeb.Models.Entities.Properties
{
    public class CPUSocket : PartProperty
    {
        public CPUSocket()
        {
            //this.UsedByThisCoolers = new HashSet<CPUCooler>();
            //this.UsedByThisCPUBlocks = new HashSet<WC_CPU_Block>();
        }

        public virtual ICollection<CPUCooler> CPUCoolers { get; set; }
        public virtual ICollection<WC_CPU_Block> WC_CPU_Blocks { get; set; }
        public virtual ICollection<CPU> CPUs { get; set; }
        public virtual ICollection<Motherboard> Motherboards { get; set; }
    }
}
