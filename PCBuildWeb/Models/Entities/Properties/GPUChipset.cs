using PCBuildWeb.Models.Entities.Bases;

namespace PCBuildWeb.Models.Entities.Properties
{
    public class GPUChipset : PartProperty
    {
        public GPUChipsetSeries ChipsetSeries { get; set; }
        public int ChipsetSeriesId { get; set; }
    }
}
