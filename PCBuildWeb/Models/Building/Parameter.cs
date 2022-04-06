using Microsoft.AspNetCore.Mvc;
using PCBuildWeb.Models.Entities.Properties;
using PCBuildWeb.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PCBuildWeb.Models.Building
{
    public class Parameter
    {
        public Parameter()
        {
        }

        [Display(Name = "Level")]
        [Required(ErrorMessage = "{0} is required")]
        [Range(1, 50, ErrorMessage = "{0} should be a value between {1} and {2}")]
        public int CurrentLevel { get; set; }
        [Display(Name = "Budget")]
        [Required(ErrorMessage = "{0} is required")]
        [DataType(DataType.Currency)]
        [Range(1, 100000, ErrorMessage = "{0} should be a value between {1} and {2}")]
        public int Budget { get; set; }
        [ForeignKey("ManufacturerId")]
        public Manufacturer? PreferredManufacturer { get; set; }
        [Display(Name = "Preferred Manufacturer")]
        public int? ManufacturerId { get; set; }
        [Display(Name = "Target Score")]
        [Range(1, 100000, ErrorMessage = "{0} should be a value between {1} and {2}")]
        public int? TargetScore { get; set; }
        [Display(Name = "Target Memory Size (GB)")]
        [Range(2, 512, ErrorMessage = "{0} should be a value between {1} and {2}")]
        public int? TargetMemorySize { get; set; }
        [Display(Name = "Target CPU Clock (MHz)")]
        [Range(2000, 6000, ErrorMessage = "{0} should be a value between {1} and {2}")]
        public int? TargetCPUClock { get; set; }
        [Display(Name = "Target GPU Clock (MHz)")]
        [Range(500, 3000, ErrorMessage = "{0} should be a value between {1} and {2}")]
        public int? TargetGPUClock { get; set; }
        [Display(Name = "Memory Channels")]
        [Required(ErrorMessage = "{0} is required")]
        [Range(1, 4, ErrorMessage = "{0} should be a value between {1} and {2}")]
        public int MemoryChannels { get; set; }
        [Display(Name = "Must have AIO Watercooler?")]
        public bool MustHaveAIOCooler { get; set; }
        [Display(Name = "Must have Dual GPUs?")]
        public bool MustHaveDualGPU { get; set; }
        [Display(Name = "Must have Custom Watercooler?")]
        public bool MustHaveCustomWC { get; set; }
        [Display(Name = "Enable Open Bench Cases?")]
        public bool EnableOpenBench { get; set; }
        [Display(Name = "Building Type")]
        [BindProperty]
        [EnumDataType(typeof(BuildTypeEnum))]
        public BuildTypeEnum BuildType { get; set; }
        public List<Priority> PartPriorities { get; set; } = new List<Priority>();
    }
}
