﻿using PCBuildWeb.Data;
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
        public readonly CPUCoolerService _cpuCoolerService;
        public readonly CPUService _cpuService;
        public readonly GPUService _gpuService;
        public readonly MotherboardService _motherboardService;

        public BuildService(PCBuildWebContext context, CPUService cpuService, MotherboardService motherboardService, 
            GPUService gpuService, CPUCoolerService cpuCoolerService)
        {
            _context = context;
            _cpuService = cpuService;
            _motherboardService = motherboardService;
            _gpuService = gpuService;
            _cpuCoolerService = cpuCoolerService;
        }

        public async void BuildNewPC()
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
                        Memory? memory = FindBestMemory(newBuild, newComponent);
                        newComponent.BuildPart = memory;
                        break;
                    case PartType.Storage:
                        Storage? storage = FindBestStorage(newBuild, newComponent);
                        newComponent.BuildPart = storage;
                        break;
                    case PartType.PSU:
                        PSU? psu = FindBestPSU(newBuild, newComponent);
                        newComponent.BuildPart = psu;
                        break;
                    case PartType.Case:
                        Case? casePart = FindBestCase(newBuild, newComponent);
                        newComponent.BuildPart = casePart;
                        break;
                    case PartType.CaseFan:
                        CaseFan? caseFan = FindBestCaseFan(newBuild, newComponent);
                        newComponent.BuildPart = caseFan;
                        break;
                    case PartType.WC_CPU_Block:
                        WC_CPU_Block? wc_cpu_block = FindBestWCCPUBlock(newBuild, newComponent);
                        newComponent.BuildPart = wc_cpu_block;
                        break;
                    case PartType.WC_Radiator:
                        WC_Radiator? wc_radiator = FindBestWCRadiator(newBuild, newComponent);
                        newComponent.BuildPart = wc_radiator;
                        break;
                    case PartType.WC_Reservoir:
                        WC_Reservoir? wc_reservoir = FindBestWCReservoir(newBuild, newComponent);
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
                    Component newMemoryComponent = Helper.CreateDeepCopy(memoryComponent);
                    newBuild.Components.Add(newMemoryComponent);
                    //newBuild.Components.Add(new Component()
                    //{
                    //    Type = memoryComponent.Type,
                    //    Priority = memoryComponent.Priority,
                    //    BudgetPercent = memoryComponent.BudgetPercent,
                    //    BudgetValue = memoryComponent.BudgetValue,
                    //    BuildPart = memoryComponent.BuildPart                        
                    //});
                }
            }

            // Add more fans (considering free slots)
            Component? fanComponent = newBuild.Components.Where(c => c.Type == PartType.CaseFan).FirstOrDefault();
            if (fanComponent != null)
            {
                var freeSlots = CheckFanFreeSlots(newBuild);

                for (int i = 1; i < (freeSlots.Fan120 + freeSlots.Fan140); i++)
                {
                    Component newFanComponent = Helper.CreateDeepCopy(fanComponent);
                    newBuild.Components.Add(newFanComponent);
                    //newBuild.Components.Add(new Component()
                    //{
                    //    Type = fanComponent.Type,
                    //    Priority = fanComponent.Priority,
                    //    BudgetPercent = fanComponent.BudgetPercent,
                    //    BudgetValue = fanComponent.BudgetValue,
                    //    BuildPart = fanComponent.BuildPart
                    //});
                }
            }
        }

        //Find best Memory for the build parameters
        public Memory? FindBestMemory(Build build, Component component)
        {
            //Should consider multi-channel memory for budget and size of each chip
            var memoryBudget = component.BudgetValue / build.MemoryChannels;
            var memorySize = build.TargetMemorySize / build.MemoryChannels;

            var bestMemory = _context.Memory
                                .Where(c => c.Price <= memoryBudget)
                                .Where(c => c.LevelUnlock <= build.CurrentLevel)
                                .Where(c => c.LevelPercent <= build.CurrentLevelPercent)
                                .Where(c => c.Size >= memorySize)
                                .OrderByDescending(c => c.Price);

            // Check for Manufator preference
            if (build.PreferredManufacturer != null)
            {
                if (bestMemory.Where(c => c.Manufacturer == build.PreferredManufacturer).Any())
                {
                    bestMemory = bestMemory.Where(c => c.Manufacturer == build.PreferredManufacturer).OrderByDescending(c => c.Price);
                }
            }

            Component? preRequisiteComponent = build.Components.Where(c => c.Type == PartType.Motherboard).FirstOrDefault();
            ComputerPart? preRequisiteComputerPart = null;
            if (preRequisiteComponent != null)
            {
                preRequisiteComputerPart = preRequisiteComponent.BuildPart;
            }
            if (preRequisiteComputerPart != null)
            {
                Motherboard selectedMobo = (Motherboard)preRequisiteComputerPart;
                bestMemory = bestMemory.Where(m => m.Frequency <= selectedMobo.MaxRamSpeed).OrderByDescending(c => c.Price);
            }

            return bestMemory.FirstOrDefault(); ;
        }

        //Find best storage for the build parameters
        public Storage? FindBestStorage(Build build, Component component)
        {
            var bestStorage = _context.Storage
                                .Where(c => c.Price <= component.BudgetValue)
                                .Where(c => c.LevelUnlock <= build.CurrentLevel)
                                .Where(c => c.LevelPercent <= build.CurrentLevelPercent)
                                .OrderByDescending(c => c.Speed);

            // Check for Manufator preference
            if (build.PreferredManufacturer != null)
            {
                if (bestStorage.Where(c => c.Manufacturer == build.PreferredManufacturer).Any())
                {
                    bestStorage = bestStorage.Where(c => c.Manufacturer == build.PreferredManufacturer).OrderByDescending(c => c.Price);
                }
            }

            var currentSelectedStorage = bestStorage.FirstOrDefault();
            if (currentSelectedStorage != null)
            {
                //If the best storage is a M.2, should match mobo support or else downgrade
                if (currentSelectedStorage.Type == StorageType.M_2)
                {
                    Component? preRequisiteComponent = build.Components.Where(c => c.Type == PartType.Motherboard).FirstOrDefault();
                    ComputerPart? preRequisiteComputerPart = null;
                    if (preRequisiteComponent != null)
                    {
                        preRequisiteComputerPart = preRequisiteComponent.BuildPart;
                    }
                    if (preRequisiteComputerPart != null)
                    {
                        Motherboard selectedMobo = (Motherboard)preRequisiteComputerPart;
                        if (selectedMobo.M2Slots == 0)
                        {
                            // No M.2 support => downgrade type
                            bestStorage = bestStorage.Where(s => s.Type != StorageType.M_2).OrderByDescending(s => s.Speed);
                        }
                        else
                        {
                            // Mobo supports M.2. Check for heatsink support
                            if (selectedMobo.M2SlotsSupportingHeatsinks == 0)
                            {
                                // Remove Heatsinked M.2 from list
                                bestStorage = bestStorage.Where(s => !s.IncludesHeatsink).OrderByDescending(s => s.Speed);
                            }
                        }
                    }
                }
            }

            return bestStorage.FirstOrDefault(); ;
        }

        //Find best PSU for the build parameters
        public PSU? FindBestPSU(Build build, Component component)
        {
            var bestPSU = _context.PSU
                            .Where(c => c.Price <= component.BudgetValue)
                            .Where(c => c.LevelUnlock <= build.CurrentLevel)
                            .Where(c => c.LevelPercent <= build.CurrentLevelPercent)
                            .OrderByDescending(c => c.Price);

            // Check for Manufator preference
            if (build.PreferredManufacturer != null)
            {
                if (bestPSU.Where(c => c.Manufacturer == build.PreferredManufacturer).Any())
                {
                    bestPSU = bestPSU.Where(c => c.Manufacturer == build.PreferredManufacturer).OrderByDescending(c => c.Price);
                }
            }

            // Sum used power from CPU and GPUs in the build
            int neededPower = 0;
            Component? preRequisiteComponent = build.Components.Where(c => c.Type == PartType.CPU).FirstOrDefault();
            ComputerPart? preRequisiteComputerPart = null;
            if (preRequisiteComponent != null)
            {
                preRequisiteComputerPart = preRequisiteComponent.BuildPart;
            }
            if (preRequisiteComputerPart != null)
            {
                CPU selectedCPU = (CPU)preRequisiteComputerPart;
                neededPower += selectedCPU.Wattage;
            }
            // Should consider Dual GPU Builds
            List<GPU>? preRequisiteComponents = (List<GPU>?)build.Components.Where(c => c.Type == PartType.GPU);
            if (preRequisiteComponents != null)
            {
                neededPower += preRequisiteComponents.Sum(c => c.Wattage);
            }

            // Add at least 10% power margin
            bestPSU = bestPSU.Where(p => p.Wattage >= (neededPower * 1.1)).OrderByDescending(c => c.Price);

            return bestPSU.FirstOrDefault();
        }

        //Find best Case for the build parameters
        public Case? FindBestCase(Build build, Component component)
        {
            var bestCase = _context.Case
                            .Where(c => c.Price <= component.BudgetValue)
                            .Where(c => c.LevelUnlock <= build.CurrentLevel)
                            .Where(c => c.LevelPercent <= build.CurrentLevelPercent)
                            .OrderByDescending(c => c.Price);

            // Check for Manufator preference
            if (build.PreferredManufacturer != null)
            {
                if (bestCase.Where(c => c.Manufacturer == build.PreferredManufacturer).Any())
                {
                    bestCase = bestCase.Where(c => c.Manufacturer == build.PreferredManufacturer).OrderByDescending(c => c.Price);
                }
            }

            // Check mobo size
            Component? preRequisiteComponent = build.Components.Where(c => c.Type == PartType.Motherboard).FirstOrDefault();
            ComputerPart? preRequisiteComputerPart = null;
            if (preRequisiteComponent != null)
            {
                preRequisiteComputerPart = preRequisiteComponent.BuildPart;
            }
            if (preRequisiteComputerPart != null)
            {
                Motherboard selectedMobo = (Motherboard)preRequisiteComputerPart;
                bestCase = bestCase.Where(c => c.SupportedMoboSizes.Contains(selectedMobo.Size)).OrderByDescending(c => c.Price);
            }

            // Check CPUCooler specs against case
            preRequisiteComponent = build.Components.Where(c => c.Type == PartType.CPUCooler).FirstOrDefault();
            preRequisiteComputerPart = null;
            if (preRequisiteComponent != null)
            {
                preRequisiteComputerPart = preRequisiteComponent.BuildPart;
            }
            if (preRequisiteComputerPart != null)
            {
                CPUCooler selectedCPUCooler = (CPUCooler)preRequisiteComputerPart;
                if (selectedCPUCooler.WaterCooler)
                {
                    // Watercooler should check for radiator slots
                    int? radiatorFanSize = selectedCPUCooler.RadiatorSize / selectedCPUCooler.RadiatorSlots;
                    if (radiatorFanSize == 120)
                    {
                        bestCase = bestCase.Where(c => c.Number120mmSlots >= selectedCPUCooler.RadiatorSlots).OrderByDescending(c => c.Price);
                    }
                    if (radiatorFanSize == 140)
                    {
                        bestCase = bestCase.Where(c => c.Number140mmSlots >= selectedCPUCooler.RadiatorSlots).OrderByDescending(c => c.Price);
                    }
                }
                // Always check for height
                bestCase = bestCase.Where(c => c.MaxCPUFanHeight > selectedCPUCooler.Height).OrderByDescending(c => c.Price);
            }

            // Check WC Radiator specs against case
            preRequisiteComponent = build.Components.Where(c => c.Type == PartType.WC_Radiator).FirstOrDefault();
            preRequisiteComputerPart = null;
            if (preRequisiteComponent != null)
            {
                preRequisiteComputerPart = preRequisiteComponent.BuildPart;
            }
            if (preRequisiteComputerPart != null)
            {
                WC_Radiator selectedCPUCooler = (WC_Radiator)preRequisiteComputerPart;
                int? radiatorFanSize = selectedCPUCooler.RadiatorSize / selectedCPUCooler.RadiatorSlots;
                if (radiatorFanSize == 120)
                {
                    bestCase = bestCase.Where(c => c.Number120mmSlots >= selectedCPUCooler.RadiatorSlots).OrderByDescending(c => c.Price);
                }
                if (radiatorFanSize == 140)
                {
                    bestCase = bestCase.Where(c => c.Number140mmSlots >= selectedCPUCooler.RadiatorSlots).OrderByDescending(c => c.Price);
                }
            }

            // Check PSU Length and FormFactor
            preRequisiteComponent = build.Components.Where(c => c.Type == PartType.PSU).FirstOrDefault();
            preRequisiteComputerPart = null;
            if (preRequisiteComponent != null)
            {
                preRequisiteComputerPart = preRequisiteComponent.BuildPart;
            }
            if (preRequisiteComputerPart != null)
            {
                PSU selectedPSU = (PSU)preRequisiteComputerPart;
                bestCase = bestCase.Where(c => c.SupportedPSUSizes.Contains(selectedPSU.PSUSize))
                    .Where(c => c.MaxPsuLength > selectedPSU.Length)
                    .OrderByDescending(c => c.Price);
            }

            // Check GPU Length
            preRequisiteComponent = build.Components.Where(c => c.Type == PartType.GPU).FirstOrDefault();
            preRequisiteComputerPart = null;
            if (preRequisiteComponent != null)
            {
                preRequisiteComputerPart = preRequisiteComponent.BuildPart;
            }
            if (preRequisiteComputerPart != null)
            {
                GPU selectedGPU = (GPU)preRequisiteComputerPart;
                bestCase = bestCase.Where(c => c.MaxGPULength > selectedGPU.Length).OrderByDescending(c => c.Price);
            }

            return bestCase.FirstOrDefault();
        }

        public CaseFan? FindBestCaseFan(Build build, Component component)
        {
            // Check how many fans free slots there is in the build
            var caseFreeSlots = CheckFanFreeSlots(build);
            // Distribute the budget for case fans for the possible ammount usable in the build
            var caseFanBudget = component.BudgetValue;
            if ((caseFreeSlots.Fan120 + caseFreeSlots.Fan140) > 0)
            {
                caseFanBudget = component.BudgetValue / (caseFreeSlots.Fan120 + caseFreeSlots.Fan140);
            }

            var bestCaseFan = _context.CaseFan
                            .Where(c => c.Price <= caseFanBudget)
                            .Where(c => c.LevelUnlock <= build.CurrentLevel)
                            .Where(c => c.LevelPercent <= build.CurrentLevelPercent)
                            .OrderByDescending(c => c.Price);

            // Check for Manufator preference
            if (build.PreferredManufacturer != null)
            {
                if (bestCaseFan.Where(c => c.Manufacturer == build.PreferredManufacturer).Any())
                {
                    bestCaseFan = bestCaseFan.Where(c => c.Manufacturer == build.PreferredManufacturer).OrderByDescending(c => c.Price);
                }
            }

            if ((caseFreeSlots.Fan120 > 0) && (caseFreeSlots.Fan140 > 0))
            {
                // Case support for both 120mm and 140mm
                bestCaseFan = bestCaseFan.Where(f => (f.Size == 120) || (f.Size == 140)).OrderByDescending(c => c.Price);
            }
            else
            {
                // Case support only for 120mm
                if (caseFreeSlots.Fan120 > 0)
                {
                    bestCaseFan = bestCaseFan.Where(f => f.Size == 120).OrderByDescending(c => c.Price);
                }
                else
                {
                    // Case support only for 140mm
                    if (caseFreeSlots.Fan140 > 0)
                    {
                        bestCaseFan = bestCaseFan.Where(f => f.Size == 120).OrderByDescending(c => c.Price);
                    }
                    else
                    {
                        // No case support for more fans
                        bestCaseFan = null;
                    }
                }
            }

            if (bestCaseFan != null)
            {
                return bestCaseFan.FirstOrDefault();
            }
            return null;
        }

        public (int Fan120, int Fan140) CheckFanFreeSlots(Build build)
        {
            // First of all, check for case already included fans and free slots
            (int Fan120, int Fan140) caseFreeSlots = (0, 0);
            (int Fan120, int Fan140) caseTotalSlots = (0, 0);
            (int Fan120, int Fan140) caseIncludedFans = (0, 0);
            Component? preRequisiteComponent = build.Components.Where(c => c.Type == PartType.Case).FirstOrDefault();
            ComputerPart? preRequisiteComputerPart = null;
            if (preRequisiteComponent != null)
            {
                preRequisiteComputerPart = preRequisiteComponent.BuildPart;
            }
            if (preRequisiteComputerPart != null)
            {
                Case selectedCase = (Case)preRequisiteComputerPart;
                caseIncludedFans.Fan120 = selectedCase.IncludedCaseFans == null ? 0 : selectedCase.IncludedCaseFans.Where(f => f.Size == 120).Count();
                caseIncludedFans.Fan140 = selectedCase.IncludedCaseFans == null ? 0 : selectedCase.IncludedCaseFans.Where(f => f.Size == 140).Count();
                caseTotalSlots = (selectedCase.Number120mmSlots, selectedCase.Number140mmSlots);
                caseFreeSlots = (caseTotalSlots.Fan120 - caseIncludedFans.Fan120, caseTotalSlots.Fan140 - caseIncludedFans.Fan140);
            }
            // Check if some slots were occuppied by WC radiator
            preRequisiteComponent = build.Components.Where(c => c.Type == PartType.CPUCooler).FirstOrDefault();
            preRequisiteComputerPart = null;
            if (preRequisiteComponent != null)
            {
                preRequisiteComputerPart = preRequisiteComponent.BuildPart;
            }
            if (preRequisiteComputerPart != null)
            {
                CPUCooler selectedCPUCooler = (CPUCooler)preRequisiteComputerPart;
                if (selectedCPUCooler.WaterCooler)
                {
                    int? radiatorFanSize = selectedCPUCooler.RadiatorSize / selectedCPUCooler.RadiatorSlots;
                    if ((radiatorFanSize == 120) && (caseFreeSlots.Fan120 > 0) && (selectedCPUCooler.RadiatorSlots != null))
                    {
                        caseFreeSlots.Fan120 -= (int)selectedCPUCooler.RadiatorSlots;
                    }
                    if ((radiatorFanSize == 140) && (caseFreeSlots.Fan140 > 0) && (selectedCPUCooler.RadiatorSlots != null))
                    {
                        caseFreeSlots.Fan140 -= (int)selectedCPUCooler.RadiatorSlots;
                    }
                }
            }
            // Check if there's already a fan in the build (not included by default)
            List<Component> componentList = build.Components.Where(c => c.Type == PartType.CaseFan).ToList();
            if (componentList != null)
            {
                List<CaseFan> buildFans = (List<CaseFan>)componentList.Where(c => c.Type == PartType.CaseFan);
                caseFreeSlots.Fan120 -= buildFans.Where(f => f.Size == 120).Count();
                caseFreeSlots.Fan140 -= buildFans.Where(f => f.Size == 140).Count();
            }

            return caseFreeSlots;
        }

        // Find best Custom WaterCooler CPU Block for the build
        public WC_CPU_Block? FindBestWCCPUBlock(Build build, Component component)
        {
            var bestWC_CPU_Block = _context.WC_CPU_Block
                            .Where(c => c.Price <= component.BudgetValue)
                            .Where(c => c.LevelUnlock <= build.CurrentLevel)
                            .Where(c => c.LevelPercent <= build.CurrentLevelPercent)
                            .OrderByDescending(c => c.Price);

            // Check for Manufator preference
            if (build.PreferredManufacturer != null)
            {
                if (bestWC_CPU_Block.Where(c => c.Manufacturer == build.PreferredManufacturer).Any())
                {
                    bestWC_CPU_Block = bestWC_CPU_Block.Where(c => c.Manufacturer == build.PreferredManufacturer).OrderByDescending(c => c.Price);
                }
            }

            // Check CPU Socket Type
            Component? preRequisiteComponent = build.Components.Where(c => c.Type == PartType.CPU).FirstOrDefault();
            ComputerPart? preRequisiteComputerPart = null;
            if (preRequisiteComponent != null)
            {
                preRequisiteComputerPart = preRequisiteComponent.BuildPart;
            }
            if (preRequisiteComputerPart != null)
            {
                CPU selectedCPU = (CPU)preRequisiteComputerPart;
                bestWC_CPU_Block = bestWC_CPU_Block.Where(c => c.SupportedCPUSockets.Contains(selectedCPU.CPUSocket)).OrderByDescending(c => c.Price);
            }

            return bestWC_CPU_Block.FirstOrDefault();

        }

        // Find best Custom WaterCooler Radiator for the build
        public WC_Radiator? FindBestWCRadiator(Build build, Component component)
        {
            var bestWC_Radiator = _context.WC_Radiator
                            .Where(c => c.Price <= component.BudgetValue)
                            .Where(c => c.LevelUnlock <= build.CurrentLevel)
                            .Where(c => c.LevelPercent <= build.CurrentLevelPercent)
                            .OrderByDescending(c => c.Price);

            // Check for Manufator preference
            if (build.PreferredManufacturer != null)
            {
                if (bestWC_Radiator.Where(c => c.Manufacturer == build.PreferredManufacturer).Any())
                {
                    bestWC_Radiator = bestWC_Radiator.Where(c => c.Manufacturer == build.PreferredManufacturer).OrderByDescending(c => c.Price);
                }
            }

            //The only check is made at case

            return bestWC_Radiator.FirstOrDefault();
        }

        // Find best Custom WaterCooler Reservoir for the build
        public WC_Reservoir? FindBestWCReservoir(Build build, Component component)
        {
            var bestWC_Reservoir = _context.WC_Reservoir
                            .Where(c => c.Price <= component.BudgetValue)
                            .Where(c => c.LevelUnlock <= build.CurrentLevel)
                            .Where(c => c.LevelPercent <= build.CurrentLevelPercent)
                            .OrderByDescending(c => c.Price);

            // Check for Manufator preference
            if (build.PreferredManufacturer != null)
            {
                if (bestWC_Reservoir.Where(c => c.Manufacturer == build.PreferredManufacturer).Any())
                {
                    bestWC_Reservoir = bestWC_Reservoir.Where(c => c.Manufacturer == build.PreferredManufacturer).OrderByDescending(c => c.Price);
                }
            }

            //The only check is made at case

            return bestWC_Reservoir.FirstOrDefault();
        }
    }
}
