using PCBuildWeb.Models.Entities.Bases;
using PCBuildWeb.Models.Entities.Properties;

namespace PCBuildWeb.Models.Entities.Parts
{
    public class WC_CPU_Block : ComputerPart
    {
        public List<CPUSocket> SupportedCPUSockets { get; set; }
    }
}
