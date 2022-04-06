using PCBuildWeb.Models.Entities.Bases;

namespace PCBuildWeb.Models.Entities.Properties
{
    public class MoboChipset : PartProperty, ICloneable
    {
        public object Clone()
        {
            var moboChipsetClone = (MoboChipset)MemberwiseClone();
            return moboChipsetClone;
        }
    }
}
