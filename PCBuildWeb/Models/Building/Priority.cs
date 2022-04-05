using PCBuildWeb.Models.Entities.Bases;
using PCBuildWeb.Models.Enums;

namespace PCBuildWeb.Models.Building
{
    public class Priority
    {
        public PartType PartType { get; set; }
        public int PartPriority { get; set; }
        public double PartBudgetPercent { get; set; }
    }
}
