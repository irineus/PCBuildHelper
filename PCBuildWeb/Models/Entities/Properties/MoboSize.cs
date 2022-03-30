using PCBuildWeb.Models.Entities.Bases;
using PCBuildWeb.Models.Entities.Parts;

namespace PCBuildWeb.Models.Entities.Properties
{
    public class MoboSize : PartProperty
    {
        public MoboSize()
        {
            this.SupportedCases = new HashSet<Case>();
        }

        public virtual ICollection<Case> SupportedCases { get; set; }
    }
}
