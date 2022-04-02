using PCBuildWeb.Models.Entities.Bases;

namespace PCBuildWeb.Models.Building
{
    public class Component : BuildPartSpec
    {
        public Component()
        {
        }

        public ComputerPart? BuildPart { get; set; }
        public double BudgetValue { get; set; }
    }
}
