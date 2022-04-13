using Microsoft.AspNetCore.Mvc.Rendering;
using PCBuildWeb.Data;
using PCBuildWeb.Models.Building;
using PCBuildWeb.Models.Entities.Parts;
using PCBuildWeb.Models.Entities.Properties;
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
        public readonly BuildTypeService _buildTypeService;
        public readonly BuildTypeStructureService _buildTypeStructureService;

        public BuildService(PCBuildWebContext context, CPUService cpuService, MotherboardService motherboardService,
            GPUService gpuService, CPUCoolerService cpuCoolerService, MemoryService memoryService, PSUService psuService,
            StorageService storageService, CaseFanService caseFanService, WC_CPU_BlockService wcCpuBlockService,
            WC_RadiatorService wcRadiatorService, WC_ReservoirService wcReservoirService, CaseService caseService,
            ManufacturerService manufacturerService, BuildTypeService buildTypeService, BuildTypeStructureService buildTypeStructureService)
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
            _buildTypeService = buildTypeService;
            _buildTypeStructureService = buildTypeStructureService;
        }

        public async Task<Build> BuildNewPC(Parameter parameter)
        {
            Build buildNewPC = new Build() { Parameter = parameter };

            // Set Build Component Slots and Priorities for the BuildType
            buildNewPC.Components = await SetBuildComponentsDefaultProperties(parameter.BuildType);

            // Get the preferred manufacturer object
            if ((buildNewPC.Parameter.ManufacturerId > 0) && (buildNewPC.Parameter.PreferredManufacturer is null))
            {
                buildNewPC.Parameter.PreferredManufacturer = await _manufacturerService.FindByIdAsync(buildNewPC.Parameter.ManufacturerId.Value);
            }
            
            foreach (Component component in buildNewPC.Components.OrderBy(c => c.Priority))
            {
                // Get the default budget for the component (doesn't check for other types)
                double budgetValue = await GetComponentBuildBudget(buildNewPC, component.PartType);
                // Find the best component for the build
                Component newComponent = await FindBestComponent(buildNewPC, component.PartType, budgetValue);
                // Check if a part was found
                if (newComponent.BuildPart is not null)
                {
                    // Update the component budget value
                    newComponent.BudgetValue = budgetValue;
                    // Update the component with the part that was found
                    int componentIndex = buildNewPC.Components.IndexOf(component);
                    buildNewPC.Components[componentIndex].BuildPart = newComponent.BuildPart;
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

            // Get the preferred manufacturer object
            if ((reBuild.Parameter.ManufacturerId > 0) && (reBuild.Parameter.PreferredManufacturer is null))
            {
                reBuild.Parameter.PreferredManufacturer = await _manufacturerService.FindByIdAsync(reBuild.Parameter.ManufacturerId.Value);
            }

            // Retrieve BuildPart info for each component
            await RetrieveBuildPartInfo(reBuild.Components);

            // Create a new Component List for the rebuild parts
            List<Component> rebuildComponents = new List<Component>();

            foreach (Component component in reBuild.Components.OrderBy(c => c.Priority))
            {
                if (component.BuildPart is not null)
                {
                    // Get the component original budget taking into account the other parts of the build
                    double originalPartBudget = await GetComponentBuildBudget(reBuild, component.PartType);
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
            //buildPC = SetComponentPriorities(buildPC);

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
        
        private async Task<List<Component>> SetBuildComponentsDefaultProperties(BuildType buildType)
        {
            if (buildType is null)
            {
                throw new ArgumentNullException(nameof(buildType));
            }

            List<BuildTypeStructure> buildTypeComponents = await _buildTypeStructureService.FindBuildTypeComponentsAsync(buildType);
            List<Component> components = new List<Component>();

            foreach (var part in buildTypeComponents)
            {
                components.Add(new Component()
                {
                    BuildPart = null,
                    BudgetValue = 0,
                    Commited = false,
                    Priority = part.Priority,
                    PartType = part.PartType,
                    BudgetPercent = part.BudgetPercent
                });
            }

            return components;
        }

        //Order build components by priority
        public List<Component> OrderBuildComponents(Build build)
        {
            return build.Components.OrderBy(c => c.Priority).ToList();
        }

        // Get the build component budget, witch is eventualy based in other parts of the build
        public async Task<double> GetComponentBuildBudget(Build build, PartType partType)
        {
            if (build is null)
            {
                throw new ArgumentNullException(nameof(build));
            }
            if (build.Parameter is null)
            {
                throw new ArgumentNullException(nameof(build.Parameter));
            }
            // Get default build budget
            int defaultBuildBudget = build.Parameter.Budget;
            // Get component part budget percent
            double componentPartBudgetPercent = await GetComponentPartDefaultBudgetPercent(build.BuildType, partType);
            // Get default budget value for parts
            double budgetValue = defaultBuildBudget * componentPartBudgetPercent;
            // Set custom budget value for Dual GPU Builds
            if ((partType == PartType.GPU) && (build.Parameter.MustHaveDualGPU))
            {
                budgetValue = budgetValue / 2;
            }
            // Set custom budget value for Memory
            if (partType == PartType.Memory)
            {
                budgetValue /= build.Parameter.MemoryChannels;
            }
            // Set custom budget value for case
            if (partType == PartType.CaseFan)
            {
                // Only get fan budget if there's a pre-selected case
                if (build.Components.Any(c => c.BuildPart!.PartType == PartType.Case))
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
            }            

            return budgetValue;
        }
        
        public async Task<double> GetComponentPartDefaultBudgetPercent(BuildType buildType, PartType partType)
        {
            if (buildType is null)
            {
                throw new ArgumentNullException(nameof(buildType));
            }

            BuildTypeStructure? componentType = await _buildTypeStructureService.FindBuildTypeStructureComponentAsync(buildType, partType);

            if (componentType is null)
            {
                throw new ArgumentNullException(nameof(componentType));
            }

            return componentType.BudgetPercent;
        }
    }
}
