using PCBuildWeb.Models.Entities.Bases;
using PCBuildWeb.Models.Entities.Properties;
using System.ComponentModel.DataAnnotations;

namespace PCBuildWeb.Models.Entities.Parts
{
    public class CPUCooler : ComputerPart
    {
        public CPUCooler()
        {
            this.CompatibleSockets = new HashSet<CPUSocket>();
        }

        [Display(Name="Is a Watercooler?")]
        public bool WaterCooler { get; set; }
        [Display(Name = "Is a Passive Cooler (no fans)?")]
        public bool Passive { get; set; }
        [Display(Name = "Air Flow")]
        [Required(ErrorMessage = "{0} is required")]
        [Range(1.00, 999.99, ErrorMessage = "{0} should be a value between {1} and {2}")]
        public double AirFlow { get; set; }
        public virtual ICollection<CPUSocket> CompatibleSockets { get; set; }
        [Display(Name = "Cooler Height")]
        [Required(ErrorMessage = "{0} is required")]
        [Range(1, 999, ErrorMessage = "{0} should be a value between {1} and {2}")]
        public int Height { get; set; }
        [Display(Name = "Radiator Size (for Watercooler)")]
        [Range(1, 999, ErrorMessage = "{0} should be a value between {1} and {2}")]
        public int RadiatorSize { get; set; }
        [Display(Name = "Radiator Slots  (for Watercooler)")]
        [Range(0, 10, ErrorMessage = "{0} should be a value between {1} and {2}")]
        public int RadiatorSlots { get; set; }
        [Display(Name = "Radiator Thickness (for Watercooler)")]
        [Range(0.01, 1.00, ErrorMessage = "{0} should be a value between {1} and {2}")]
        public double RadiatorThickness { get; set; }
        [Display(Name = "Air Pressure")]
        [Range(0.01, 99.99, ErrorMessage = "{0} should be a value between {1} and {2}")]
        public double AirPressure { get; set; }
    }
}
