using PCBuildWeb.Models.Entities.Bases;
using PCBuildWeb.Models.Entities.Parts;

namespace PCBuildWeb.Models.Entities.Properties
{
    public class CPUSocket : PartProperty
    {
        public CPUSocket()
        {
            this.UsedByThisCoolers = new HashSet<CPUCooler>();
            this.UsedByThisCPUBlocks = new HashSet<WC_CPU_Block>();
        }

        public virtual ICollection<CPUCooler> UsedByThisCoolers { get; set; }
        public virtual ICollection<WC_CPU_Block> UsedByThisCPUBlocks { get; set; }
    }
}
