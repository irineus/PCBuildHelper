using PCBuildWeb.Models.Entities.Bases;

namespace PCBuildWeb.Models.Building
{
    public class Component : ICloneable
    {
        public ComputerPart? BuildPart { get; set; }
        public double BudgetValue { get; set; }

        public object Clone()
        {
            var component = (Component)MemberwiseClone();
            if (BuildPart is not null)
            {
                component.BuildPart = (ComputerPart)BuildPart.Clone();
            }
            return component;
        }
    }
}
