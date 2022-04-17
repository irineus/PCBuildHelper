using Microsoft.AspNetCore.Mvc.Rendering;
using PCBuildWeb.Data;
using PCBuildWeb.Models.Building;
using PCBuildWeb.Models.Entities.Bases;
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

        public async Task<Build> BuildPC(Build buildPC)
        {
            if (buildPC is null)
            {
                throw new ArgumentNullException(nameof(buildPC));
            }
            if (buildPC.Parameter is null)
            {
                throw new ArgumentNullException(nameof(buildPC.Parameter));
            }

            // Get the preferred manufacturer object
            if ((buildPC.Parameter.ManufacturerId > 0) && (buildPC.Parameter.PreferredManufacturer is null))
            {
                buildPC.Parameter.PreferredManufacturer = await _manufacturerService.FindByIdAsync(buildPC.Parameter.ManufacturerId.Value);
            }

            // Get the Build Type object
            if ((buildPC.Parameter.BuildTypeId > 0) && ((buildPC.Parameter.BuildType is null) || (buildPC.BuildType is null)))
            {
                BuildType? buildType = await _buildTypeService.FindByIdAsync(buildPC.Parameter.BuildTypeId);
                if (buildType is null)
                {
                    throw new ArgumentNullException(nameof(buildType));
                }
                buildPC.BuildType = buildType;
                buildPC.Parameter.BuildType = buildType;
            }

            if (buildPC.Components is null)
            {
                // Build New PC
                buildPC = await BuildNewPC(buildPC.Parameter);
            }
            else
            {
                // Update the Build
                buildPC = await ReBuildPC(buildPC);
            }

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

            buildPC.Components = OrderBuildComponents(buildPC);

            return buildPC;
        }

        public async Task<Build> BuildNewPC(Parameter parameter)
        {
            if (parameter.BuildType is null)
            {
                throw new ArgumentNullException(nameof(parameter.BuildType));
            }
            Build buildNewPC = new Build() 
            { 
                Parameter = parameter,
                Components = await SetBuildComponentsDefaultProperties(parameter.BuildType),
                BuildType = parameter.BuildType
            };
            
            foreach (Component component in buildNewPC.Components.OrderBy(c => c.Priority))
            {
                // Get the default budget for the component (doesn't check for other types)
                double budgetValue = await GetComponentBuildBudget(buildNewPC, component.PartType);
                // Find the best component for the build
                ComputerPart? BuildPart = await FindBestBuildPart(buildNewPC, component.PartType, budgetValue);
                // Check if a part was found
                if (BuildPart is not null)
                {
                    int componentIndex = buildNewPC.Components.IndexOf(component);
                    // Update the component budget value
                    buildNewPC.Components[componentIndex].BudgetValue = budgetValue;
                    // Update the component with the part that was found                    
                    buildNewPC.Components[componentIndex].BuildPart = BuildPart;
                }
            }

            // Check if there's any selected build part in the component list
            List<Component>? componentsWithBuildParts = buildNewPC.Components.Where(c => c.BuildPart is not null).ToList();
            if (componentsWithBuildParts.Any())
            {
                // Add more GPUs for dual GPU build
                Component? gpuComponent = buildNewPC.Components.Where(c => c.PartType == PartType.GPU).FirstOrDefault();
                if (gpuComponent != null)
                {
                    if (buildNewPC.Parameter.MustHaveDualGPU)
                    {
                        buildNewPC.Components.Add((Component)gpuComponent.Clone());
                    }
                }

                // Add more memories (considering memory channel count)
                Component? memoryComponent = buildNewPC.Components.Where(c => c.PartType == PartType.Memory).FirstOrDefault();
                if (memoryComponent != null)
                {
                    for (int i = 1; i < buildNewPC.Parameter.MemoryChannels; i++)
                    {
                        buildNewPC.Components.Add((Component)memoryComponent.Clone());
                    }
                }

                // Add more fans (considering free slots)
                Component? fanComponent = buildNewPC.Components.Where(c => c.PartType == PartType.CaseFan).FirstOrDefault();
                if (fanComponent != null)
                {
                    var freeSlots = await _caseFanService.CheckFanFreeSlots(buildNewPC, true, _caseService, _cpuCoolerService, _wcRadiatorService);

                    for (int i = 1; i <= (freeSlots.Fan120 + freeSlots.Fan140); i++)
                    {
                        buildNewPC.Components.Add((Component)(fanComponent.Clone()));
                    }
                }
            }
            return buildNewPC;
        }

        public async Task<Build> ReBuildPC(Build reBuild)
        {
            if (reBuild.Components is null)
            {
                throw new ArgumentNullException(nameof(reBuild.Components));
            }            
            // Retrieve BuildPart info for each component
            await RetrieveBuildPartInfo(reBuild.Components);

            foreach (Component component in reBuild.Components.OrderBy(c => c.Priority))
            {
                if (component.BuildPart is not null)
                {
                    // Get the component original budget taking into account the other parts of the build
                    double originalPartBudget = await GetComponentBuildBudget(reBuild, component.PartType);
                    // Check if a higher priority part was updated
                    bool isHigherPriorityPartUpdated = reBuild.Components.Where(c => c.Priority < component.Priority).Any(c => c.Updated);
                    // Should find a new best part only if:
                    // - A higher priority part was updated
                    // OR
                    // - Component is not commited AND had it's budget modified
                    if (((!component.Commited) && (component.BudgetValue != originalPartBudget)) || (isHigherPriorityPartUpdated))
                    {
                        ComputerPart? newBuildPart = await FindBestBuildPart(reBuild, component.BuildPart.PartType);
                        // Check if a part was found
                        if (newBuildPart is not null)
                        {
                            // Only change part if it's different
                            if (newBuildPart.Id != component.BuildPart.Id)
                            {
                                int componentIndex = reBuild.Components.IndexOf(component);
                                // Update the component with the part that was found                    
                                reBuild.Components[componentIndex].BuildPart = newBuildPart;
                                reBuild.Components[componentIndex].Updated = true;
                            }                            
                        }
                    }
                }
            }
            return reBuild;
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

        private async Task<ComputerPart?> FindBestBuildPart(Build buildPC, PartType partType)
        {
            if (buildPC.Components is null)
            {
                throw new ArgumentNullException(nameof(buildPC.Components));
            }
            return await FindBestBuildPart(buildPC, partType, buildPC.Components.Where(c => c.PartType == partType).First().BudgetValue);
        }

        private async Task<ComputerPart?> FindBestBuildPart(Build buildPC, PartType partType, double budgetValue)
        {
            ComputerPart? selectedBuildPart = new ComputerPart();
            
            switch (partType)
            {
                case PartType.CPU:
                    selectedBuildPart = await _cpuService.FindBestCPU(buildPC, budgetValue, 
                        _motherboardService, _cpuCoolerService, _wcCpuBlockService);
                    break;
                case PartType.Motherboard:
                    selectedBuildPart = await _motherboardService.FindBestMotherboard(buildPC, budgetValue, 
                        _cpuService, _cpuCoolerService, _wcCpuBlockService, _caseService, _memoryService, _storageService, _gpuService);
                    break;
                case PartType.GPU:
                    selectedBuildPart = await _gpuService.FindBestGPU(buildPC, budgetValue,
                        _caseService, _motherboardService);
                    break;
                case PartType.CPUCooler:
                    selectedBuildPart = await _cpuCoolerService.FindBestCPUCooler(buildPC, budgetValue, 
                        _cpuService, _motherboardService, _caseService);
                    break;
                case PartType.Memory:
                    selectedBuildPart = await _memoryService.FindBestMemory(buildPC, budgetValue, 
                        _motherboardService);
                    break;
                case PartType.Storage:
                    selectedBuildPart = await _storageService.FindBestStorage(buildPC, budgetValue, 
                        _motherboardService);
                    break;
                case PartType.PSU:
                    selectedBuildPart = await _psuService.FindBestPSU(buildPC, budgetValue,
                        _cpuService, _gpuService, _caseService);
                    break;
                case PartType.Case:
                    selectedBuildPart = await _caseService.FindBestCase(buildPC, budgetValue, 
                        _cpuCoolerService, _gpuService, _motherboardService, _psuService, _wcRadiatorService);
                    break;
                case PartType.CaseFan:
                    selectedBuildPart = await _caseFanService.FindBestCaseFan(buildPC, budgetValue, 
                        _caseService, _cpuCoolerService, _wcRadiatorService);
                    break;
                case PartType.WC_CPU_Block:
                    selectedBuildPart = await _wcCpuBlockService.FindBestWCCPUBlock(buildPC, budgetValue,
                        _cpuService);
                    break;
                case PartType.WC_Radiator:
                    selectedBuildPart = await _wcRadiatorService.FindBestWCRadiator(buildPC, budgetValue,
                        _caseService);
                    break;
                case PartType.WC_Reservoir:
                    selectedBuildPart = await _wcReservoirService.FindBestWCReservoir(buildPC, budgetValue);
                    break;
            }

            return selectedBuildPart;
        }

        //Calculate build total scores
        private async Task<int> CalculateBuildTotalScore(List<Component> components, string scoreType)
        {
            int totalBasicScore = 0;
            int totalOCScore = 0;
            int totalRankingScore = 0;
            if (components.Any())
            {
                foreach (var component in components.Where(c => c.PartType == PartType.CPU || c.PartType == PartType.GPU).ToList())
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
            if (build.Components is null)
            {
                throw new ArgumentNullException(nameof(build.Components));
            }
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
            if (build.Components is null)
            {
                throw new ArgumentNullException(nameof(build.Components));
            }
            // Get default build budget
            int defaultBuildBudget = build.Parameter.Budget!.Value;
            // Get component part budget percent
            double componentPartBudgetPercent = build.Components.First(c => c.PartType == partType).BudgetPercent;
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
                if (build.Components.Any(c => c.PartType == PartType.Case))
                {
                    // For budget purposes, check for slots without considering others fans perviosly selected for build
                    var caseFreeSlots = await _caseFanService.CheckFanFreeSlots(build, false, _caseService, _cpuCoolerService, _wcRadiatorService);
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
    }
}
