using Microsoft.EntityFrameworkCore;
using PCBuildWeb.Data;
using PCBuildWeb.Models.Building;
using PCBuildWeb.Models.Entities.Parts;

namespace PCBuildWeb.Services.Entities.Parts
{
    public class CPUService
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
        public async Task<CPU?> FindBestCPU(Build build, Component component)
        {
            List<CPU> bestCPU = await FindAllAsync();
            bestCPU = bestCPU
                .Where(c => c.Price <= component.BudgetValue)
                .Where(c => c.LevelUnlock <= build.CurrentLevel)
                .Where(c => c.LevelPercent <= build.CurrentLevelPercent)
                .OrderByDescending(c => c.Price)
                .ToList();

            // Check for Manufator preference
            if (build.PreferredManufacturer != null)
            {
                if (bestCPU.Where(c => c.Manufacturer == build.PreferredManufacturer).Any())
                {
                    bestCPU = bestCPU
                        .Where(c => c.Manufacturer == build.PreferredManufacturer)
                        .OrderByDescending(c => c.Price)
                        .ToList();
                }
            }

            if (build.MemoryChannels > 0)
            {
                bestCPU = bestCPU.Where(c => c.MaxMemoryChannels <= build.MemoryChannels).OrderByDescending(c => c.Price).ToList();
            }

            return bestCPU.FirstOrDefault();
            
        }

        public bool CPUExists(int id)
        {
            return _context.CPU.Any(e => e.Id == id);
        }
    }
}
