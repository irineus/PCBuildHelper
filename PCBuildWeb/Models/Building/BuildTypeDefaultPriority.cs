using PCBuildWeb.Models.Enums;

namespace PCBuildWeb.Models.Building
{
    public class BuildTypeDefaultPriority
    {
        public BuildTypeEnum BuildType { get; set; }

        public List<Priority> PartPriorities { get; set; }

        public BuildTypeDefaultPriority()
        {
            PartPriorities = new List<Priority>();
        }

        public BuildTypeDefaultPriority(BuildTypeEnum type)
        {
            BuildType = type;
            PartPriorities = new List<Priority>();
            switch (type)
            {
                case BuildTypeEnum.Usual:
                    PartPriorities.Add(new Priority() { PartType = PartType.CPU, PartPriority = 1, PartBudgetPercent = 0.2 });
                    PartPriorities.Add(new Priority() { PartType = PartType.Motherboard, PartPriority = 2, PartBudgetPercent = 0.12 });
                    PartPriorities.Add(new Priority() { PartType = PartType.GPU, PartPriority = 3, PartBudgetPercent = 0.3 });
                    PartPriorities.Add(new Priority() { PartType = PartType.CPUCooler, PartPriority = 4, PartBudgetPercent = 0.05});
                    PartPriorities.Add(new Priority() { PartType = PartType.Memory, PartPriority = 5, PartBudgetPercent = 0.1 });
                    PartPriorities.Add(new Priority() { PartType = PartType.Storage, PartPriority = 6, PartBudgetPercent = 0.1 });
                    PartPriorities.Add(new Priority() { PartType = PartType.PSU, PartPriority = 7, PartBudgetPercent = 0.05 });
                    PartPriorities.Add(new Priority() { PartType = PartType.Case, PartPriority = 8, PartBudgetPercent = 0.05 });
                    PartPriorities.Add(new Priority() { PartType = PartType.CaseFan, PartPriority = 9, PartBudgetPercent = 0.03 });
                    break;
                case BuildTypeEnum.DualGPU:
                    PartPriorities.Add(new Priority() { PartType = PartType.CPU, PartPriority = 1, PartBudgetPercent = 0.18 });
                    PartPriorities.Add(new Priority() { PartType = PartType.Motherboard, PartPriority = 2, PartBudgetPercent = 0.1 });
                    PartPriorities.Add(new Priority() { PartType = PartType.GPU, PartPriority = 3, PartBudgetPercent = 0.19 });
                    PartPriorities.Add(new Priority() { PartType = PartType.GPU, PartPriority = 4, PartBudgetPercent = 0.19 });
                    PartPriorities.Add(new Priority() { PartType = PartType.CPUCooler, PartPriority = 5, PartBudgetPercent = 0.08 });
                    PartPriorities.Add(new Priority() { PartType = PartType.Memory, PartPriority = 6, PartBudgetPercent = 0.08 });
                    PartPriorities.Add(new Priority() { PartType = PartType.Storage, PartPriority = 7, PartBudgetPercent = 0.08 });
                    PartPriorities.Add(new Priority() { PartType = PartType.PSU, PartPriority = 8, PartBudgetPercent = 0.04 });
                    PartPriorities.Add(new Priority() { PartType = PartType.Case, PartPriority = 9, PartBudgetPercent = 0.04 });
                    PartPriorities.Add(new Priority() { PartType = PartType.CaseFan, PartPriority = 10, PartBudgetPercent = 0.02 });
                    break;
                case BuildTypeEnum.AIO:
                    PartPriorities.Add(new Priority() { PartType = PartType.CPU, PartPriority = 1, PartBudgetPercent = 0.2 });
                    PartPriorities.Add(new Priority() { PartType = PartType.Motherboard, PartPriority = 2, PartBudgetPercent = 0.10 });
                    PartPriorities.Add(new Priority() { PartType = PartType.GPU, PartPriority = 3, PartBudgetPercent = 0.28 });
                    PartPriorities.Add(new Priority() { PartType = PartType.CPUCooler, PartPriority = 4, PartBudgetPercent = 0.15 });
                    PartPriorities.Add(new Priority() { PartType = PartType.Memory, PartPriority = 5, PartBudgetPercent = 0.09 });
                    PartPriorities.Add(new Priority() { PartType = PartType.Storage, PartPriority = 6, PartBudgetPercent = 0.08 });
                    PartPriorities.Add(new Priority() { PartType = PartType.PSU, PartPriority = 7, PartBudgetPercent = 0.04 });
                    PartPriorities.Add(new Priority() { PartType = PartType.Case, PartPriority = 8, PartBudgetPercent = 0.04 });
                    PartPriorities.Add(new Priority() { PartType = PartType.CaseFan, PartPriority = 9, PartBudgetPercent = 0.02 });
                    break;
                case BuildTypeEnum.CustomWC:
                    PartPriorities.Add(new Priority() { PartType = PartType.CPU, PartPriority = 1, PartBudgetPercent = 0.18 });
                    PartPriorities.Add(new Priority() { PartType = PartType.Motherboard, PartPriority = 2, PartBudgetPercent = 0.10 });
                    PartPriorities.Add(new Priority() { PartType = PartType.GPU, PartPriority = 3, PartBudgetPercent = 0.25 });
                    PartPriorities.Add(new Priority() { PartType = PartType.WC_CPU_Block, PartPriority = 4, PartBudgetPercent = 0.08 });
                    PartPriorities.Add(new Priority() { PartType = PartType.WC_Radiator, PartPriority = 5, PartBudgetPercent = 0.08 });
                    PartPriorities.Add(new Priority() { PartType = PartType.WC_Reservoir, PartPriority = 6, PartBudgetPercent = 0.05 });
                    PartPriorities.Add(new Priority() { PartType = PartType.Memory, PartPriority = 7, PartBudgetPercent = 0.08 });
                    PartPriorities.Add(new Priority() { PartType = PartType.Storage, PartPriority = 8, PartBudgetPercent = 0.06 });
                    PartPriorities.Add(new Priority() { PartType = PartType.PSU, PartPriority = 9, PartBudgetPercent = 0.05 });
                    PartPriorities.Add(new Priority() { PartType = PartType.Case, PartPriority = 10, PartBudgetPercent = 0.05 });
                    PartPriorities.Add(new Priority() { PartType = PartType.CaseFan, PartPriority = 11, PartBudgetPercent = 0.02 });
                    break;
            }
        }

    }  
}
