using PCBuildWeb.Models.Entities.Properties;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PCBuildWeb.Models.Building
{
    public class Build
    {
        public Build()
        {
        }

        [Display(Name = "Level")]
        [Required(ErrorMessage = "{0} is required")]
        [Range(1, 50, ErrorMessage = "{0} should be a value between {1} and {2}")]
        public int CurrentLevel { get; set; }
        [Display(Name = "Level %")]
        [Required(ErrorMessage = "{0} is required")]
        [Range(1, 99, ErrorMessage = "{0} should be a value between {1} and {2}")]
        public int CurrentLevelPercent { get; set; } = 1;
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
        [Required(ErrorMessage = "{0} is required")]
        [Range(1, 100000, ErrorMessage = "{0} should be a value between {1} and {2}")]
        public int? TargetScore { get; set; }
        [Display(Name = "Target Memory Size (GB)")]
        [Required(ErrorMessage = "{0} is required")]
        [Range(2, 512, ErrorMessage = "{0} should be a value between {1} and {2}")]
        public int? TargetMemorySize { get; set; }
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
        [EnumDataType(typeof(BuildType))]
        public BuildType BuildType { get; set; }
        public ICollection<Component> Components { get; set; }
    }
}
