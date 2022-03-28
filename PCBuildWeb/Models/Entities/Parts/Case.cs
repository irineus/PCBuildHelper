using PCBuildWeb.Models.Entities.Bases;
using PCBuildWeb.Models.Entities.Properties;
using PCBuildWeb.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PCBuildWeb.Models.Entities.Parts
{
    public class Case : ComputerPart
    {
        public Case()
        {
            this.SupportedMoboSizes = new HashSet<MoboSize>();
            this.SupportedPSUSizes = new HashSet<PSUSize>();
            this.IncludedCaseFans = new HashSet<CaseFan>();
        }

        [Display(Name = "Case Size")]
        [Required(ErrorMessage = "{0} is required")]
        [EnumDataType(typeof(CaseSize))]
        public CaseSize CaseSize { get; set; }
        [Display(Name = "List of Supported Mobo Sizes")]
        [Required(ErrorMessage = "{0} is required")]
        public virtual ICollection<MoboSize> SupportedMoboSizes { get; set; }
        [Display(Name = "List of Supported PSU Sizes")]
        [Required(ErrorMessage = "{0} is required")]
        public virtual ICollection<PSUSize> SupportedPSUSizes { get; set; }
        [Display(Name = "Number of 120mm Slots")]
        [Required(ErrorMessage = "{0} is required")]
        [Range(0, 20, ErrorMessage = "{0} should be a value between {1} and {2}")]
        public int Number120mmSlots { get; set; }
        [Display(Name = "Number of 140mm Slots")]
        [Required(ErrorMessage = "{0} is required")]
        [Range(0, 20, ErrorMessage = "{0} should be a value between {1} and {2}")]
        public int Number140mmSlots { get; set; }
        [Display(Name = "PSU Max Lenght")]
        [Required(ErrorMessage = "{0} is required")]
        [Range(10, 500, ErrorMessage = "{0} should be a value between {1} and {2}")]
        public int MaxPsuLength { get; set; }
        [Display(Name = "GPU Card Max Lenght")]
        [Required(ErrorMessage = "{0} is required")]
        [Range(10, 999, ErrorMessage = "{0} should be a value between {1} and {2}")]
        public int MaxGPULength { get; set; }
        [Display(Name = "CPU Air Cooler Max Lenght")]
        [Required(ErrorMessage = "{0} is required")]
        [Range(10, 500, ErrorMessage = "{0} should be a value between {1} and {2}")]
        public int MaxCPUFanHeight { get; set; }
        [Display(Name = "Used for Watercooler Jobs?")]
        [Required(ErrorMessage = "{0} is required")]
        public bool UseForWcJobs { get; set; }
        [Display(Name = "Is an Open Bench Case?")]
        [Required(ErrorMessage = "{0} is required")]
        public bool IsOpenBench { get; set; }
        [Display(Name = "List of Included Fans")]
        public virtual ICollection<CaseFan>? IncludedCaseFans { get; set; }
        [Display(Name = "Restricted GPU Length")]
        public int? RestrictedGpuLength { get; set; }
        [Display(Name = "Restricted GPU Length")]
        [Required(ErrorMessage = "{0} is required")]
        [Range(0.00, 10.00, ErrorMessage = "{0} should be a value between {1} and {2}")]
        public double InherentCooling { get; set; }
    }
}
