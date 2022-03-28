using PCBuildWeb.Models.Entities.Bases;
using PCBuildWeb.Models.Entities.Properties;
using PCBuildWeb.Models.Enums;

namespace PCBuildWeb.Models.Entities.Parts
{
    public class PSU : ComputerPart
    {
        public int Wattage { get; set; }
        public int Length { get; set; }
        public PSUType Type { get; set; }
        public PSUSize PSUSize { get; set; }
        public int PSUSizeId { get; set; }
    }
}
