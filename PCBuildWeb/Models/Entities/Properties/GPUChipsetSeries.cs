using PCBuildWeb.Models.Entities.Bases;

namespace PCBuildWeb.Models.Entities.Properties
{
    public class GPUChipsetSeries : PartProperty, ICloneable
    {
        public object Clone()
        {
            var gpuChipsetSeries = (GPUChipsetSeries)MemberwiseClone();
            return gpuChipsetSeries;
        }
    }
}
