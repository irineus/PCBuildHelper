using PCBuildWeb.Models.Entities.Bases;
using PCBuildWeb.Models.Enums;

namespace PCBuildWeb.Models.Entities.Parts
{
    public class Storage : ComputerPart
    {
        public StorageType Type { get; set; }
        public int Size { get; set; }
        public int Speed { get; set; }
        public bool IncludesHeatsink { get; set; }
        public double HeatsinkThickness { get; set; }

    }
}
