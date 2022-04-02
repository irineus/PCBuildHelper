using PCBuildWeb.Models.Entities.Bases;
using PCBuildWeb.Models.Enums;

namespace PCBuildWeb.Models.Building
{
    public class BuildPartSpec
    {
        public PartType Type { get; set; }
        public int Priority { get; set; }
        public double BudgetPercent { get; set; }
    }
}
