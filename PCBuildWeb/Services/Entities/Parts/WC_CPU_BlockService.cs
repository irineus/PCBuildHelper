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
    public class WC_CPU_BlockService : IBuildPartService<WC_CPU_Block>
    {
        private readonly PCBuildWebContext _context;

        public WC_CPU_BlockService(PCBuildWebContext context)
        {
            _context = context;
        }

        public async Task<List<WC_CPU_Block>> FindAllAsync()
        {
            return await _context.WC_CPU_Block
                .Include(w => w.Manufacturer)
                .Include(w => w.CPUSockets)
                .ToListAsync();
        }

        public async Task<WC_CPU_Block?> FindByIdAsync(int id)
        {
            return await _context.WC_CPU_Block
                .Include(w => w.Manufacturer)
                .Include(w => w.CPUSockets)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        // Find best Custom WaterCooler CPU Block for the build
        public async Task<WC_CPU_Block?> FindBestWCCPUBlock(Build build, double budgetValue, 
            CPUService _cpuService)
        {
            List<WC_CPU_Block> bestWC_CPU_Block = await FindAllAsync();
            bestWC_CPU_Block = bestWC_CPU_Block
                .Where(c => c.Price <= budgetValue)
                .Where(c => c.LevelUnlock < build.Parameter.CurrentLevel)
                .OrderByDescending(c => c.Price)
                .ToList();

            // Check CPU Socket Type
            CPU? prerequisiteCPU = await BuildFilters.FindPrerequisitePartAsync(build.Components, PartType.WC_CPU_Block, PartType.CPU, _cpuService);
            bestWC_CPU_Block = prerequisiteCPU is null ? bestWC_CPU_Block : BuildFilters.SimpleFilter(bestWC_CPU_Block, c => c.CPUSockets.Contains(prerequisiteCPU.CPUSocket)).ToList();

            // Check for Manufacturer preference
            bestWC_CPU_Block = BuildFilters.IfAnyFilter(bestWC_CPU_Block, c => c.Manufacturer == build.Parameter.PreferredManufacturer).ToList();

            return bestWC_CPU_Block.FirstOrDefault();
        }

        public bool WC_CPU_BlockExists(int id)
        {
            return _context.WC_CPU_Block.Any(e => e.Id == id);
        }
    }
}
