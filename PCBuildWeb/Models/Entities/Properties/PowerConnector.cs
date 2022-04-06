using PCBuildWeb.Models.Entities.Bases;
using PCBuildWeb.Models.Entities.Parts;

namespace PCBuildWeb.Models.Entities.Properties
{
    public class PowerConnector : PartProperty, ICloneable
    {
        public PowerConnector()
        {
            //this.UsedInThisGPUs = new HashSet<GPU>();
        }
        public List<GPU> GPUs { get; set; } = new List<GPU>();

        public object Clone()
        {
            var powerconnectorClone = (PowerConnector)MemberwiseClone();
            if (GPUs is not null)
            {
                foreach (GPU gpu in GPUs)
                {
                    powerconnectorClone.GPUs.Add((GPU)gpu.Clone());
                }
            }
            return powerconnectorClone;
        }
    }
}
