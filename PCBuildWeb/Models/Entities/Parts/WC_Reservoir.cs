using PCBuildWeb.Models.Entities.Bases;
using System.ComponentModel.DataAnnotations;

namespace PCBuildWeb.Models.Entities.Parts
{
    public class WC_Reservoir : ComputerPart
    {
        [Display(Name = "Height")]
        [Required(ErrorMessage = "{0} is required")]
        [Range(0.4, 2.0, ErrorMessage = "{0} should be a value between {1} and {2}")]
        public double Height { get; set; }
    }
}
