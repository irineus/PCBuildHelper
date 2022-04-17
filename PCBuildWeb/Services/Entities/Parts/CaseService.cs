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
    public class CaseService : IBuildPartService<Case>
    {
        private readonly PCBuildWebContext _context;

        public CaseService(PCBuildWebContext context)
        {
            _context = context;
        }

        public async Task<List<Case>> FindAllAsync()
        {
            return await _context.Case
                .Include(c => c.Manufacturer)
                .Include(c => c.MoboSizes)
                .Include(c => c.PSUSizes)
                .Include(c => c.MoboSizes)
                .Include(c => c.CaseFans)
                .ToListAsync();
        }

        public async Task<Case?> FindByIdAsync(int id)
        {
            return await _context.Case
                .Include(c => c.Manufacturer)
                .Include(c => c.MoboSizes)
                .Include(c => c.PSUSizes)
                .Include(c => c.MoboSizes)
                .Include(c => c.CaseFans)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        //Find best Case for the build parameters
        public async Task<Case?> FindBestCase(Build build, double budgetValue,
            CPUCoolerService _cpuCoolerService, GPUService _gpuService, MotherboardService _motherboardService, 
            PSUService _psuService, WC_RadiatorService _wcRadiatorService)
        {
            List<Case> bestCase = await FindAllAsync();
            bestCase = bestCase
                .Where(c => c.Price <= budgetValue)
                .Where(c => c.LevelUnlock < build.Parameter.CurrentLevel)
                .OrderByDescending(c => (c.Number120mmSlots + c.Number140mmSlots))
                .ThenByDescending(c => c.CaseSize)
                .ThenByDescending(c => c.MaxGPULength)
                .ThenByDescending(c => c.MaxPsuLength)
                .ThenByDescending(c => c.MaxCPUFanHeight)
                .ThenByDescending(c => c.Price)
                .ToList();

            bestCase = !build.Parameter.EnableOpenBench ? BuildFilters.SimpleFilter(bestCase, c => !c.IsOpenBench).ToList() : bestCase;

            // Check if there's any selected build part in the component list
            Motherboard? prerequisiteMobo = await BuildFilters.FindPrerequisitePartAsync(build.Components, PartType.Case, PartType.Motherboard, _motherboardService);
            bestCase = prerequisiteMobo is null ? bestCase : BuildFilters.SimpleFilter(bestCase, c => c.MoboSizes.Contains(prerequisiteMobo.Size)).ToList();

            // Check CPUCooler specs against case
            CPUCooler? prerequisiteCPUCooler = await BuildFilters.FindPrerequisitePartAsync(build.Components, PartType.Case, PartType.CPUCooler, _cpuCoolerService);
            if (prerequisiteCPUCooler is not null)
            {
                if (prerequisiteCPUCooler is not null)
                {
                    if (prerequisiteCPUCooler.WaterCooler)
                    {
                        // Watercooler should check for radiator slots
                        int? radiatorFanSize = prerequisiteCPUCooler.RadiatorSize / prerequisiteCPUCooler.RadiatorSlots;
                        if (radiatorFanSize == 120)
                        {
                            // 120mm radiator can go in both 120 or 140mm slots
                            bestCase = BuildFilters.SimpleFilter(bestCase, c => (c.Number120mmSlots >= prerequisiteCPUCooler.RadiatorSlots) || (c.Number140mmSlots >= prerequisiteCPUCooler.RadiatorSlots)).ToList();
                        }
                        if (radiatorFanSize == 140)
                        {
                            // restrict for 140mm slots only
                            bestCase = BuildFilters.SimpleFilter(bestCase, c => c.Number140mmSlots >= prerequisiteCPUCooler.RadiatorSlots).ToList();
                        }
                    }
                    // Always check for height
                    bestCase = BuildFilters.SimpleFilter(bestCase, c => c.MaxCPUFanHeight > prerequisiteCPUCooler.Height).ToList();
                }
            }

            // Check WC Radiator specs against case
            WC_Radiator? prerequisiteWCRadiator = await BuildFilters.FindPrerequisitePartAsync(build.Components, PartType.Case, PartType.WC_Radiator, _wcRadiatorService);
            if (prerequisiteWCRadiator is not null)
            {
                int? radiatorFanSize = prerequisiteWCRadiator.RadiatorSize / prerequisiteWCRadiator.RadiatorSlots;
                if (radiatorFanSize == 120)
                {
                    bestCase = BuildFilters.SimpleFilter(bestCase, c => c.Number120mmSlots >= prerequisiteWCRadiator.RadiatorSlots).ToList();
                }
                if (radiatorFanSize == 140)
                {
                    bestCase = BuildFilters.SimpleFilter(bestCase, c => c.Number140mmSlots >= prerequisiteWCRadiator.RadiatorSlots).ToList();
                }
            }

            // Check PSU Length and FormFactor
            PSU? prerequisitePSU = await BuildFilters.FindPrerequisitePartAsync(build.Components, PartType.Case, PartType.PSU, _psuService);
            bestCase = prerequisitePSU is null ? bestCase : BuildFilters.MultipleFilter(bestCase, c => c.PSUSizes.Contains(prerequisitePSU.PSUSize), 
                                                                                                  c => c.MaxPsuLength > prerequisitePSU.Length).ToList();

            // Check GPU Length
            GPU? prerequisiteGPU = await BuildFilters.FindPrerequisitePartAsync(build.Components, PartType.Case, PartType.GPU, _gpuService);
            bestCase = prerequisiteGPU is null ? bestCase : BuildFilters.SimpleFilter(bestCase, c => c.MaxGPULength > prerequisiteGPU.Length).ToList();

            // Check for Manufacturer preference
            bestCase = BuildFilters.IfAnyFilter(bestCase, c => c.Manufacturer == build.Parameter.PreferredManufacturer).ToList();

            return bestCase.FirstOrDefault();
        }

        public bool CaseExists(int id)
        {
            return _context.Case.Any(e => e.Id == id);
        }
    }
}
