using PCBuildWeb.Models.Entities.Bases;
using PCBuildWeb.Models.Entities.Parts;

namespace PCBuildWeb.Models.Entities.Properties
{
    public class PowerConnector : PartProperty
    {
        public PowerConnector()
        {
            this.UsedInThisGPUs = new HashSet<GPU>();
        }
        public virtual ICollection<GPU> UsedInThisGPUs { get; set; }
    }
}
