using PCBuildWeb.Models.Entities.Bases;
using PCBuildWeb.Models.Entities.Properties;
using System.ComponentModel.DataAnnotations;

namespace PCBuildWeb.Models.Entities.Parts
{
    public class WC_CPU_Block : ComputerPart, ICloneable
    {
        public WC_CPU_Block()
        {
        }

        [Display(Name = "Supported CPU Sockets")]
        public List<CPUSocket> CPUSockets { get; set; } = new List<CPUSocket>();

        public new object Clone()
        {
            var wcCPUBlockClone = (WC_CPU_Block)MemberwiseClone();
            if (CPUSockets is not null)
            {
                foreach (CPUSocket cpuSocket in CPUSockets)
                {
                    wcCPUBlockClone.CPUSockets.Add((CPUSocket)cpuSocket.Clone());
                }
            }
            return wcCPUBlockClone;
        }        
    }
}
