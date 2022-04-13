using Microsoft.EntityFrameworkCore;
using PCBuildWeb.Data;
using PCBuildWeb.Models.Building;
using PCBuildWeb.Models.Entities.Bases;
using PCBuildWeb.Models.Entities.Parts;
using PCBuildWeb.Models.Entities.Properties;
using PCBuildWeb.Models.Enums;
using PCBuildWeb.Utils.Filters;

namespace PCBuildWeb.Services.Entities.Parts
{
    public class CPUCoolerService
    {
        private readonly PCBuildWebContext _context;
        private readonly CPUService _cpuService;

        public CPUCoolerService(PCBuildWebContext context, CPUService cpuService)
        {
            _context = context;
            _cpuService = cpuService;
        }

        public async Task<List<CPUCooler>> FindAllAsync()
        {
            return await _context.CPUCooler
                .Include(c => c.Manufacturer)
                .Include(c => c.CPUSockets)
                .ToListAsync();
        }

        public async Task<CPUCooler?> FindByIdAsync(int id)
        {
            return await _context.CPUCooler
                .Include(c => c.Manufacturer)
                .Include(c => c.CPUSockets)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        //Find best CPUCooler for the build parameters
        public async Task<CPUCooler?> FindBestCPUCooler(Build build, double budgetValue)
        {
            if (build.Parameter is null)
                throw new ArgumentNullException(nameof(build.Parameter));
            if (build.Components is null)
                throw new ArgumentNullException(nameof(build.Components));

            List<CPUCooler> bestCPUCooler = await FindAllAsync();
            bestCPUCooler = bestCPUCooler
                .Where(c => c.Price <= budgetValue)
                .Where(c => c.LevelUnlock < build.Parameter.CurrentLevel)
                .OrderByDescending(c => c.AirFlow)
                .ThenByDescending(c => c.WaterCooler) // After AirFlow, Priority to WaterCooler
                .ThenByDescending(c => c.Lighting.HasValue) // Prefer something with lightning
                .ThenBy(c => c.Lighting) // Prefer RGB
                .ThenByDescending(c => c.Price)
                .ToList();
            
            // If MustHaveAIOCooler is selected, enforce WaterCooler. If not, it's yet possible to have a WC, but it's not mandatory
            bestCPUCooler = build.Parameter.MustHaveAIOCooler ? BuildFilters.SimpleFilter(bestCPUCooler, c => c.WaterCooler).ToList() : bestCPUCooler;

            // Filter Cooler socket by the socket of the selected CPU
            CPU? prerequisitePart = await BuildFilters.FindPrerequisitePartAsync(build.Components, PartType.CPUCooler, PartType.CPU, (CPUService)_cpuService);
            bestCPUCooler = prerequisitePart is null ? bestCPUCooler : BuildFilters.SimpleFilter(bestCPUCooler, c => c.CPUSockets.Contains(prerequisitePart.CPUSocket)).ToList();

            // Check for Manufacturer preference
            bestCPUCooler = BuildFilters.IfAnyFilter(bestCPUCooler, c => c.Manufacturer == build.Parameter.PreferredManufacturer).ToList();

            return bestCPUCooler.FirstOrDefault();
        }
        
        public bool CPUCoolerExists(int id)
        {
            return _context.CPUCooler.Any(e => e.Id == id);
        }
    }
}
