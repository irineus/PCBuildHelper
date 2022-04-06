using Microsoft.EntityFrameworkCore;
using PCBuildWeb.Data;
using PCBuildWeb.Models.Building;
using PCBuildWeb.Models.Entities.Bases;
using PCBuildWeb.Models.Entities.Parts;
using PCBuildWeb.Models.Enums;

namespace PCBuildWeb.Services.Entities.Parts
{
    public class CaseFanService
    {
        private readonly PCBuildWebContext _context;
        private readonly CaseService _caseService;
        private readonly CPUCoolerService _cpuCoolerService;
        private readonly WC_RadiatorService _wcRadiatorService;

        public CaseFanService(PCBuildWebContext context, CaseService caseService, CPUCoolerService cpuCoolerService, WC_RadiatorService wcRadiatorService)
        {
            _context = context;
            _caseService = caseService;
            _cpuCoolerService = cpuCoolerService;
            _wcRadiatorService = wcRadiatorService;
        }

        public async Task<List<CaseFan>> FindAllAsync()
        {
            return await _context.CaseFan.Include(c => c.Manufacturer)
                .ToListAsync();
        }

        public async Task<CaseFan?> FindByIdAsync(int id)
        {
            return await _context.CaseFan
                .Include(c => c.Manufacturer)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<CaseFan?> FindBestCaseFan(Build build, Component component)
        {
            // Check how many fans free slots there is in the build
            var caseFreeSlots = await CheckFanFreeSlots(build);
            // Distribute the budget for case fans for the possible ammount usable in the build
            var caseFanBudget = component.BudgetValue;
            if ((caseFreeSlots.Fan120 + caseFreeSlots.Fan140) > 0)
            {
                caseFanBudget = component.BudgetValue / (caseFreeSlots.Fan120 + caseFreeSlots.Fan140);
                component.BudgetValue = caseFanBudget;
            }

            List<CaseFan>? bestCaseFan = await FindAllAsync();
            bestCaseFan = bestCaseFan
                .Where(c => c.Price <= caseFanBudget)
                .Where(c => c.LevelUnlock < build.Parameter.CurrentLevel)
                .OrderByDescending(c => c.Lighting.HasValue)
                .ThenBy(c => c.Lighting)
                .ThenByDescending(c => c.AirFlow)
                .ThenBy(c => c.Size)
                .ThenByDescending(c => c.Price)
                .ToList();

            // Check for Manufator preference
            if (build.Parameter.PreferredManufacturer != null)
            {
                if (bestCaseFan.Where(c => c.Manufacturer == build.Parameter.PreferredManufacturer).Any())
                {
                    bestCaseFan = bestCaseFan
                        .Where(c => c.Manufacturer == build.Parameter.PreferredManufacturer)
                        .ToList();
                }
            }

            if ((caseFreeSlots.Fan120 > 0) && (caseFreeSlots.Fan140 > 0))
            {
                // Case support for both 120mm and 140mm, the universal fan will be 140mm
                bestCaseFan = bestCaseFan
                    .Where(f => (f.Size == 120))
                    .ToList();
            }
            else
            {
                // Case support left only for 140mm, fit this one
                if (caseFreeSlots.Fan140 > 0)
                {
                    bestCaseFan = bestCaseFan
                        .Where(f => f.Size == 140)
                        .ToList();
                }
                else
                {
                    // No case support for more fans
                    bestCaseFan = null;
                }
            }

            if (bestCaseFan != null)
            {
                return bestCaseFan.FirstOrDefault();
            }
            return null;
        }

        public async Task<(int Fan120, int Fan140)> CheckFanFreeSlots(Build build)
        {
            // First of all, check for case already included fans and free slots
            (int Fan120, int Fan140) caseFreeSlots = (0, 0);
            (int Fan120, int Fan140) caseTotalSlots = (0, 0);
            (int Fan120, int Fan140) caseIncludedFans = (0, 0);
            // Check if there's any selected build part in the component list
            List<Component>? componentsWithBuildParts = build.Components.Where(c => c.BuildPart is not null).ToList();
            if (componentsWithBuildParts.Any())
            {
                Component? preRequisiteComponent = build.Components.Where(c => c.BuildPart!.PartType == PartType.Case).FirstOrDefault();
                if (preRequisiteComponent != null)
                {
                    ComputerPart? preRequisiteComputerPart = null;
                    preRequisiteComputerPart = preRequisiteComponent.BuildPart;
                    if (preRequisiteComputerPart != null)
                    {
                        Case? selectedCase = await _caseService.FindByIdAsync(preRequisiteComputerPart.Id);
                        if (selectedCase != null)
                        {
                            caseIncludedFans.Fan120 = selectedCase.CaseFans == null ? 0
                                : selectedCase.CaseFans.Where(f => f.Size == 120).Count();
                            caseIncludedFans.Fan140 = selectedCase.CaseFans == null ? 0
                                : selectedCase.CaseFans.Where(f => f.Size == 140).Count();
                            caseTotalSlots = (selectedCase.Number120mmSlots, selectedCase.Number140mmSlots);
                            caseFreeSlots = (caseTotalSlots.Fan120 - caseIncludedFans.Fan120, caseTotalSlots.Fan140 - caseIncludedFans.Fan140);
                        }
                    }
                }

                // Check if some slots were occuppied by AIO cooler
                preRequisiteComponent = build.Components.Where(c => c.BuildPart!.PartType == PartType.CPUCooler).FirstOrDefault();
                if (preRequisiteComponent != null)
                {
                    ComputerPart? preRequisiteComputerPart = null;
                    preRequisiteComputerPart = preRequisiteComponent.BuildPart;
                    if (preRequisiteComputerPart != null)
                    {
                        CPUCooler? selectedCPUCooler = await _cpuCoolerService.FindByIdAsync(preRequisiteComputerPart.Id);
                        if (selectedCPUCooler != null)
                        {
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
                    }
                }

                // Check if some slots were occuppied by WC Radiator
                preRequisiteComponent = build.Components.Where(c => c.BuildPart!.PartType == PartType.WC_Radiator).FirstOrDefault();
                if (preRequisiteComponent != null)
                {
                    ComputerPart? preRequisiteComputerPart = null;
                    preRequisiteComputerPart = preRequisiteComponent.BuildPart;
                    if (preRequisiteComputerPart != null)
                    {
                        WC_Radiator? selectedWCRadiator = await _wcRadiatorService.FindByIdAsync(preRequisiteComputerPart.Id);
                        if (selectedWCRadiator != null)
                        {
                            int? radiatorFanSize = selectedWCRadiator.RadiatorSize / selectedWCRadiator.RadiatorSlots;
                            if ((radiatorFanSize == 120) && (caseFreeSlots.Fan120 > 0))
                            {
                                caseFreeSlots.Fan120 -= (int)selectedWCRadiator.RadiatorSlots;
                            }
                            if ((radiatorFanSize == 140) && (caseFreeSlots.Fan140 > 0))
                            {
                                caseFreeSlots.Fan140 -= (int)selectedWCRadiator.RadiatorSlots;
                            }
                        }
                    }
                }

                // Check if there's already a fan in the build (not included by default)
                List<Component> componentList = build.Components.Where(c => c.BuildPart!.PartType == PartType.CaseFan).ToList();
                if (componentList.Count > 0)
                {
                    List<CaseFan?> buildFans = componentList.Where(c => c.BuildPart!.PartType == PartType.CaseFan).Select(c => c.BuildPart).Select(c => c as CaseFan).ToList();
                    if (buildFans is not null)
                    {
                        caseFreeSlots.Fan120 -= buildFans.Where(f => f!.Size == 120).Count();
                        caseFreeSlots.Fan140 -= buildFans.Where(f => f!.Size == 140).Count();
                    }
                }
            }
            return caseFreeSlots;
        }

        public bool CaseFanExists(int id)
        {
            return _context.CaseFan.Any(e => e.Id == id);
        }
    }
}
