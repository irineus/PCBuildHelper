using PCBuildWeb.Models.Enums;

namespace PCBuildWeb.Models.Building
{
    public class BuildType
    {
        public TypeEnum Type { get; set; }

        public ICollection<BuildPartSpec> BuildPartSpecs { get; set; }

        public BuildType()
        {
            BuildPartSpecs = new List<BuildPartSpec>();
        }

        public BuildType(TypeEnum type)
        {
            Type = type;
            BuildPartSpecs = new List<BuildPartSpec>();
            switch (type)
            {
                case TypeEnum.Usual:
                    BuildPartSpecs.Add(new BuildPartSpec() { Type = PartType.CPU, Priority = 1, BudgetPercent = 0.2 });
                    BuildPartSpecs.Add(new BuildPartSpec() { Type = PartType.Motherboard, Priority = 2, BudgetPercent = 0.12 });
                    BuildPartSpecs.Add(new BuildPartSpec() { Type = PartType.GPU, Priority = 3, BudgetPercent = 0.3 });
                    BuildPartSpecs.Add(new BuildPartSpec() { Type = PartType.CPUCooler, Priority = 4, BudgetPercent = 0.05});
                    BuildPartSpecs.Add(new BuildPartSpec() { Type = PartType.Memory, Priority = 5, BudgetPercent = 0.1 });
                    BuildPartSpecs.Add(new BuildPartSpec() { Type = PartType.Storage, Priority = 6, BudgetPercent = 0.1 });
                    BuildPartSpecs.Add(new BuildPartSpec() { Type = PartType.PSU, Priority = 7, BudgetPercent = 0.05 });
                    BuildPartSpecs.Add(new BuildPartSpec() { Type = PartType.Case, Priority = 8, BudgetPercent = 0.05 });
                    BuildPartSpecs.Add(new BuildPartSpec() { Type = PartType.CaseFan, Priority = 9, BudgetPercent = 0.03 });
                    break;
                case TypeEnum.DualGPU:
                    BuildPartSpecs.Add(new BuildPartSpec() { Type = PartType.CPU, Priority = 1, BudgetPercent = 0.18 });
                    BuildPartSpecs.Add(new BuildPartSpec() { Type = PartType.Motherboard, Priority = 2, BudgetPercent = 0.1 });
                    BuildPartSpecs.Add(new BuildPartSpec() { Type = PartType.GPU, Priority = 3, BudgetPercent = 0.19 });
                    BuildPartSpecs.Add(new BuildPartSpec() { Type = PartType.GPU, Priority = 4, BudgetPercent = 0.19 });
                    BuildPartSpecs.Add(new BuildPartSpec() { Type = PartType.CPUCooler, Priority = 5, BudgetPercent = 0.08 });
                    BuildPartSpecs.Add(new BuildPartSpec() { Type = PartType.Memory, Priority = 6, BudgetPercent = 0.08 });
                    BuildPartSpecs.Add(new BuildPartSpec() { Type = PartType.Storage, Priority = 7, BudgetPercent = 0.08 });
                    BuildPartSpecs.Add(new BuildPartSpec() { Type = PartType.PSU, Priority = 8, BudgetPercent = 0.04 });
                    BuildPartSpecs.Add(new BuildPartSpec() { Type = PartType.Case, Priority = 9, BudgetPercent = 0.04 });
                    BuildPartSpecs.Add(new BuildPartSpec() { Type = PartType.CaseFan, Priority = 10, BudgetPercent = 0.02 });
                    break;
                case TypeEnum.AIO:
                    BuildPartSpecs.Add(new BuildPartSpec() { Type = PartType.CPU, Priority = 1, BudgetPercent = 0.2 });
                    BuildPartSpecs.Add(new BuildPartSpec() { Type = PartType.Motherboard, Priority = 2, BudgetPercent = 0.10 });
                    BuildPartSpecs.Add(new BuildPartSpec() { Type = PartType.GPU, Priority = 3, BudgetPercent = 0.28 });
                    BuildPartSpecs.Add(new BuildPartSpec() { Type = PartType.CPUCooler, Priority = 4, BudgetPercent = 0.15 });
                    BuildPartSpecs.Add(new BuildPartSpec() { Type = PartType.Memory, Priority = 5, BudgetPercent = 0.09 });
                    BuildPartSpecs.Add(new BuildPartSpec() { Type = PartType.Storage, Priority = 6, BudgetPercent = 0.08 });
                    BuildPartSpecs.Add(new BuildPartSpec() { Type = PartType.PSU, Priority = 7, BudgetPercent = 0.04 });
                    BuildPartSpecs.Add(new BuildPartSpec() { Type = PartType.Case, Priority = 8, BudgetPercent = 0.04 });
                    BuildPartSpecs.Add(new BuildPartSpec() { Type = PartType.CaseFan, Priority = 9, BudgetPercent = 0.02 });
                    break;
                case TypeEnum.CustomWC:
                    BuildPartSpecs.Add(new BuildPartSpec() { Type = PartType.CPU, Priority = 1, BudgetPercent = 0.18 });
                    BuildPartSpecs.Add(new BuildPartSpec() { Type = PartType.Motherboard, Priority = 2, BudgetPercent = 0.10 });
                    BuildPartSpecs.Add(new BuildPartSpec() { Type = PartType.GPU, Priority = 3, BudgetPercent = 0.25 });
                    BuildPartSpecs.Add(new BuildPartSpec() { Type = PartType.WC_CPU_Block, Priority = 4, BudgetPercent = 0.08 });
                    BuildPartSpecs.Add(new BuildPartSpec() { Type = PartType.WC_Radiator, Priority = 5, BudgetPercent = 0.08 });
                    BuildPartSpecs.Add(new BuildPartSpec() { Type = PartType.WC_Reservoir, Priority = 6, BudgetPercent = 0.05 });
                    BuildPartSpecs.Add(new BuildPartSpec() { Type = PartType.Memory, Priority = 7, BudgetPercent = 0.08 });
                    BuildPartSpecs.Add(new BuildPartSpec() { Type = PartType.Storage, Priority = 8, BudgetPercent = 0.06 });
                    BuildPartSpecs.Add(new BuildPartSpec() { Type = PartType.PSU, Priority = 9, BudgetPercent = 0.05 });
                    BuildPartSpecs.Add(new BuildPartSpec() { Type = PartType.Case, Priority = 10, BudgetPercent = 0.05 });
                    BuildPartSpecs.Add(new BuildPartSpec() { Type = PartType.CaseFan, Priority = 11, BudgetPercent = 0.02 });
                    break;
            }
        }

    }

    public enum TypeEnum
    {
        Usual,
        DualGPU,
        AIO,
        CustomWC
    }    
}
