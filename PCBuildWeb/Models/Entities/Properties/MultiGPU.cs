using PCBuildWeb.Models.Entities.Bases;
using PCBuildWeb.Models.Entities.Parts;

namespace PCBuildWeb.Models.Entities.Properties
{
    public class MultiGPU : PartProperty
    {
        public MultiGPU()
        {
            this.UsedInThisMobos = new HashSet<Motherboard>();
        }

        public virtual ICollection<Motherboard> UsedInThisMobos { get; set; }
    }
}
