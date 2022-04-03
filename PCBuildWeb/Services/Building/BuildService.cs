using PCBuildWeb.Data;
using PCBuildWeb.Models.Building;
using PCBuildWeb.Models.Entities.Bases;
using PCBuildWeb.Models.Entities.Parts;
using PCBuildWeb.Models.Enums;
using PCBuildWeb.Utils;
using Microsoft.EntityFrameworkCore;
using PCBuildWeb.Services.Entities.Parts;

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

        public BuildService(PCBuildWebContext context, CPUService cpuService, MotherboardService motherboardService,
            GPUService gpuService, CPUCoolerService cpuCoolerService, MemoryService memoryService, PSUService psuService,
            StorageService storageService, CaseFanService caseFanService, WC_CPU_BlockService wcCpuBlockService,
            WC_RadiatorService wcRadiatorService, WC_ReservoirService wcReservoirService, CaseService caseService)
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
        }

        public async Task BuildNewPC()
        {
            Build newBuild = new Build()
            {
                Budget = 2000,
                CurrentLevel = 22,
                CurrentLevelPercent = 1,
                MemoryChannels = 2,
                PreferredManufacturer = null,
                TargetMemorySize = null,
                TargetScore = 3000
            };

            //Select build type
            newBuild.BuildType = new BuildType(TypeEnum.Usual);
            newBuild.Components = new List<Component>();

            // Search the best coponent for the build (order by priority to build acordely)
            foreach (var component in newBuild.BuildType.BuildPartSpecs.OrderBy(b => b.Priority))
            {
                //Set build component basic properties
                var newComponent = (new Component()
                {
                    Type = component.Type,
                    Priority = component.Priority,
                    BudgetPercent = component.BudgetPercent,
                    BudgetValue = newBuild.Budget * component.BudgetPercent
                });

                switch (component.Type)
                {
                    case PartType.CPU:
                        CPU? cpu = await _cpuService.FindBestCPU(newBuild, newComponent);
                        newComponent.BuildPart = cpu;
                        break;
                    case PartType.Motherboard:
                        Motherboard? mobo = await _motherboardService.FindBestMotherboard(newBuild, newComponent);
                        newComponent.BuildPart = mobo;
                        break;
                    case PartType.GPU:
                        GPU? gpu = await _gpuService.FindBestGPU(newBuild, newComponent);
                        newComponent.BuildPart = gpu;
                        break;
                    case PartType.CPUCooler:
                        CPUCooler? cpuCooler = await _cpuCoolerService.FindBestCPUCooler(newBuild, newComponent);
                        newComponent.BuildPart = cpuCooler;
                        break;
                    case PartType.Memory:
                        Memory? memory = await _memoryService.FindBestMemory(newBuild, newComponent);
                        newComponent.BuildPart = memory;
                        break;
                    case PartType.Storage:
                        Storage? storage = await _storageService.FindBestStorage(newBuild, newComponent);
                        newComponent.BuildPart = storage;
                        break;
                    case PartType.PSU:
                        PSU? psu = await _psuService.FindBestPSU(newBuild, newComponent);
                        newComponent.BuildPart = psu;
                        break;
                    case PartType.Case:
                        Case? casePart = await _caseService.FindBestCase(newBuild, newComponent);
                        newComponent.BuildPart = casePart;
                        break;
                    case PartType.CaseFan:
                        CaseFan? caseFan = await _caseFanService.FindBestCaseFan(newBuild, newComponent);
                        newComponent.BuildPart = caseFan;
                        break;
                    case PartType.WC_CPU_Block:
                        WC_CPU_Block? wc_cpu_block = await _wcCpuBlockService.FindBestWCCPUBlock(newBuild, newComponent);
                        newComponent.BuildPart = wc_cpu_block;
                        break;
                    case PartType.WC_Radiator:
                        WC_Radiator? wc_radiator = await _wcRadiatorService.FindBestWCRadiator(newBuild, newComponent);
                        newComponent.BuildPart = wc_radiator;
                        break;
                    case PartType.WC_Reservoir:
                        WC_Reservoir? wc_reservoir = await _wcReservoirService.FindBestWCReservoir(newBuild, newComponent);
                        newComponent.BuildPart = wc_reservoir;
                        break;
                }
                newBuild.Components.Add(newComponent);
            }

            // Add more memories (considering memory channel count)
            Component? memoryComponent = newBuild.Components.Where(c => c.Type == PartType.Memory).FirstOrDefault();
            if (memoryComponent != null)
            {
                for (int i = 1; i < newBuild.MemoryChannels; i++)
                {
                    newBuild.Components.Add(CopyComponent(memoryComponent));
                }
            }

            // Add more fans (considering free slots)
            Component? fanComponent = newBuild.Components.Where(c => c.Type == PartType.CaseFan).FirstOrDefault();
            if (fanComponent != null)
            {
                var freeSlots = await _caseFanService.CheckFanFreeSlots(newBuild);

                for (int i = 1; i < (freeSlots.Fan120 + freeSlots.Fan140); i++)
                {
                    newBuild.Components.Add(CopyComponent(fanComponent));
                }
            }
        }

        //Copy the component to a new one
        public Component CopyComponent(Component component)
        {
            Component newComponent = new Component()
            {
                Type = component.Type,
                Priority = component.Priority,
                BudgetPercent = component.BudgetPercent,
                BudgetValue = component.BudgetValue,
                BuildPart = component.BuildPart
            };

            return newComponent;
        }
    }
}
