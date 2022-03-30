using PCBuildWeb.Models.Entities.Bases;
using PCBuildWeb.Models.Entities.Properties;
using PCBuildWeb.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PCBuildWeb.Models.Entities.Parts
{
    public class GPU : ComputerPart
    {
        public GPU()
        {
            this.PowerConnectors = new HashSet<PowerConnector>();
        }

        [Display(Name = "Chipset Brand")]
        [Required(ErrorMessage = "{0} is required")]
        [EnumDataType(typeof(GPUChipsetBrand))]
        public GPUChipsetBrand ChipsetBrand { get; set; }
        [ForeignKey("GPUChipsetId")]
        public GPUChipset GPUChipset { get; set; }
        [Display(Name = "GPU Chipset")]
        public int GPUChipsetId { get; set; }
        [Display(Name = "Watercooled?")]
        public bool IsWaterCooled { get; set; }
        [Display(Name = "Ranking Score")]
        [Required(ErrorMessage = "{0} is required")]
        [Range(100, 50000, ErrorMessage = "{0} should be a value between {1} and {2}")]
        public int RankingScore { get; set; }
        [Display(Name = "VRAM (GB)")]
        [Required(ErrorMessage = "{0} is required")]
        [Range(2, 36, ErrorMessage = "{0} should be a value between {1} and {2}")]
        public int VRAM { get; set; }
        [Display(Name = "Min Core Frequency")]
        [Required(ErrorMessage = "{0} is required")]
        [Range(100, 5000, ErrorMessage = "{0} should be a value between {1} and {2}")]
        public int MinCoreFrequency { get; set; }
        [Display(Name = "Base Core Frequency")]
        [Required(ErrorMessage = "{0} is required")]
        [Range(100, 5000, ErrorMessage = "{0} should be a value between {1} and {2}")]
        public int BaseCoreFrequency { get; set; }
        [Display(Name = "Overclocked Core Frequency")]
        [Required(ErrorMessage = "{0} is required")]
        [Range(100, 5000, ErrorMessage = "{0} should be a value between {1} and {2}")]
        public int OverclockedCoreFrequency { get; set; }
        [Display(Name = "Max Core Frequency")]
        [Required(ErrorMessage = "{0} is required")]
        [Range(100, 5000, ErrorMessage = "{0} should be a value between {1} and {2}")]
        public int MaxCoreFrequency { get; set; }
        [Display(Name = "Min Mem Frequency")]
        [Required(ErrorMessage = "{0} is required")]
        [Range(-500, 5000, ErrorMessage = "{0} should be a value between {1} and {2}")]
        public int MinMemFrequency { get; set; }
        [Display(Name = "Base Mem Frequency")]
        [Required(ErrorMessage = "{0} is required")]
        [Range(500, 5000, ErrorMessage = "{0} should be a value between {1} and {2}")]
        public int BaseMemFrequency { get; set; }
        [Display(Name = "Overclocked Mem Frequency")]
        [Required(ErrorMessage = "{0} is required")]
        [Range(500, 5000, ErrorMessage = "{0} should be a value between {1} and {2}")]
        public int OverclockedMemFrequency { get; set; }
        [Display(Name = "Max Mem Frequency")]
        [Required(ErrorMessage = "{0} is required")]
        [Range(500, 5000, ErrorMessage = "{0} should be a value between {1} and {2}")]
        public int MaxMemFrequency { get; set; }
        [Display(Name = "Length")]
        [Required(ErrorMessage = "{0} is required")]
        [Range(100, 500, ErrorMessage = "{0} should be a value between {1} and {2}")]
        public int Length { get; set; }
        [Display(Name = "Consumption (Watts)")]
        [Required(ErrorMessage = "{0} is required")]
        [Range(10, 1000, ErrorMessage = "{0} should be a value between {1} and {2}")]
        public int Wattage { get; set; }
        [ForeignKey("MultiGPUId")]
        public MultiGPU? MultiGPU { get; set; }
        [Display(Name = "Multi GPU Type")]
        public int? MultiGPUId { get; set; }
        [Display(Name = "Slot Size")]
        [Required(ErrorMessage = "{0} is required")]
        [Range(1.00, 4.00, ErrorMessage = "{0} should be a value between {1} and {2}")]
        public double SlotSize { get; set; }
        [Display(Name = "Power Connector List")]
        public virtual ICollection<PowerConnector>? PowerConnectors { get; set; }
        [Display(Name = "Score to Value Ratio")]
        [Required(ErrorMessage = "{0} is required")]
        [Range(1.00, 20.00, ErrorMessage = "{0} should be a value between {1} and {2}")]
        public double ScoreToValueRatio { get; set; }
        [Display(Name = "Single GPU Score")]
        [Required(ErrorMessage = "{0} is required")]
        [Range(100, 50000, ErrorMessage = "{0} should be a value between {1} and {2}")]
        public int SingleGPUScore { get; set; }
        [Display(Name = "Double GPU Score")]
        [Required(ErrorMessage = "{0} is required")]
        [Range(0, 50000, ErrorMessage = "{0} should be a value between {1} and {2}")]
        public int? DualGPUScore { get; set; }
        [Display(Name = "Dual GPU Performance Increased")]
        [Required(ErrorMessage = "{0} is required")]
        [Range(0.00, 3.00, ErrorMessage = "{0} should be a value between {1} and {2}")]
        public double? DualGPUPerformanceIncrease { get; set; }
        [Display(Name = "Overclocked Single GPU Score")]
        [Required(ErrorMessage = "{0} is required")]
        [Range(1000, 50000, ErrorMessage = "{0} should be a value between {1} and {2}")]
        public int? OverclockedSingleGPUScore { get; set; }
        [Display(Name = "Overclocked Dual GPU Score")]
        [Required(ErrorMessage = "{0} is required")]
        [Range(0, 50000, ErrorMessage = "{0} should be a value between {1} and {2}")]
        public int? OverclockedDualGPUScore { get; set; }


    }
}
