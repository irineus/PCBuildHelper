using PCBuildWeb.Models.Entities.Bases;
using PCBuildWeb.Models.Entities.Properties;
using System.ComponentModel.DataAnnotations;

namespace PCBuildWeb.Models.Entities.Parts
{
    public class WC_CPU_Block : ComputerPart
    {
        public WC_CPU_Block()
        {
            this.SupportedCPUSockets = new HashSet<CPUSocket>();
        }

        [Display(Name = "Supported CPU Sockets")]
        public virtual ICollection<CPUSocket> SupportedCPUSockets { get; set; }
    }
}
