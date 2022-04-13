using PCBuildWeb.Models.Entities.Bases;
using PCBuildWeb.Models.Entities.Parts;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PCBuildWeb.Models.Entities.Properties
{
    public class GPUChipset : PartProperty, ICloneable
    {
        [ForeignKey("ChipsetSeriesId")]
        public GPUChipsetSeries ChipsetSeries { get; set; } = new GPUChipsetSeries();
        [Display(Name = "Chipset Series")]
        public int ChipsetSeriesId { get; set; }
        [Display(Name = "GPUs with this Chipset")]
        public List<GPU> GPUs { get; set; } = new List<GPU>();

        public object Clone()
        {
            var gpuChipset = (GPUChipset)MemberwiseClone();
            if(ChipsetSeries is not null)
            {
                gpuChipset.ChipsetSeries = (GPUChipsetSeries)ChipsetSeries.Clone();
            }
            //if (GPUs is not null)
            //{
            //    foreach (GPU gpu in GPUs)
            //    {
            //        gpuChipset.GPUs.Add((GPU)gpu.Clone());
            //    }
            //}
            return gpuChipset;
        }
    }
}
