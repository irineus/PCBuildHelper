using PCBuildWeb.Models.Entities.Properties;
using PCBuildWeb.Models.Enums;

namespace PCBuildWeb.Models.Building
{
    //public class BuildTypePriority
    //{
    //    public int Id { get; set; }
    //    public BuildType BuildType { get; set; } = new BuildType();

    //    public List<ComponentPriority> PartPriorities { get; set; } = new List<ComponentPriority>();

    //    public BuildTypePriority()
    //    {
    //    }

        //public BuildTypePriority(BuildType type)
        //{
        //    BuildType = type;
        //    PartPriorities = new List<ComponentPriority>();
        //    switch (type)
        //    {
        //        case BuildType.Usual: // No custom WC and one GPU
        //            PartPriorities.Add(new ComponentPriority() { PartType = PartType.CPU, Priority = 1, BudgetPercent = 0.2 });
        //            PartPriorities.Add(new ComponentPriority() { PartType = PartType.Motherboard, Priority = 2, BudgetPercent = 0.12 });
        //            PartPriorities.Add(new ComponentPriority() { PartType = PartType.GPU, Priority = 3, BudgetPercent = 0.3 });
        //            PartPriorities.Add(new ComponentPriority() { PartType = PartType.CPUCooler, Priority = 4, BudgetPercent = 0.05});
        //            PartPriorities.Add(new ComponentPriority() { PartType = PartType.Memory, Priority = 5, BudgetPercent = 0.1 });
        //            PartPriorities.Add(new ComponentPriority() { PartType = PartType.Storage, Priority = 6, BudgetPercent = 0.1 });
        //            PartPriorities.Add(new ComponentPriority() { PartType = PartType.PSU, Priority = 7, BudgetPercent = 0.05 });
        //            PartPriorities.Add(new ComponentPriority() { PartType = PartType.Case, Priority = 8, BudgetPercent = 0.05 });
        //            PartPriorities.Add(new ComponentPriority() { PartType = PartType.CaseFan, Priority = 9, BudgetPercent = 0.03 });
        //            break;
        //        case BuildType.DualGPU: // Same as Usual build, but with modified GPU budget
        //            PartPriorities.Add(new ComponentPriority() { PartType = PartType.CPU, Priority = 1, BudgetPercent = 0.18 });
        //            PartPriorities.Add(new ComponentPriority() { PartType = PartType.Motherboard, Priority = 2, BudgetPercent = 0.1 });
        //            PartPriorities.Add(new ComponentPriority() { PartType = PartType.GPU, Priority = 3, BudgetPercent = 0.38 });
        //            PartPriorities.Add(new ComponentPriority() { PartType = PartType.CPUCooler, Priority = 4, BudgetPercent = 0.08 });
        //            PartPriorities.Add(new ComponentPriority() { PartType = PartType.Memory, Priority = 5, BudgetPercent = 0.08 });
        //            PartPriorities.Add(new ComponentPriority() { PartType = PartType.Storage, Priority = 6, BudgetPercent = 0.08 });
        //            PartPriorities.Add(new ComponentPriority() { PartType = PartType.PSU, Priority = 7, BudgetPercent = 0.04 });
        //            PartPriorities.Add(new ComponentPriority() { PartType = PartType.Case, Priority = 8, BudgetPercent = 0.04 });
        //            PartPriorities.Add(new ComponentPriority() { PartType = PartType.CaseFan, Priority = 9, BudgetPercent = 0.02 });
        //            break;
        //        case BuildType.AIO: // Force an AIO for CPUCooler. Increase CPUCooler budget
        //            PartPriorities.Add(new ComponentPriority() { PartType = PartType.CPU, Priority = 1, BudgetPercent = 0.2 });
        //            PartPriorities.Add(new ComponentPriority() { PartType = PartType.Motherboard, Priority = 2, BudgetPercent = 0.10 });
        //            PartPriorities.Add(new ComponentPriority() { PartType = PartType.GPU, Priority = 3, BudgetPercent = 0.28 });
        //            PartPriorities.Add(new ComponentPriority() { PartType = PartType.CPUCooler, Priority = 4, BudgetPercent = 0.15 });
        //            PartPriorities.Add(new ComponentPriority() { PartType = PartType.Memory, Priority = 5, BudgetPercent = 0.09 });
        //            PartPriorities.Add(new ComponentPriority() { PartType = PartType.Storage, Priority = 6, BudgetPercent = 0.08 });
        //            PartPriorities.Add(new ComponentPriority() { PartType = PartType.PSU, Priority = 7, BudgetPercent = 0.04 });
        //            PartPriorities.Add(new ComponentPriority() { PartType = PartType.Case, Priority = 8, BudgetPercent = 0.04 });
        //            PartPriorities.Add(new ComponentPriority() { PartType = PartType.CaseFan, Priority = 9, BudgetPercent = 0.02 });
        //            break;
        //        case BuildType.CustomWC: // Include Custom WC parts
        //            PartPriorities.Add(new ComponentPriority() { PartType = PartType.CPU, Priority = 1, BudgetPercent = 0.18 });
        //            PartPriorities.Add(new ComponentPriority() { PartType = PartType.Motherboard, Priority = 2, BudgetPercent = 0.10 });
        //            PartPriorities.Add(new ComponentPriority() { PartType = PartType.GPU, Priority = 3, BudgetPercent = 0.25 });
        //            PartPriorities.Add(new ComponentPriority() { PartType = PartType.WC_CPU_Block, Priority = 4, BudgetPercent = 0.08 });
        //            PartPriorities.Add(new ComponentPriority() { PartType = PartType.WC_Radiator, Priority = 5, BudgetPercent = 0.08 });
        //            PartPriorities.Add(new ComponentPriority() { PartType = PartType.WC_Reservoir, Priority = 6, BudgetPercent = 0.05 });
        //            PartPriorities.Add(new ComponentPriority() { PartType = PartType.Memory, Priority = 7, BudgetPercent = 0.08 });
        //            PartPriorities.Add(new ComponentPriority() { PartType = PartType.Storage, Priority = 8, BudgetPercent = 0.06 });
        //            PartPriorities.Add(new ComponentPriority() { PartType = PartType.PSU, Priority = 9, BudgetPercent = 0.05 });
        //            PartPriorities.Add(new ComponentPriority() { PartType = PartType.Case, Priority = 10, BudgetPercent = 0.05 });
        //            PartPriorities.Add(new ComponentPriority() { PartType = PartType.CaseFan, Priority = 11, BudgetPercent = 0.02 });
        //            break;
        //    }
        //}

    //}  
}
