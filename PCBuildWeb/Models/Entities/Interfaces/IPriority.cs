using PCBuildWeb.Models.Enums;

namespace PCBuildWeb.Models.Entities.Interfaces
{
    public interface IPriority
    {
        public PartType PartType { get; set; }
        public int Priority { get; set; }
        public double BudgetPercent { get; set; }
    }
}
