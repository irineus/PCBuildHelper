using PCBuildWeb.Models.Entities.Properties;

namespace PCBuildWeb.Models.Building
{
    public class Build
    {
        public Build()
        {
        }

        public int CurrentLevel { get; set; }
        public int CurrentLevelPercent { get; set; } = 1;
        public int Budget { get; set; }
        public Manufacturer? PreferredManufacturer { get; set; }
        public int? TargetScore { get; set; }
        public int? TargetMemorySize { get; set; }
        public bool MustHaveDualGPU { get; set; }
        public bool MustHaveAIOCooler { get; set; }
        public bool MustHaveCustomWC { get; set; }
        public ICollection<PartPriority> PartPriorities { get; set; }
    }
}
