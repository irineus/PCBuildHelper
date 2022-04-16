using Microsoft.EntityFrameworkCore;
using PCBuildWeb.Data;
using PCBuildWeb.Models.Building;
using PCBuildWeb.Models.Entities.Bases;
using PCBuildWeb.Models.Entities.Parts;
using PCBuildWeb.Models.Enums;
using PCBuildWeb.Services.Interfaces;
using PCBuildWeb.Utils.Filters;

namespace PCBuildWeb.Services.Entities.Parts
{
    public class CaseFanService : IBuildPartService<CaseFan>
    {
        private readonly PCBuildWebContext _context;

        public CaseFanService(PCBuildWebContext context)
        {
            _context = context;
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

        public async Task<CaseFan?> FindBestCaseFan(Build build, double budgetValue, 
            CaseService _caseService, CPUCoolerService _cpuCoolerService, WC_RadiatorService _wcRadiatorService)
        {
            List<CaseFan>? bestCaseFan = await FindAllAsync();
            bestCaseFan = bestCaseFan
                .Where(c => c.Price <= budgetValue)
                .Where(c => c.LevelUnlock < build.Parameter.CurrentLevel)
                .OrderByDescending(c => c.Lighting.HasValue)
                .ThenBy(c => c.Lighting)
                .ThenByDescending(c => c.AirFlow)
                .ThenBy(c => c.Size)
                .ThenByDescending(c => c.Price)
                .ToList();

            var caseFreeSlots = await CheckFanFreeSlots(build, true, _caseService, _cpuCoolerService, _wcRadiatorService);
            if ((caseFreeSlots.Fan120 > 0) && (caseFreeSlots.Fan140 > 0))
            {
                // Case support for both 120mm and 140mm, the universal fan will be 120mm
                bestCaseFan = BuildFilters.SimpleFilter(bestCaseFan, f => (f.Size == 120)).ToList();
            }
            else
            {
                // Case support left only for 140mm, fit this one
                if (caseFreeSlots.Fan140 > 0)
                {
                    bestCaseFan = BuildFilters.SimpleFilter(bestCaseFan, f => (f.Size == 140)).ToList();
                }
                else
                {
                    // No case support for more fans
                    bestCaseFan = null;
                }
            }

            // Check for Manufacturer preference
            bestCaseFan = BuildFilters.IfAnyFilter(bestCaseFan, c => c.Manufacturer == build.Parameter.PreferredManufacturer).ToList();

            return bestCaseFan.FirstOrDefault();
        }

        public async Task<(int Fan120, int Fan140)> CheckFanFreeSlots(Build build, bool checkForOtherFans, 
            CaseService _caseService, CPUCoolerService _cpuCoolerService, WC_RadiatorService _wcRadiatorService)
        {
            if (build is null)
            {
                return (0, 0);
            }
            if (build.Components is null)
            {
                return (0, 0);
            }
            // First of all, check for case already included fans and free slots
            (int Fan120, int Fan140) caseFreeSlots = (0, 0);
            (int Fan120, int Fan140) caseTotalSlots = (0, 0);
            (int Fan120, int Fan140) caseIncludedFans = (0, 0);
            // Check if there's any selected build part in the component list
            Case? prerequisiteCase = await BuildFilters.FindComponentPartAsync(build.Components, PartType.Case, _caseService);
            if (prerequisiteCase is not null)
            {
                caseIncludedFans.Fan120 = prerequisiteCase.CaseFans == null ? 0
                    : prerequisiteCase.CaseFans.Where(f => f.Size == 120).Count();
                caseIncludedFans.Fan140 = prerequisiteCase.CaseFans == null ? 0
                    : prerequisiteCase.CaseFans.Where(f => f.Size == 140).Count();
                caseTotalSlots = (prerequisiteCase.Number120mmSlots, prerequisiteCase.Number140mmSlots);
                caseFreeSlots = (caseTotalSlots.Fan120 - caseIncludedFans.Fan120, caseTotalSlots.Fan140 - caseIncludedFans.Fan140);
            }

            // Check if some slots were occuppied by AIO cooler
            CPUCooler? prerequisiteCPUCooler = await BuildFilters.FindComponentPartAsync(build.Components, PartType.CPUCooler, _cpuCoolerService);
            if (prerequisiteCPUCooler != null)
            {
                if (prerequisiteCPUCooler.WaterCooler)
                {
                    int? radiatorFanSize = prerequisiteCPUCooler.RadiatorSize / prerequisiteCPUCooler.RadiatorSlots;
                    if ((radiatorFanSize == 120) && (caseFreeSlots.Fan120 > 0) && (prerequisiteCPUCooler.RadiatorSlots != null))
                    {
                        caseFreeSlots.Fan120 -= (int)prerequisiteCPUCooler.RadiatorSlots;
                    }
                    if ((radiatorFanSize == 140) && (caseFreeSlots.Fan140 > 0) && (prerequisiteCPUCooler.RadiatorSlots != null))
                    {
                        caseFreeSlots.Fan140 -= (int)prerequisiteCPUCooler.RadiatorSlots;
                    }
                }
            }

            // Check if some slots were occuppied by WC Radiator
            WC_Radiator? prerequisiteWCRadiator = await BuildFilters.FindComponentPartAsync(build.Components, PartType.WC_Radiator, _wcRadiatorService);
            if (prerequisiteWCRadiator != null)
            {
                int? radiatorFanSize = prerequisiteWCRadiator.RadiatorSize / prerequisiteWCRadiator.RadiatorSlots;
                if ((radiatorFanSize == 120) && (caseFreeSlots.Fan120 > 0))
                {
                    caseFreeSlots.Fan120 -= (int)prerequisiteWCRadiator.RadiatorSlots;
                }
                if ((radiatorFanSize == 140) && (caseFreeSlots.Fan140 > 0))
                {
                    caseFreeSlots.Fan140 -= (int)prerequisiteWCRadiator.RadiatorSlots;
                }
            }

            if (checkForOtherFans)
            {
                //Check if there's already a fan in the build (not included by default)
                List<Component> componentList = build.Components.Where(c => c.PartType == PartType.CaseFan).ToList();
                if (componentList.Any())
                {
                    foreach (Component component in componentList)
                    {
                        if (component.BuildPart is not null)
                        {
                            CaseFan? fanComponent = await FindByIdAsync(component.BuildPart.Id);
                            if (fanComponent is not null)
                            {
                                if (fanComponent.Size == 120)
                                {
                                    if (caseFreeSlots.Fan120 > 0)
                                    {
                                        // Case there's still 120mm free slots, decrement from it
                                        caseFreeSlots.Fan120--;
                                    }
                                    else
                                    {
                                        // If there's no more 120mm free slots, it's using an 140mm slot. So decrement from 140mm
                                        caseFreeSlots.Fan140--;
                                    }

                                }
                                else if (fanComponent.Size == 140)
                                {
                                    // 140mm fans decrements only 140mm slots
                                    caseFreeSlots.Fan140--;
                                }
                            }

                        }
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
