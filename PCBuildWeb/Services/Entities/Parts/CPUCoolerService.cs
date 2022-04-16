using Microsoft.EntityFrameworkCore;
using PCBuildWeb.Data;
using PCBuildWeb.Models.Building;
using PCBuildWeb.Models.Entities.Parts;
using PCBuildWeb.Models.Enums;
using PCBuildWeb.Services.Interfaces;
using PCBuildWeb.Utils.Filters;

namespace PCBuildWeb.Services.Entities.Parts
{
    public class CPUCoolerService : IBuildPartService<CPUCooler>
    {
        private readonly PCBuildWebContext _context;

        public CPUCoolerService(PCBuildWebContext context)
        {
            _context = context;
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
        public async Task<CPUCooler?> FindBestCPUCooler(Build build, double budgetValue, 
            CPUService _cpuService, MotherboardService _motherboardService, CaseService _caseService)
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

            if (build.Parameter.MustHaveAIOCooler)
            {
                // If MustHaveAIOCooler is selected, enforce WaterCooler. If not, it's yet possible to have a WC, but it's not mandatory
                bestCPUCooler = BuildFilters.SimpleFilter(bestCPUCooler, c => c.WaterCooler).ToList();
                // Check for case fan slots to match the sizer o AIO Radiator
                Case? prerequisiteCase = await BuildFilters.FindPrerequisitePartAsync(build.Components, PartType.CPUCooler, PartType.Case, _caseService);
                bestCPUCooler = prerequisiteCase is null ? bestCPUCooler : BuildFilters
                    .SimpleFilter(bestCPUCooler, c => ((c.RadiatorSlots <= prerequisiteCase.Number120mmSlots) && (c.RadiatorSize == 120)) || (c.RadiatorSlots <= prerequisiteCase.Number140mmSlots)).ToList();
            }            

            // Filter Cooler socket by the socket of the selected CPU
            CPU? prerequisiteCPU = await BuildFilters.FindPrerequisitePartAsync(build.Components, PartType.CPUCooler, PartType.CPU, _cpuService);
            bestCPUCooler = prerequisiteCPU is null ? bestCPUCooler : BuildFilters.SimpleFilter(bestCPUCooler, c => c.CPUSockets.Contains(prerequisiteCPU.CPUSocket)).ToList();

            // Filter Cooler socket by the socket of the selected Motherboard CPU Socket
            Motherboard? prerequisiteMobo = await BuildFilters.FindPrerequisitePartAsync(build.Components, PartType.CPUCooler, PartType.Motherboard, _motherboardService);
            bestCPUCooler = prerequisiteMobo is null ? bestCPUCooler : BuildFilters.SimpleFilter(bestCPUCooler, c => c.CPUSockets.Contains(prerequisiteMobo.CPUSocket)).ToList();

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
