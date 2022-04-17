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
    public class MemoryService : IBuildPartService<Memory>
    {
        private readonly PCBuildWebContext _context;

        public MemoryService(PCBuildWebContext context)
        {
            _context = context;
        }

        public async Task<List<Memory>> FindAllAsync()
        {
            return await _context.Memory
                .Include(m => m.Manufacturer)
                .ToListAsync();
        }

        public async Task<Memory?> FindByIdAsync(int id)
        {
            return await _context.Memory
                .Include(m => m.Manufacturer)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        //Find best Memory for the build parameters
        public async Task<Memory?> FindBestMemory(Build build, double budgetValue, 
            MotherboardService _motherboardService)
        {
            // Check the size of each memory chip, considering the number of channels desired
            int? memorySize = build.Parameter.TargetMemorySize / build.Parameter.MemoryChannels;
            if (memorySize is null)
            {
                memorySize = 0;
            }

            List<Memory> bestMemory = await FindAllAsync();
            bestMemory = bestMemory
                .Where(c => c.Price <= budgetValue)
                .Where(c => c.LevelUnlock < build.Parameter.CurrentLevel)
                .Where(c => c.Size >= memorySize)
                .OrderByDescending(c => c.Lighting.HasValue)
                .ThenBy(c => c.Lighting)
                .ThenByDescending(c => c.Size)
                .ThenByDescending(c => c.Frequency)
                .ThenByDescending(c => c.Price)
                .ToList();

            // Filter Memory for Motherboard supported frequency
            Motherboard? prerequisiteMobo = await BuildFilters.FindPrerequisitePartAsync(build.Components, PartType.Memory, PartType.Motherboard, _motherboardService);
            bestMemory = prerequisiteMobo is null ? bestMemory : BuildFilters.SimpleFilter(bestMemory, m => (prerequisiteMobo.MinRamSpeed <= m.Frequency) && (m.Frequency <= prerequisiteMobo.MaxRamSpeed)).ToList();

            // Check for Manufacturer preference
            bestMemory = BuildFilters.IfAnyFilter(bestMemory, c => c.Manufacturer == build.Parameter.PreferredManufacturer).ToList();

            return bestMemory.FirstOrDefault(); ;
        }

        public bool MemoryExists(int id)
        {
            return _context.Memory.Any(e => e.Id == id);
        }
    }
}
