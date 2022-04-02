using PCBuildWeb.Models.Entities.Bases;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PCBuildWeb.Models.Entities.Properties
{
    public class GPUChipset : PartProperty
    {
        [ForeignKey("ChipsetSeriesId")]
        public GPUChipsetSeries ChipsetSeries { get; set; }
        [Display(Name = "Chipset Series")]
        public int ChipsetSeriesId { get; set; }
    }
}
