using PCBuildWeb.Models.Entities.Bases;
using PCBuildWeb.Models.Entities.Parts;

namespace PCBuildWeb.Models.Entities.Properties
{
    public class PSUSize : PartProperty
    {
        public PSUSize()
        {
            this.SupportedCases = new HashSet<Case>();
        }

        public virtual ICollection<Case> SupportedCases { get; set; }
    }
}
