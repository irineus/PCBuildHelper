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
    public class WC_RadiatorService : IBuildPartService<WC_Radiator>
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
        public async Task<WC_Radiator?> FindBestWCRadiator(Build build, double budgetValue, 
            CaseService _caseService)
        {
            List<WC_Radiator> bestWC_Radiator = await FindAllAsync();
            bestWC_Radiator = bestWC_Radiator
                .Where(c => c.Price <= budgetValue)
                .Where(c => c.LevelUnlock < build.Parameter.CurrentLevel)
                .OrderByDescending(c => c.AirFlow)
                .ThenByDescending(c => c.Price)
                .ToList();

            // Check Case specs against WC Radiator
            Case? prerequisiteCase = await BuildFilters.FindPrerequisitePartAsync(build.Components, PartType.WC_Radiator, PartType.Case, _caseService);
            if (prerequisiteCase is not null)
            {
                bestWC_Radiator = BuildFilters.SimpleFilter(bestWC_Radiator, c => ((c.RadiatorSlots <= prerequisiteCase.Number120mmSlots) && (c.RadiatorSize == 120)) ||
                                                                                   (c.RadiatorSlots <= prerequisiteCase.Number140mmSlots) && ((c.RadiatorSize == 120) || (c.RadiatorSize == 140))).ToList();
            }

            // Check for Manufacturer preference
            bestWC_Radiator = BuildFilters.IfAnyFilter(bestWC_Radiator, c => c.Manufacturer == build.Parameter.PreferredManufacturer).ToList();

            return bestWC_Radiator.FirstOrDefault();
        }

        public bool WC_RadiatorExists(int id)
        {
            return _context.WC_Radiator.Any(e => e.Id == id);
        }
    }
}
