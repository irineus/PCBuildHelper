using PCBuildWeb.Models.Entities.Bases;
using PCBuildWeb.Models.Entities.Interfaces;
using PCBuildWeb.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace PCBuildWeb.Models.Entities.Properties
{
    public class BuildType : PartProperty, ICloneable, IPriority
    {
        //[Display(Name = "Usual")]
        //Usual = 1,
        //[Display(Name = "Dual GPU")]
        //DualGPU = 2,
        //[Display(Name = "Usual with AIO")]
        //AIO = 3,
        //[Display(Name = "Custom Watercooled")]
        //CustomWC = 4,

        public PartType PartType { get; set; }
        public int Priority { get; set; }
        public double BudgetPercent { get; set; }

        public object Clone()
        {
            var buildType = (BuildType)MemberwiseClone();
            return buildType;
        }        
    }
}
