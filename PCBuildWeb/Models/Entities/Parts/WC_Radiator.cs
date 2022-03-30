using PCBuildWeb.Models.Entities.Bases;
using PCBuildWeb.Models.Entities.Properties;
using System.ComponentModel.DataAnnotations;

namespace PCBuildWeb.Models.Entities.Parts
{
    public class WC_Radiator : ComputerPart
    {
        [Display(Name = "Air Flow")]
        [Required(ErrorMessage = "{0} is required")]
        [Range(50, 200, ErrorMessage = "{0} should be a value between {1} and {2}")]
        public int AirFlow { get; set; }
        [Display(Name = "Size (mm)")]
        [Required(ErrorMessage = "{0} is required")]
        [Range(120, 480, ErrorMessage = "{0} should be a value between {1} and {2}")]
        public int RadiatorSize { get; set; }
        [Display(Name = "Slots")]
        [Required(ErrorMessage = "{0} is required")]
        [Range(1, 4, ErrorMessage = "{0} should be a value between {1} and {2}")]
        public int RadiatorSlots { get; set; }
        [Display(Name = "Thickness")]
        [Required(ErrorMessage = "{0} is required")]
        [Range(0.1, 0.5, ErrorMessage = "{0} should be a value between {1} and {2}")]
        public double RadiatorThickness { get; set; }
        [Display(Name = "Air Pressure")]
        [Required(ErrorMessage = "{0} is required")]
        [Range(1.00, 5.00, ErrorMessage = "{0} should be a value between {1} and {2}")]
        public double AirPresure { get; set; }
    }
}
