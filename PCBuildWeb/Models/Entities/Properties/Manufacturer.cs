using PCBuildWeb.Models.Entities.Bases;

namespace PCBuildWeb.Models.Entities.Properties
{
    public class Manufacturer : PartProperty, ICloneable
    {
        public object Clone()
        {
            var manufacturerClone = (Manufacturer)MemberwiseClone();
            return manufacturerClone;
        }
    }
}
