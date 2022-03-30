using PCBuildWeb.Models.Enums;

namespace PCBuildWeb.Models.Building
{
    public class PartPriority
    {
        public PartType PartType { get; set; }
        public int PriorityOrder { get; set; }
        public int BudgetPercent { get; set; }        
    }
}
