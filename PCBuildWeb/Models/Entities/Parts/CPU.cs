using PCBuildWeb.Models.Entities.Bases;
using PCBuildWeb.Models.Entities.Properties;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PCBuildWeb.Models.Entities.Parts
{
    public class CPU : ComputerPart, ICloneable
    {
        [ForeignKey("SeriesId")]
        public CPUSeries Series { get; set; }
        [Display(Name = "CPU Series")]
        [Required(ErrorMessage = "{0} is required")]
        public int SeriesId { get; set; }
        [Display(Name = "Ranking Score")]
        [Required(ErrorMessage = "{0} is required")]
        [Range(1, 99999, ErrorMessage = "{0} should be a value between {1} and {2}")]
        public int RankingScore { get; set; }
        [Display(Name = "Frequency (MHz)")]
        [Required(ErrorMessage = "{0} is required")]
        [Range(2000, 10000, ErrorMessage = "{0} should be a value between {1} and {2}")]
        public int Frequency { get; set; }
        [Display(Name = "Core Count")]
        [Required(ErrorMessage = "{0} is required")]
        [Range(1, 99, ErrorMessage = "{0} should be a value between {1} and {2}")]
        public int Cores { get; set; }
        [ForeignKey("CPUSocketId")]
        public CPUSocket CPUSocket { get; set; }
        [Display(Name = "CPU Socket Type")]
        [Required(ErrorMessage = "{0} is required")]
        public int CPUSocketId { get; set; }
        [Display(Name = "CPU TDP (Watts)")]
        [Required(ErrorMessage = "{0} is required")]
        [Range(1, 1000, ErrorMessage = "{0} should be a value between {1} and {2}")]
        public int Wattage { get; set; }
        [Display(Name = "Overclockable?")]
        public bool Overclockable { get; set; }
        [Display(Name = "Thermal Throttling Threshold")]
        [Required(ErrorMessage = "{0} is required")]
        [Range(1, 150, ErrorMessage = "{0} should be a value between {1} and {2}")]
        public int ThermalThrottling { get; set; }
        [Display(Name = "Voltage")]
        [Required(ErrorMessage = "{0} is required")]
        [Range(0.01, 12.00, ErrorMessage = "{0} should be a value between {1} and {2}")]
        public double Voltage { get; set; }
        [Display(Name = "Basic CPU Score")]
        [Required(ErrorMessage = "{0} is required")]
        [Range(1, 50000, ErrorMessage = "{0} should be a value between {1} and {2}")]
        public int BasicCPUScore { get; set; }
        [Display(Name = "Score to Value Ratio")]
        [Required(ErrorMessage = "{0} is required")]
        [Range(0.01, 99.99, ErrorMessage = "{0} should be a value between {1} and {2}")]
        public double ScoreToValueRatio { get; set; }
        [Display(Name = "Default Memory Speed")]
        [Required(ErrorMessage = "{0} is required")]
        [Range(1000, 10000, ErrorMessage = "{0} should be a value between {1} and {2}")]
        public int DefaultMemorySpeed { get; set; }
        [Display(Name = "Overclocked CPU Score")]
        [Required(ErrorMessage = "{0} is required")]
        [Range(1, 50000, ErrorMessage = "{0} should be a value between {1} and {2}")]
        public int OverclockedCPUScore { get; set; }
        [Display(Name = "Multiplier Step")]
        [Required(ErrorMessage = "{0} is required")]
        [Range(0.25, 1.00, ErrorMessage = "{0} should be a value between {1} and {2}")]
        public double MultiplierStep { get; set; }
        [Display(Name = "Number of Dies")]
        [Required(ErrorMessage = "{0} is required")]
        [Range(1, 5, ErrorMessage = "{0} should be a value between {1} and {2}")]
        public double NumberOfDies { get; set; }
        [Display(Name = "Max Memory Channels")]
        [Required(ErrorMessage = "{0} is required")]
        [Range(2, 4, ErrorMessage = "{0} should be a value between {1} and {2}")]
        public int MaxMemoryChannels { get; set; }
        [Display(Name = "Overclock Base Voltage")]
        [Required(ErrorMessage = "{0} is required")]
        [Range(1.00, 2.00, ErrorMessage = "{0} should be a value between {1} and {2}")]
        public double OverclockedVoltage { get; set; }
        [Display(Name = "Overclock Base Frequency")]
        [Required(ErrorMessage = "{0} is required")]
        [Range(1000, 10000, ErrorMessage = "{0} should be a value between {1} and {2}")]
        public int OverclockedFrequency { get; set; }
        [Display(Name = "Core Clock Multiplier")]
        [Required(ErrorMessage = "{0} is required")]
        [Range(0.00001, 0.01, ErrorMessage = "{0} should be a value between {1} and {2}")]
        public double CoreClockMultiplier { get; set; }
        [Display(Name = "Mem Channels Multiplier")]
        [Required(ErrorMessage = "{0} is required")]
        [Range(0.1, 20.00, ErrorMessage = "{0} should be a value between {1} and {2}")]
        public double MemChannelsMultiplier { get; set; }
        [Display(Name = "Mem Clock Multiplier")]
        [Required(ErrorMessage = "{0} is required")]
        [Range(0.00001, 0.01, ErrorMessage = "{0} should be a value between {1} and {2}")]
        public double MemClockMultiplier { get; set; }

        public new object Clone()
        {
            var cpuClone = (CPU)MemberwiseClone();
            if (Series is not null)
            {
                cpuClone.Series = (CPUSeries)Series.Clone();
            }
            if (CPUSocket is not null)
            {
                cpuClone.CPUSocket = (CPUSocket)CPUSocket.Clone();
            }
            return cpuClone;
        }
    }
}
