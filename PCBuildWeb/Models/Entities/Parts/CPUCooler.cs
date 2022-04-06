using PCBuildWeb.Models.Entities.Bases;
using PCBuildWeb.Models.Entities.Properties;
using System.ComponentModel.DataAnnotations;

namespace PCBuildWeb.Models.Entities.Parts
{
    public class CPUCooler : ComputerPart, ICloneable
    {
        public CPUCooler()
        {
        }

        [Display(Name="Is a Watercooler (AIO)?")]
        public bool WaterCooler { get; set; }
        [Display(Name = "Is a Passive Cooler (no fans)?")]
        public bool Passive { get; set; }
        [Display(Name = "Air Flow")]
        [Required(ErrorMessage = "{0} is required")]
        [Range(1.00, 999.99, ErrorMessage = "{0} should be a value between {1} and {2}")]
        public double AirFlow { get; set; }
        public List<CPUSocket> CPUSockets { get; set; } = new List<CPUSocket>();
        [Display(Name = "Cooler Height")]
        [Required(ErrorMessage = "{0} is required")]
        [Range(1, 999, ErrorMessage = "{0} should be a value between {1} and {2}")]
        public int Height { get; set; }
        [Display(Name = "Radiator Size (for Watercooler)")]
        [Range(1, 999, ErrorMessage = "{0} should be a value between {1} and {2}")]
        public int? RadiatorSize { get; set; }
        [Display(Name = "Radiator Slots  (for Watercooler)")]
        [Range(0, 10, ErrorMessage = "{0} should be a value between {1} and {2}")]
        public int? RadiatorSlots { get; set; }
        [Display(Name = "Radiator Thickness (for Watercooler)")]
        [Range(0.01, 1.00, ErrorMessage = "{0} should be a value between {1} and {2}")]
        public double? RadiatorThickness { get; set; }
        [Display(Name = "Air Pressure")]
        [Range(0.01, 99.99, ErrorMessage = "{0} should be a value between {1} and {2}")]
        public double? AirPressure { get; set; }

        public new object Clone()
        {
            var cpuCoolerClone = (CPUCooler)MemberwiseClone();
            if (CPUSockets is not null)
            {
                foreach (CPUSocket cpuSocket in CPUSockets)
                {
                    cpuCoolerClone.CPUSockets.Add((CPUSocket)cpuSocket.Clone());
                }
            }
            return cpuCoolerClone;
        }
    }
}
