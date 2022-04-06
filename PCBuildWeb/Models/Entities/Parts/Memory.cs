using PCBuildWeb.Models.Entities.Bases;
using System.ComponentModel.DataAnnotations;

namespace PCBuildWeb.Models.Entities.Parts
{
    public class Memory : ComputerPart, ICloneable
    {
        [Display(Name = "Size (GB)")]
        [Required(ErrorMessage = "{0} is required")]
        [Range(2, 64, ErrorMessage = "{0} should be a value between {1} and {2}")]
        public int Size { get; set; }
        [Display(Name = "Frequency (MHz)")]
        [Required(ErrorMessage = "{0} is required")]
        [Range(2000, 5000, ErrorMessage = "{0} should be a value between {1} and {2}")]
        public int Frequency { get; set; }
        [Display(Name = "Voltage")]
        [Required(ErrorMessage = "{0} is required")]
        [Range(1.00, 2.00, ErrorMessage = "{0} should be a value between {1} and {2}")]
        public double Voltage { get; set; }
        [Display(Name = "Price Per GB")]
        [Required(ErrorMessage = "{0} is required")]
        [Range(5.00, 100.00, ErrorMessage = "{0} should be a value between {1} and {2}")]
        public double PricePerGB { get; set; }
        [Display(Name = "Overclocked Base Voltage")]
        [Required(ErrorMessage = "{0} is required")]
        [Range(1.00, 2.00, ErrorMessage = "{0} should be a value between {1} and {2}")]
        public double OverclockedBaseVoltage { get; set; }
        [Display(Name = "Overclocked Base Frequency (MHz)")]
        [Required(ErrorMessage = "{0} is required")]
        [Range(2000, 5000, ErrorMessage = "{0} should be a value between {1} and {2}")]
        public double OverclockedBaseFrequency { get; set; }

        public new object Clone()
        {
            var memoryClone = (Memory)MemberwiseClone();
            return memoryClone;
        }
    }
}
