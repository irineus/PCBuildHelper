using System.ComponentModel.DataAnnotations;

namespace PCBuildWeb.Models.Enums
{
    public enum BuildTypeEnum : int
    {
        [Display(Name = "Usual")]
        Usual = 1,
        [Display(Name = "Dual GPU")]
        DualGPU = 2,
        [Display(Name = "Usual with AIO")]
        AIO = 3,
        [Display(Name = "Custom Watercooled")]
        CustomWC = 4,
    }
}
