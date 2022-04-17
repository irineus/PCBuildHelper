using PCBuildWeb.Models.Entities.Bases;

namespace PCBuildWeb.Models.Entities.Properties
{
    public class BuildType : PartProperty, ICloneable
    {

        public object Clone()
        {
            var buildType = (BuildType)MemberwiseClone();
            return buildType;
        }
    }
}
