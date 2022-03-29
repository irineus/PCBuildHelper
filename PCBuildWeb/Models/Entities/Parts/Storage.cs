using PCBuildWeb.Models.Entities.Bases;
using PCBuildWeb.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace PCBuildWeb.Models.Entities.Parts
{
    public class Storage : ComputerPart
    {
        [Display(Name = "Type")]
        [Required(ErrorMessage = "{0} is required")]
        [EnumDataType(typeof(StorageType))]
        public StorageType Type { get; set; }
        [Display(Name = "Size (GB)")]
        [Required(ErrorMessage = "{0} is required")]
        [Range(100, 6000, ErrorMessage = "{0} should be a value between {1} and {2}")]
        public int Size { get; set; }
        [Display(Name = "Speed (MB/s)")]
        [Required(ErrorMessage = "{0} is required")]
        [Range(100, 10000, ErrorMessage = "{0} should be a value between {1} and {2}")]
        public int Speed { get; set; }
        [Display(Name = "Includes Heatsink?")]
        public bool IncludesHeatsink { get; set; }
        [Display(Name = "Heatsink Thickness")]
        public double HeatsinkThickness { get; set; }

    }
}
