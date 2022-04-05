using Microsoft.EntityFrameworkCore;
using PCBuildWeb.Data;
using PCBuildWeb.Models.Building;
using PCBuildWeb.Models.Entities.Bases;
using PCBuildWeb.Models.Entities.Parts;
using PCBuildWeb.Models.Enums;

namespace PCBuildWeb.Services.Entities.Parts
{
    public class WC_RadiatorService
    {
        private readonly PCBuildWebContext _context;
        

        public WC_RadiatorService(PCBuildWebContext context)
        {
            _context = context;
        }

        public async Task<List<WC_Radiator>> FindAllAsync()
        {
            return await _context.WC_Radiator
                .Include(w => w.Manufacturer)
                .ToListAsync();
        }

        public async Task<WC_Radiator?> FindByIdAsync(int id)
        {
            return await _context.WC_Radiator
                .Include(w => w.Manufacturer)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        // Find best Custom WaterCooler Radiator for the build
        public async Task<WC_Radiator?> FindBestWCRadiator(Build build, Component component)
        {
            List<WC_Radiator> bestWC_Radiator = await FindAllAsync();
            bestWC_Radiator = bestWC_Radiator
                .Where(c => c.Price <= component.BudgetValue)
                .Where(c => c.LevelUnlock <= build.Parameter.CurrentLevel)
                .Where(c => c.LevelPercent <= build.Parameter.CurrentLevelPercent)
                .OrderByDescending(c => c.Price)
                .ToList();

            // Check for Manufator preference
            if (build.Parameter.PreferredManufacturer != null)
            {
                if (bestWC_Radiator.Where(c => c.Manufacturer == build.Parameter.PreferredManufacturer).Any())
                {
                    bestWC_Radiator = bestWC_Radiator
                        .Where(c => c.Manufacturer == build.Parameter.PreferredManufacturer)
                        .OrderByDescending(c => c.Price)
                        .ToList();
                }
            }

            //The only other check is made at case

            return bestWC_Radiator.FirstOrDefault();
        }

        public bool WC_RadiatorExists(int id)
        {
            return _context.WC_Radiator.Any(e => e.Id == id);
        }
    }
}
