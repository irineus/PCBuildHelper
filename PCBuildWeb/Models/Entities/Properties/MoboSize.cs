using PCBuildWeb.Models.Entities.Bases;
using PCBuildWeb.Models.Entities.Parts;

namespace PCBuildWeb.Models.Entities.Properties
{
    public class MoboSize : PartProperty, ICloneable
    {
        public MoboSize()
        {
        }

        public List<Case> Cases { get; set; } = new List<Case>();

        public object Clone()
        {
            var moboSizeClone = (MoboSize)MemberwiseClone();
            if (Cases is not null)
            {
                foreach (Case casePart in Cases)
                {
                    moboSizeClone.Cases.Add((Case)casePart.Clone());
                }
            }
            return moboSizeClone;
        }        
    }
}
