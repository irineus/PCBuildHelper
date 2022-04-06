using PCBuildWeb.Models.Entities.Bases;

namespace PCBuildWeb.Models.Entities.Properties
{
    public class CPUSeries : PartProperty, ICloneable
    {

        public object Clone()
        {
            var cpuSeries = (CPUSeries)MemberwiseClone();
            return cpuSeries;
        }
    }
}
