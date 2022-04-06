using PCBuildWeb.Models.Entities.Bases;
using PCBuildWeb.Models.Entities.Parts;

namespace PCBuildWeb.Models.Entities.Properties
{
    public class PSUSize : PartProperty, ICloneable
    {
        public PSUSize()
        {
        }
        
        public List<Case> Cases { get; set; } = new List<Case>();

        public object Clone()
        {
            var psuSizeClone = (PSUSize)MemberwiseClone();
            if (Cases is not null)
            {
                foreach (Case casePart in Cases)
                {
                    psuSizeClone.Cases.Add((Case)casePart.Clone());
                }
            }
            return psuSizeClone;
        }
    }
}
