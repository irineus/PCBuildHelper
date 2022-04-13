using Microsoft.EntityFrameworkCore;
using PCBuildWeb.Data;
using PCBuildWeb.Models.Building;
using PCBuildWeb.Models.Entities.Parts;
using PCBuildWeb.Utils.Filters;
using PCBuildWeb.Services.Interfaces;

namespace PCBuildWeb.Services.Entities.Parts
{
    public class CPUService : IBuildPartService<CPU>
    {
        private readonly PCBuildWebContext _context;

        public CPUService(PCBuildWebContext context)
        {
            _context = context;
        }

        public async Task<List<CPU>> FindAllAsync()
        {
            return await _context.CPU
                .Include(c => c.CPUSocket)
                .Include(c => c.Manufacturer)
                .Include(c => c.Series)
                .ToListAsync();
        }

        public async Task<CPU?> FindByIdAsync(int id)
        {
            return await _context.CPU
                .Include(c => c.CPUSocket)
                .Include(c => c.Manufacturer)
                .Include(c => c.Series)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        //Find best CPU for the build parameters
        public async Task<CPU?> FindBestCPU(Build build, double budgetValue)
        {
            if (build.Parameter is null) 
                throw new ArgumentNullException(nameof(build.Parameter));
            if (build.Components is null) 
                throw new ArgumentNullException(nameof(build.Components));

            List<CPU> bestCPU = await FindAllAsync();
            bestCPU = bestCPU
                .Where(c => c.Price <= budgetValue)
                .Where(c => c.LevelUnlock < build.Parameter.CurrentLevel)
                .OrderByDescending(c => c.RankingScore) // Order by ranking score
                .ThenByDescending(c => c.Price)
                .ToList();

            // Check for Clock Target
            bestCPU = build.Parameter.TargetCPUClock is null ? bestCPU : BuildFilters.SimpleFilter(bestCPU, c => c.OverclockedFrequency >= build.Parameter.TargetCPUClock).ToList();

            // Check for Memory Channels
            bestCPU = BuildFilters.SimpleFilter(bestCPU, c => c.MaxMemoryChannels >= build.Parameter.MemoryChannels).ToList();

            // Check for Manufacturer preference
            bestCPU = BuildFilters.IfAnyFilter(bestCPU, c => c.Manufacturer == build.Parameter.PreferredManufacturer).ToList();

            return bestCPU.FirstOrDefault();
        }

        public bool CPUExists(int id)
        {
            return _context.CPU.Any(e => e.Id == id);
        }
    }
}
