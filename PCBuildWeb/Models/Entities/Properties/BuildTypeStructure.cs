using PCBuildWeb.Models.Entities.Bases;
using PCBuildWeb.Models.Entities.Interfaces;
using PCBuildWeb.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PCBuildWeb.Models.Entities.Properties
{
    public class BuildTypeStructure : PartProperty, ICloneable, IPriority
    {
        [ForeignKey("BuildTypeId")]
        public BuildType BuildType { get; set; } = new BuildType();
        [Display(Name = "Build Type")]
        public int BuildTypeId { get; set; }
        public PartType PartType { get; set; }
        public int Priority { get; set; }
        public double BudgetPercent { get; set; }

        public object Clone()
        {
            var buildType = (BuildTypeStructure)MemberwiseClone();
            if (BuildType is not null)
            {
                buildType.BuildType = (BuildType)BuildType.Clone();
            }
            return buildType;
        }        
    }
}
