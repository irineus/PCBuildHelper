using PCBuildWeb.Models.Entities.Bases;
using System.ComponentModel.DataAnnotations;

namespace PCBuildWeb.Models.Building
{
    public class Component : ICloneable
    {
        public ComputerPart? BuildPart { get; set; }
        [Display(Name = "Budget")]
        public double BudgetValue { get; set; }
        public bool Commited { get; set; }
        public int Priority { get; set; }

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
