using PCBuildWeb.Models.Entities.Bases;

namespace PCBuildWeb.Models.Entities.Parts
{
    public class Memory : ComputerPart
    {
        public int Size { get; set; }
        public int Frequency { get; set; }
        public double Voltage { get; set; }
        public double PricePerGB { get; set; }
        public double OverclockedBaseVoltage { get; set; }
        public double OverclockedBaseFrequency { get; set; }
    }
}
