using PCBuildWeb.Models.Entities.Bases;
using PCBuildWeb.Models.Entities.Properties;
using PCBuildWeb.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PCBuildWeb.Models.Entities.Parts
{
    public class Motherboard : ComputerPart, ICloneable
    {
        public Motherboard()
        {
        }
        
        [ForeignKey("MoboChipsetId")]
        public MoboChipset MoboChipset { get; set; } = new MoboChipset();
        [Display(Name = "Chipset")]
        public int MoboChipsetId { get; set; }
        [ForeignKey("CPUSocketId")]
        public CPUSocket CPUSocket { get; set; } = new CPUSocket();
        [Display(Name = "Socket")]
        public int CPUSocketId { get; set; }
        [ForeignKey("MoboSizeId")]
        public MoboSize Size { get; set; } = new MoboSize();
        [Display(Name = "Mobo Form Factor")]
        public int MoboSizeId { get; set; }
        [Display(Name = "Max Ram Speed")]
        [Required(ErrorMessage = "{0} is required")]
        [Range(2000, 6000, ErrorMessage = "{0} should be a value between {1} and {2}")]
        public int MaxRamSpeed { get; set; }
        [Display(Name = "Multi GPU Support")]
        public List<MultiGPU> MultiGPUs { get; } = new List<MultiGPU>();
        [Display(Name = "Dual GPU Max Slot Size")]
        [Required(ErrorMessage = "{0} is required")]
        [Range(2000, 6000, ErrorMessage = "{0} should be a value between {1} and {2}")]
        public int DualGPUMaxSlotSize { get; set; }
        [Display(Name = "Overclockable?")]
        public bool Overclockable { get; set; }
        [Display(Name = "M.2 Slots")]
        [Required(ErrorMessage = "{0} is required")]
        [Range(0, 6, ErrorMessage = "{0} should be a value between {1} and {2}")]
        public int M2Slots { get; set; }
        [Display(Name = "M.2 (With Heatsink) Slots")]
        [Required(ErrorMessage = "{0} is required")]
        [Range(0, 6, ErrorMessage = "{0} should be a value between {1} and {2}")]
        public int M2SlotsSupportingHeatsinks { get; set; }
        [Display(Name = "RAM Slots")]
        [Required(ErrorMessage = "{0} is required")]
        [Range(2, 8, ErrorMessage = "{0} should be a value between {1} and {2}")]
        public int RamSlots { get; set; }
        [Display(Name = "SATA Slots")]
        [Required(ErrorMessage = "{0} is required")]
        [Range(2, 6, ErrorMessage = "{0} should be a value between {1} and {2}")]
        public int SATASlots { get; set; }
        [Display(Name = "CPU Block Included?")]
        public bool IncludesCPUBlock { get; set; }
        [Display(Name = "Default RAM Speed")]
        [Required(ErrorMessage = "{0} is required")]
        [Range(2000, 6000, ErrorMessage = "{0} should be a value between {1} and {2}")]
        public int DefaultRamSpeed { get; set; }
        [Display(Name = "Min RAM Speed")]
        [Required(ErrorMessage = "{0} is required")]
        [Range(2000, 6000, ErrorMessage = "{0} should be a value between {1} and {2}")]
        public int MinRamSpeed { get; set; }
        
        public new object Clone()
        {
            var moboClone = (Motherboard)MemberwiseClone();
            if (MoboChipset is not null)
            {
                moboClone.MoboChipset = (MoboChipset)MoboChipset.Clone();
            }
            if (CPUSocket is not null)
            {
                moboClone.CPUSocket = (CPUSocket)CPUSocket.Clone();
            }
            if (Size is not null)
            {
                moboClone.Size = (MoboSize)Size.Clone();
            }
            if (MultiGPUs is not null)
            {
                foreach (MultiGPU multiGPU in MultiGPUs)
                {
                    moboClone.MultiGPUs.Add((MultiGPU)multiGPU.Clone());
                }
            }
            return moboClone;
        }
    }
}
