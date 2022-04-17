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
    public class GPUService : IBuildPartService<GPU>
    {
        private readonly PCBuildWebContext _context;

        public GPUService(PCBuildWebContext context)
        {
            _context = context;
        }

        public async Task<List<GPU>> FindAllAsync()
        {
            return await _context.GPU.Include(g => g.GPUChipset)
                .Include(g => g.Manufacturer)
                .Include(g => g.MultiGPU)
                .Include(g => g.PowerConnectors)
                .ToListAsync();
        }

        public async Task<GPU?> FindByIdAsync(int id)
        {
            return await _context.GPU
                .Include(g => g.GPUChipset)
                .Include(g => g.Manufacturer)
                .Include(g => g.MultiGPU)
                .Include(g => g.PowerConnectors)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        //Find best GPU for the build parameters
        public async Task<GPU?> FindBestGPU(Build build, double budgetValue, CaseService _caseService, MotherboardService _motherboardService)
        {
            List<GPU> bestGPU = await FindAllAsync();
            bestGPU = bestGPU
                .Where(c => c.Price <= budgetValue)
                .Where(c => c.LevelUnlock < build.Parameter.CurrentLevel)
                .OrderByDescending(c => c.RankingScore)
                .ThenByDescending(c => c.Price)
                .ToList();

            // Check for Custom WC requisite
            bestGPU = build.Parameter.MustHaveCustomWC ? bestGPU.Where(c => c.IsWaterCooled).ToList() : bestGPU.Where(c => !c.IsWaterCooled).ToList();

            // Check for Clock Target
            bestGPU = build.Parameter.TargetGPUClock is null ? bestGPU : bestGPU.Where(c => c.OverclockedCoreFrequency >= build.Parameter.TargetGPUClock).ToList();

            // Filter GPU for Case length support
            Case? prerequisiteCase = await BuildFilters.FindPrerequisitePartAsync(build.Components, PartType.GPU, PartType.Case, _caseService);
            bestGPU = prerequisiteCase is null ? bestGPU : BuildFilters.SimpleFilter(bestGPU, g => g.Length < prerequisiteCase.MaxGPULength).ToList();

            // Check Dual GPU Requisites
            if (build.Parameter.MustHaveDualGPU)
            {
                // Filter only GPUs with MultiGPU Support
                bestGPU = BuildFilters.SimpleFilter(bestGPU, g => g.MultiGPU is not null).ToList();
                // Filter GPU for those that matches Motherboard MultiGPU support
                Motherboard? prerequisiteMotherboard = await BuildFilters.FindPrerequisitePartAsync(build.Components, PartType.GPU, PartType.Motherboard, _motherboardService);
                bestGPU = prerequisiteMotherboard is null ? bestGPU : BuildFilters.SimpleFilter(bestGPU, g => prerequisiteMotherboard.MultiGPUs.Contains(g.MultiGPU!)).ToList();
            }
            
            // Check for Manufacturer preference
            bestGPU = BuildFilters.IfAnyFilter(bestGPU, c => c.Manufacturer == build.Parameter.PreferredManufacturer).ToList();

            return bestGPU.FirstOrDefault();
        }

        public bool GPUExists(int id)
        {
            return _context.GPU.Any(e => e.Id == id);
        }
    }
}
