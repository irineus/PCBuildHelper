using PCBuildWeb.Models.Entities.Bases;
using PCBuildWeb.Models.Entities.Properties;
using PCBuildWeb.Models.Enums;

namespace PCBuildWeb.Models.Entities.Parts
{
    public class Case : ComputerPart
    {
        public CaseSize CaseSize { get; set; }
        public List<MoboSize> SupportedMoboSizes { get; set; }
        public List<PSUSize> SupportedPSUSizes { get; set; }
        public int MaxNumberRadiator120 { get; set; }
        public int MaxNumberRadiator140 { get; set; }
        public int MaxPsuLength { get; set; }
        public int MaxGPULength { get; set; }
        public bool UseForWcJobs { get; set; }
        public bool IsOpenBench { get; set; }
        public List<CaseFan> IncludedCaseFans { get; set; }
        public int RestrictedGpuLength { get; set; }
        public double InherentCooling { get; set; }
        public int PriceWithoutFans { get; set; }
    }
}
