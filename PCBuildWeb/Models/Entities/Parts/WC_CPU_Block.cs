using PCBuildWeb.Models.Entities.Bases;
using PCBuildWeb.Models.Entities.Properties;
using System.ComponentModel.DataAnnotations;

namespace PCBuildWeb.Models.Entities.Parts
{
    public class WC_CPU_Block : ComputerPart
    {
        public WC_CPU_Block()
        {
        }

        [Display(Name = "Supported CPU Sockets")]
        public List<CPUSocket> CPUSockets { get; set; } = new List<CPUSocket>();
    }
}
