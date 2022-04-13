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
            var cpuSocket = (CPUSocket)MemberwiseClone();
            //if (CPUCoolers is not null)
            //{
            //    foreach (CPUCooler cpuCooler in CPUCoolers)
            //    {
            //        cpuSocket.CPUCoolers.Add((CPUCooler)cpuCooler.Clone());
            //    }
            //}
            //if (WC_CPU_Blocks is not null)
            //{
            //    foreach (WC_CPU_Block wcCPUBlock in WC_CPU_Blocks)
            //    {
            //        cpuSocket.WC_CPU_Blocks.Add((WC_CPU_Block)wcCPUBlock.Clone());
            //    }
            //}
            //if (CPUs is not null)
            //{
            //    foreach (CPU cpu in CPUs)
            //    {
            //        cpuSocket.CPUs.Add((CPU)cpu.Clone());
            //    }
            //}
            //if (Motherboards is not null)
            //{
            //    foreach (Motherboard mobo in Motherboards)
            //    {
            //        cpuSocket.Motherboards.Add((Motherboard)mobo.Clone());
            //    }
            //}
            return cpuSocket;
        }
    }
}
