using PCBuildWeb.Models.Entities.Properties;
using PCBuildWeb.Models.Enums;

namespace PCBuildWeb.Models.Entities.Bases
{
    public abstract class ComputerPart
    {
        public int Id { get; set; }
        public PartType PartType { get; set; }
        public int PartTypeId { get; set; }
        public Manufacturer Manufacturer { get; set; }
        public int ManufacturerId { get; set; }
        public double Price { get; set; }
        public double SellPrice { get; set; }
        public int LevelUnlock { get; set; }
        public int LevelPercent { get; set; }
        public Color Lighting { get; set; }
        public int LightingId { get; set; }
    }
}
