using PCBuildWeb.Models.Entities.Bases;
using System.ComponentModel.DataAnnotations;

namespace PCBuildWeb.Models.Entities.Parts
{
    public class CaseFan : ComputerPart
    {
        public CaseFan()
        {
        }

        [Display(Name = "Air Flow")]
        [Required(ErrorMessage = "{0} is required")]
        [Range(1.00, 999.99, ErrorMessage = "{0} should be a value between {1} and {2}")]
        public double AirFlow { get; set; }
        [Display(Name = "Size")]
        [Required(ErrorMessage = "{0} is required")]
        [Range(90, 500, ErrorMessage = "{0} should be a value between {1} and {2}")]
        public int Size { get; set; }
        [Display(Name = "Air Pressure")]
        [Range(0.01, 99.99, ErrorMessage = "{0} should be a value between {1} and {2}")]
        public double? AirPressure { get; set; }
        public List<Case> Cases { get; set; } = new List<Case>();
    }
}
