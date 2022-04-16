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
    public class PSUService : IBuildPartService<PSU>
    {
        private readonly PCBuildWebContext _context;

        public PSUService(PCBuildWebContext context)
        {
            _context = context;
        }

        public async Task<List<PSU>> FindAllAsync()
        {
            return await _context.PSU
                .Include(p => p.Manufacturer)
                .Include(p => p.PSUSize)
                .ToListAsync();
        }

        public async Task<PSU?> FindByIdAsync(int id)
        {
            return await _context.PSU
                .Include(p => p.Manufacturer)
                .Include(p => p.PSUSize)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        //Find best PSU for the build parameters
        public async Task<PSU?> FindBestPSU(Build build, double budgetValue, 
            CPUService _cpuService, GPUService _gpuService, CaseService _caseService)
        {
            List<PSU> bestPSU = await FindAllAsync();
            bestPSU = bestPSU
                .Where(c => c.Price <= budgetValue)
                .Where(c => c.LevelUnlock < build.Parameter.CurrentLevel)
                .OrderByDescending(c => c.Type) //Prioritize Modular
                .ThenByDescending(c => c.Wattage) //Prioritize Power Output
                .ThenByDescending(c => c.Price)
                .ToList();

            int neededPower = 0;
            // Sum CPU power needs from the one selected in the build
            CPU? prerequisiteCPU = await BuildFilters.FindPrerequisitePartAsync(build.Components, PartType.PSU, PartType.CPU, _cpuService);
            if (prerequisiteCPU is not null)
            {
                neededPower += prerequisiteCPU.Overclockable ? (int)(prerequisiteCPU.Wattage * 1.25) : prerequisiteCPU.Wattage;
            }
            // Sum GPU power needs from the one selected in the build
            GPU? prerequisiteGPU = await BuildFilters.FindPrerequisitePartAsync(build.Components, PartType.PSU, PartType.GPU, _gpuService);
            if (prerequisiteGPU is not null)
            {
                neededPower += prerequisiteGPU.ChipsetBrand == GPUChipsetBrand.AMD_RADEON ? (int)(prerequisiteGPU.Wattage * 1.5) : (int)(prerequisiteGPU.Wattage * 1.2);
            }
            
            // Add 10% power margin
            bestPSU = BuildFilters.SimpleFilter(bestPSU,p => p.Wattage >= (neededPower * 1.1)).ToList();

            // Filter PSU by case size and form factor support
            Case? prerequisiteCase = await BuildFilters.FindPrerequisitePartAsync(build.Components, PartType.PSU, PartType.Case, _caseService);
            bestPSU = prerequisiteCase is null ? bestPSU : BuildFilters.SimpleFilter(bestPSU, p => prerequisiteCase.PSUSizes.Contains(p.PSUSize)).ToList();
            bestPSU = prerequisiteCase is null ? bestPSU : BuildFilters.SimpleFilter(bestPSU, p => p.Length <= prerequisiteCase.MaxPsuLength).ToList();


            // Check for Manufacturer preference
            bestPSU = BuildFilters.IfAnyFilter(bestPSU, c => c.Manufacturer == build.Parameter.PreferredManufacturer).ToList();

            return bestPSU.FirstOrDefault();
        }

        public bool PSUExists(int id)
        {
            return _context.PSU.Any(e => e.Id == id);
        }
    }
}
