using PCBuildWeb.Models.Entities.Bases;
using PCBuildWeb.Models.Entities.Parts;

namespace PCBuildWeb.Models.Entities.Properties
{
    public class MultiGPU : PartProperty
    {
        public MultiGPU()
        {
            //this.UsedInThisMobos = new HashSet<Motherboard>();
        }

        public List<Motherboard> Motherboards { get; } = new List<Motherboard>();
        public List<GPU> GPUs { get; } = new List<GPU>();
    }
}
