using Microsoft.EntityFrameworkCore;
using PCBuildWeb.Data;
using PCBuildWeb.Models.Building;
using PCBuildWeb.Models.Entities.Parts;
using PCBuildWeb.Services.Interfaces;
using PCBuildWeb.Utils.Filters;

namespace PCBuildWeb.Services.Entities.Parts
{
    public class WC_ReservoirService : IBuildPartService<WC_Reservoir>
    {
        private readonly PCBuildWebContext _context;

        public WC_ReservoirService(PCBuildWebContext context)
        {
            _context = context;
        }

        public async Task<List<WC_Reservoir>> FindAllAsync()
        {
            return await _context.WC_Reservoir
                .Include(w => w.Manufacturer)
                .ToListAsync();
        }

        public async Task<WC_Reservoir?> FindByIdAsync(int id)
        {
            return await _context.WC_Reservoir
                .Include(w => w.Manufacturer)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        // Find best Custom WaterCooler Reservoir for the build
        public async Task<WC_Reservoir?> FindBestWCReservoir(Build build, double budgetValue)
        {
            var bestWC_Reservoir = await FindAllAsync();
            bestWC_Reservoir = bestWC_Reservoir
                .Where(c => c.Price <= budgetValue)
                .Where(c => c.LevelUnlock < build.Parameter.CurrentLevel)
                .OrderByDescending(c => c.Lighting.HasValue)
                .ThenBy(c => c.Lighting)
                .ThenByDescending(c => c.Price)
                .ToList();

            //The only other check is made at case

            // Check for Manufacturer preference
            bestWC_Reservoir = BuildFilters.IfAnyFilter(bestWC_Reservoir, c => c.Manufacturer == build.Parameter.PreferredManufacturer).ToList();

            return bestWC_Reservoir.FirstOrDefault();
        }

        public bool WC_ReservoirExists(int id)
        {
            return _context.WC_Reservoir.Any(e => e.Id == id);
        }        
    }
}
