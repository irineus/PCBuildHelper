using Microsoft.AspNetCore.Mvc.Rendering;
using PCBuildWeb.Data;
using PCBuildWeb.Models.Building;
using PCBuildWeb.Models.Entities.Parts;
using PCBuildWeb.Models.Enums;
using PCBuildWeb.Services.Entities.Parts;
using PCBuildWeb.Services.Entities.Properties;

namespace PCBuildWeb.Services.Building
{
    public class BuildService
    {
        public readonly PCBuildWebContext _context;
        public readonly CaseFanService _caseFanService;
        public readonly CaseService _caseService;
        public readonly CPUCoolerService _cpuCoolerService;
        public readonly CPUService _cpuService;
        public readonly GPUService _gpuService;
        public readonly MemoryService _memoryService;
        public readonly MotherboardService _motherboardService;
        public readonly PSUService _psuService;
        public readonly StorageService _storageService;
        public readonly WC_CPU_BlockService _wcCpuBlockService;
        public readonly WC_RadiatorService _wcRadiatorService;
        public readonly WC_ReservoirService _wcReservoirService;
        public readonly ManufacturerService _manufacturerService;

        public BuildService(PCBuildWebContext context, CPUService cpuService, MotherboardService motherboardService,
            GPUService gpuService, CPUCoolerService cpuCoolerService, MemoryService memoryService, PSUService psuService,
            StorageService storageService, CaseFanService caseFanService, WC_CPU_BlockService wcCpuBlockService,
            WC_RadiatorService wcRadiatorService, WC_ReservoirService wcReservoirService, CaseService caseService,
            ManufacturerService manufacturerService)
        {
            _context = context;
            _cpuService = cpuService;
            _motherboardService = motherboardService;
            _gpuService = gpuService;
            _cpuCoolerService = cpuCoolerService;
            _memoryService = memoryService;
            _psuService = psuService;
            _storageService = storageService;
            _caseFanService = caseFanService;
            _wcCpuBlockService = wcCpuBlockService;
            _wcRadiatorService = wcRadiatorService;
            _wcReservoirService = wcReservoirService;
            _caseService = caseService;
            _manufacturerService = manufacturerService;
        }

        public async Task<Build> BuildNewPC(Parameter parameter)
        {
            Build buildNewPC = new Build() { Parameter = parameter };

            //Set default part priorities for the build type
            buildNewPC.Parameter.PartPriorities = new BuildTypeDefaultPriority(buildNewPC.Parameter.BuildType).PartPriorities;

            if (buildNewPC.Parameter.ManufacturerId > 0)
            {
                buildNewPC.Parameter.PreferredManufacturer = await _manufacturerService.FindByIdAsync(buildNewPC.Parameter.ManufacturerId.Value);
            }

            buildNewPC.Components = new List<Component>();

            foreach (Priority priorityComponent in buildNewPC.Parameter.PartPriorities.OrderBy(b => b.PartPriority))
            {
                double budgetValue = await GetComponentBudget(buildNewPC, priorityComponent);

                Component newComponent = await FindBestComponent(buildNewPC, priorityComponent.PartType, budgetValue);
                // Check if a part was found
                if (newComponent.BuildPart is not null)
                {
                    newComponent.BudgetValue = budgetValue;
                    // Add new component in the build
                    buildNewPC.Components.Add(newComponent);
                }
            }

            // Check if there's any selected build part in the component list
            List<Component>? componentsWithBuildParts = buildNewPC.Components.Where(c => c.BuildPart is not null).ToList();
            if (componentsWithBuildParts.Any())
            {
                // Add more GPUs for dual GPU build
                Component? gpuComponent = buildNewPC.Components.Where(c => c.BuildPart!.PartType == PartType.GPU).FirstOrDefault();
                if (gpuComponent != null)
                {
                    if (buildNewPC.Parameter.MustHaveDualGPU)
                    {
                        buildNewPC.Components.Add((Component)gpuComponent.Clone());
                    }
                }

                // Add more memories (considering memory channel count)
                Component? memoryComponent = buildNewPC.Components.Where(c => c.BuildPart!.PartType == PartType.Memory).FirstOrDefault();
                if (memoryComponent != null)
                {
                    for (int i = 1; i < buildNewPC.Parameter.MemoryChannels; i++)
                    {
                        buildNewPC.Components.Add((Component)memoryComponent.Clone());
                    }
                }

                // Add more fans (considering free slots)
                Component? fanComponent = buildNewPC.Components.Where(c => c.BuildPart!.PartType == PartType.CaseFan).FirstOrDefault();
                if (fanComponent != null)
                {
                    var freeSlots = await _caseFanService.CheckFanFreeSlots(buildNewPC, true);

                    for (int i = 1; i <= (freeSlots.Fan120 + freeSlots.Fan140); i++)
                    {
                        buildNewPC.Components.Add((Component)(fanComponent.Clone()));
                    }
                }
            }

            return await BuildPC(buildNewPC);
        }

        public async Task<Build> ReBuildPC(Build reBuild)
        {
            if (reBuild is null)
            {
                throw new ArgumentNullException(nameof(reBuild));
            }
            if (reBuild.Parameter is null)
            {
                throw new ArgumentNullException(nameof(reBuild.Parameter));
            }
            if (reBuild.Components is null)
            {
                throw new ArgumentNullException(nameof(reBuild.Components));
            }

            //Set default part priorities for the build type
            reBuild.Parameter.PartPriorities = new BuildTypeDefaultPriority(reBuild.Parameter.BuildType).PartPriorities;

            if (reBuild.Parameter.ManufacturerId > 0)
            {
                reBuild.Parameter.PreferredManufacturer = await _manufacturerService.FindByIdAsync(reBuild.Parameter.ManufacturerId.Value);
            }

            // Retrieve BuildPart info for each component
            await RetrieveBuildPartInfo(reBuild.Components);

            // Create a new Component List for the rebuild parts
            List<Component> rebuildComponents = new List<Component>();

            int index = 0;
            foreach (Component component in reBuild.Components.OrderBy(c => c.Priority))
            {
                component.BuildPart = reBuild.Components[index].BuildPart;
                if (component.BuildPart is not null)
                {
                    double originalPartBudget = await GetComponentBudget(reBuild, reBuild.Parameter.PartPriorities.Where(p => p.PartType == component.BuildPart.PartType).FirstOrDefault());
                    // Check if should find a new best part
                    if ((!component.Commited) && (component.BudgetValue != originalPartBudget))
                    {
                        Component newComponent = await FindBestComponent(reBuild, component.BuildPart.PartType);
                        // Check if a part was found
                        if (newComponent.BuildPart is not null)
                        {
                            newComponent.BudgetValue = component.BudgetValue;
                            newComponent.Priority = component.Priority;
                            // Add revised component
                            rebuildComponents.Add(newComponent);
                        }
                    }
                    else
                    {
                        // Add old component
                        rebuildComponents.Add((Component)component.Clone());
                    }
                }
                index++;
            }

            reBuild.Components = rebuildComponents;

            return await BuildPC(reBuild);
        }

        private async Task<bool> RetrieveBuildPartInfo(List<Component> components)
        {
            if (components is null)
            {
                return false;
            }
            
            foreach (Component component in components)
            {
                if (component.BuildPart is not null)
                {
                    switch (component.BuildPart.PartType)
                    {
                        case PartType.CPU:
                            component.BuildPart = await _cpuService.FindByIdAsync(component.BuildPart.Id);
                            break;
                        case PartType.Motherboard:
                            component.BuildPart = await _motherboardService.FindByIdAsync(component.BuildPart.Id);
                            break;
                        case PartType.GPU:
                            component.BuildPart = await _gpuService.FindByIdAsync(component.BuildPart.Id);
                            break;
                        case PartType.CPUCooler:
                            component.BuildPart = await _cpuCoolerService.FindByIdAsync(component.BuildPart.Id);
                            break;
                        case PartType.Memory:
                            component.BuildPart = await _memoryService.FindByIdAsync(component.BuildPart.Id);
                            break;
                        case PartType.Storage:
                            component.BuildPart = await _storageService.FindByIdAsync(component.BuildPart.Id);
                            break;
                        case PartType.PSU:
                            component.BuildPart = await _psuService.FindByIdAsync(component.BuildPart.Id);
                            break;
                        case PartType.Case:
                            component.BuildPart = await _caseService.FindByIdAsync(component.BuildPart.Id);
                            break;
                        case PartType.CaseFan:
                            component.BuildPart = await _caseFanService.FindByIdAsync(component.BuildPart.Id);
                            break;
                        case PartType.WC_CPU_Block:
                            component.BuildPart = await _wcCpuBlockService.FindByIdAsync(component.BuildPart.Id);
                            break;
                        case PartType.WC_Radiator:
                            component.BuildPart = await _wcRadiatorService.FindByIdAsync(component.BuildPart.Id);
                            break;
                        case PartType.WC_Reservoir:
                            component.BuildPart = await _wcReservoirService.FindByIdAsync(component.BuildPart.Id);
                            break;
                    }
                }
            }
            return true;
        }

        public async Task<Build> BuildPC(Build buildPC)
        {
            if (buildPC is null)
            {
                throw new ArgumentNullException(nameof(buildPC));
            }
            if (buildPC.Components is not null)
            {
                buildPC.TotalBasicScore = await CalculateBuildTotalScore(buildPC.Components, "basic");

                buildPC.TotalOCScore = await CalculateBuildTotalScore(buildPC.Components, "overclocked");

                buildPC.TotalRankingScore = await CalculateBuildTotalScore(buildPC.Components, "ranking");
            }

            // Set the build component priority
            buildPC = SetComponentPriorities(buildPC);

            buildPC.Components = OrderBuildComponents(buildPC);

            return buildPC;
        }

        private async Task<Component> FindBestComponent(Build buildPC, PartType partType)
        {
            return await FindBestComponent(buildPC, partType, buildPC.Components.Where(c => c.BuildPart!.PartType == partType).First().BudgetValue);
        }

        private async Task<Component> FindBestComponent(Build buildPC, PartType partType, double budgetValue)
        {
            Component newComponent = new Component();

            switch (partType)
            {
                case PartType.CPU:
                    newComponent.BuildPart = await _cpuService.FindBestCPU(buildPC, budgetValue);
                    break;
                case PartType.Motherboard:
                    newComponent.BuildPart = await _motherboardService.FindBestMotherboard(buildPC, budgetValue);
                    break;
                case PartType.GPU:
                    newComponent.BuildPart = await _gpuService.FindBestGPU(buildPC, budgetValue);
                    break;
                case PartType.CPUCooler:
                    newComponent.BuildPart = await _cpuCoolerService.FindBestCPUCooler(buildPC, budgetValue);
                    break;
                case PartType.Memory:
                    newComponent.BuildPart = await _memoryService.FindBestMemory(buildPC, budgetValue);
                    break;
                case PartType.Storage:
                    newComponent.BuildPart = await _storageService.FindBestStorage(buildPC, budgetValue);
                    break;
                case PartType.PSU:
                    newComponent.BuildPart = await _psuService.FindBestPSU(buildPC, budgetValue);
                    break;
                case PartType.Case:
                    newComponent.BuildPart = await _caseService.FindBestCase(buildPC, budgetValue);
                    break;
                case PartType.CaseFan:
                    newComponent.BuildPart = await _caseFanService.FindBestCaseFan(buildPC, budgetValue);
                    break;
                case PartType.WC_CPU_Block:
                    newComponent.BuildPart = await _wcCpuBlockService.FindBestWCCPUBlock(buildPC, budgetValue);
                    break;
                case PartType.WC_Radiator:
                    newComponent.BuildPart = await _wcRadiatorService.FindBestWCRadiator(buildPC, budgetValue);
                    break;
                case PartType.WC_Reservoir:
                    newComponent.BuildPart = await _wcReservoirService.FindBestWCReservoir(buildPC, budgetValue);
                    break;
            }

            return newComponent;
        }

        //Calculate build total scores
        private async Task<int> CalculateBuildTotalScore(List<Component> components, string scoreType)
        {
            int totalBasicScore = 0;
            int totalOCScore = 0;
            int totalRankingScore = 0;
            if (components.Any())
            {
                foreach (var component in components.Where(c => c.BuildPart!.PartType == PartType.CPU || c.BuildPart.PartType == PartType.GPU).ToList())
                {
                    switch (component.BuildPart!.PartType)
                    {
                        case PartType.CPU:
                            CPU? cpuComponent = await _cpuService.FindByIdAsync(component.BuildPart.Id);
                            if (cpuComponent is not null)
                            {
                                totalBasicScore += cpuComponent.BasicCPUScore;
                                totalOCScore += cpuComponent.OverclockedCPUScore;
                                totalRankingScore += cpuComponent.RankingScore;
                            }
                            break;
                        case PartType.GPU:
                            GPU? gpuComponent = await _gpuService.FindByIdAsync(component.BuildPart.Id);
                            if (gpuComponent is not null)
                            {
                                totalBasicScore += gpuComponent.SingleGPUScore;
                                totalOCScore += gpuComponent.OverclockedSingleGPUScore ?? 0;
                                totalRankingScore += gpuComponent.RankingScore;
                            }
                            break;
                    }
                }
            }
            return scoreType switch
            {
                "basic" => totalBasicScore,
                "overclocked" => totalOCScore,
                "ranking" => totalRankingScore,
                _ => totalBasicScore,
            };
        }

        private Build SetComponentPriorities(Build build)
        {
            if (build is null)
            {
                throw new ArgumentNullException(nameof(build));
            }
            if (build.Parameter is null)
            {
                throw new ArgumentNullException(nameof(build));
            }
            foreach (var part in build.Parameter.PartPriorities)
            {
                // Get the components of the type in the build
                var components = build.Components.Where(c => c.BuildPart!.PartType == part.PartType).ToList();
                foreach (var component in components)
                {
                    component.Priority = part.PartPriority;
                }
            }
            return build;
        }

        //Order build components by priority
        public List<Component> OrderBuildComponents(Build build)
        {
            return build.Components.OrderBy(c => c.Priority).ToList();
        }

        public async Task<double> GetComponentBudget(Build build, Priority componentPriority)
        {
            // Set default budget value for parts
            double budgetValue = build.Parameter.Budget.Value * componentPriority.PartBudgetPercent;
            // Set custom budget value for Dual GPU Builds
            if ((componentPriority.PartType == PartType.GPU) && (build.Parameter.MustHaveDualGPU))
            {
                budgetValue = budgetValue / 2;
            }
            // Set custom budget value for Memory
            if (componentPriority.PartType == PartType.Memory)
            {
                budgetValue /= build.Parameter.MemoryChannels.Value;
            }
            // Set custom budget value for case
            if (componentPriority.PartType == PartType.CaseFan)
            {
                // For budget purposes, check for slots without considering others fans perviosly selected for build
                var caseFreeSlots = await _caseFanService.CheckFanFreeSlots(build, false);
                // Distribute the budget for case fans for the possible ammount usable in the build
                var caseFanBudget = budgetValue;
                if ((caseFreeSlots.Fan120 + caseFreeSlots.Fan140) > 0)
                {
                    caseFanBudget = budgetValue / (caseFreeSlots.Fan120 + caseFreeSlots.Fan140);
                    budgetValue = caseFanBudget;
                }
            }
            

            return budgetValue;
        }
    }
}
