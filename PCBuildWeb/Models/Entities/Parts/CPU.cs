using PCBuildWeb.Models.Entities.Bases;
using PCBuildWeb.Models.Entities.Properties;

namespace PCBuildWeb.Models.Entities.Parts
{
    public class CPU : ComputerPart
    {
        public string Series { get; set; }
        public int RankingScore { get; set; }
        public int Frequency { get; set; }
        public int Cores { get; set; }
        public CPUSocket Socket { get; set; }
        public int Wattage { get; set; }
        public bool Overclockable { get; set; }
        public int ThermalThrottling { get; set; }
        public double Voltage { get; set; }
        public int BasicCPUScore { get; set; }
        public double ScoreToValueRatio { get; set; }
        public int DefaultMemorySpeed { get; set; }
        public int OverclockedCPUScore { get; set; }
        public double MultiplierStep { get; set; }
        public double NumberOfDies { get; set; }
        public int MaxMemoryChannels { get; set; }
        public double OverclockedVoltage { get; set; }
        public double OverclockedFrequency { get; set; }
        public double CoreClockMultiplier { get; set; }
        public double MemChannelsMultiplier { get; set; }
        public double MemClockMultiplier { get; set; }
    }
}
