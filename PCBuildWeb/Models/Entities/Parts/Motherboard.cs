using PCBuildWeb.Models.Entities.Bases;
using PCBuildWeb.Models.Entities.Properties;
using PCBuildWeb.Models.Enums;

namespace PCBuildWeb.Models.Entities.Parts
{
    public class Motherboard : ComputerPart
    {
        public MoboChipset Chipset { get; set; }
        public CPUSocket CPUSocket { get; set; }
        public MoboSize Size { get; set; }
        public int MaxRamSpeed { get; set; }
        public List<MultiGPU> MultiGPUSupport { get; set; }
        public int DualGPUMaxSlotSize { get; set; }
        public bool Overclockable { get; set; }
        public int M2Slots { get; set; }
        public int M2SlotsSupportingHeatsinks { get; set; }
        public int RamSlots { get; set; }
        public int SATASlots { get; set; }
        public bool IncludesCPUBlock { get; set; }
        public int DefaultRamSpeed { get; set; }
    }
}
