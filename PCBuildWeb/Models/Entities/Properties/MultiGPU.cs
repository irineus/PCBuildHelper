using PCBuildWeb.Models.Entities.Bases;
using PCBuildWeb.Models.Entities.Parts;

namespace PCBuildWeb.Models.Entities.Properties
{
    public class MultiGPU : PartProperty, ICloneable
    {
        public MultiGPU()
        {
            //this.UsedInThisMobos = new HashSet<Motherboard>();
        }

        public List<Motherboard> Motherboards { get; set; } = new List<Motherboard>();
        public List<GPU> GPUs { get; set; } = new List<GPU>();

        public object Clone()
        {
            var multiGPUClone = (MultiGPU)MemberwiseClone();
            foreach (Motherboard mobo in Motherboards)
            {
                multiGPUClone.Motherboards.Add((Motherboard)mobo.Clone());
            }            
            foreach (GPU gpu in GPUs)
            {
                multiGPUClone.GPUs.Add((GPU)gpu.Clone());
            }
            return multiGPUClone;
        }
    }
}
