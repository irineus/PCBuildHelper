using PCBuildWeb.Models.Entities.Bases;
using PCBuildWeb.Models.Entities.Properties;
using PCBuildWeb.Models.Enums;

namespace PCBuildWeb.Models.Entities.Parts
{
    public class GPU : ComputerPart
    {
        public GPUChipsetBrand ChipsetBrand { get; set; }
        public GPUChipset GPUChipset { get; set; }
        public int GPUChipsetId { get; set; }
        public bool IsWaterCooled { get; set; }
        public int RankingScore { get; set; }
        public int VRAM { get; set; }
        public int MinCoreFrequency { get; set; }
        public int BaseCoreFrequency { get; set; }
        public int OverclockedCoreFrequency { get; set; }
        public int MaxCoreFrequency { get; set; }
        public int MinMemFrequency { get; set; }
        public int BaseMemFrequency { get; set; }
        public int OverclockedMemFrequency { get; set; }
        public int MaxMemFrequency { get; set; }
        public int Length { get; set; }
        public int Wattage { get; set; }
        public MultiGPU MultiGPU { get; set; }
        public double SlotSize { get; set; }
        public List<PowerConnector> PowerConnectors { get; set; }
        public double ScoreToValueRatio { get; set; }
        public int SingleGPUScore { get; set; }
        public int DualGPUScore { get; set; }
        public double DualGPUPerformanceIncrease { get; set; }
        public int OverclockedSingleGPUScore { get; set; }
        public int OverclockedDualGPUScore { get; set; }


    }
}
