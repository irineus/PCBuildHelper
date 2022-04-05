using PCBuildWeb.Models.Entities.Bases;
using PCBuildWeb.Models.Entities.Parts;

namespace PCBuildWeb.Models.Entities.Properties
{
    public class MoboSize : PartProperty
    {
        public MoboSize()
        {
        }

        public List<Case> Cases { get; set; } = new List<Case>();
    }
}
