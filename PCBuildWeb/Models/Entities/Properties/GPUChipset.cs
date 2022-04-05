using PCBuildWeb.Models.Entities.Bases;
using PCBuildWeb.Models.Entities.Parts;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PCBuildWeb.Models.Entities.Properties
{
    public class GPUChipset : PartProperty
    {
        [ForeignKey("ChipsetSeriesId")]
        public GPUChipsetSeries ChipsetSeries { get; set; } = new GPUChipsetSeries();
        [Display(Name = "Chipset Series")]
        public int ChipsetSeriesId { get; set; }
        [Display(Name = "GPUs with this Chipset")]
        public List<GPU> GPUs { get; set; } = new List<GPU>();
    }
}
