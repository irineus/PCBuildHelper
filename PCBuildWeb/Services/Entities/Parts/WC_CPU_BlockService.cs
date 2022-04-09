using Microsoft.EntityFrameworkCore;
using PCBuildWeb.Data;
using PCBuildWeb.Models.Building;
using PCBuildWeb.Models.Entities.Bases;
using PCBuildWeb.Models.Entities.Parts;
using PCBuildWeb.Models.Enums;

namespace PCBuildWeb.Services.Entities.Parts
{
    public class WC_CPU_BlockService
    {
        private readonly PCBuildWebContext _context;
        private readonly CPUService _cpuService;

        public WC_CPU_BlockService(PCBuildWebContext context, CPUService cpuService)
        {
            _context = context;
            _cpuService = cpuService;
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
        public async Task<WC_CPU_Block?> FindBestWCCPUBlock(Build build, double budgetValue)
        {
            List<WC_CPU_Block> bestWC_CPU_Block = await FindAllAsync();
            bestWC_CPU_Block = bestWC_CPU_Block
                .Where(c => c.Price <= budgetValue)
                .Where(c => c.LevelUnlock < build.Parameter.CurrentLevel)
                .OrderByDescending(c => c.Price)
                .ToList();

            // Check if there's any selected build part in the component list
            List<Component>? componentsWithBuildParts = build.Components.Where(c => c.BuildPart is not null).ToList();
            if (componentsWithBuildParts.Any())
            {
                // Check CPU Socket Type
                Component? preRequisiteComponent = build.Components.Where(c => c.BuildPart!.PartType == PartType.CPU).FirstOrDefault();
                if (preRequisiteComponent != null)
                {
                    ComputerPart? preRequisiteComputerPart = null;
                    preRequisiteComputerPart = preRequisiteComponent.BuildPart;
                    if (preRequisiteComputerPart != null)
                    {
                        CPU? selectedCPU = await _cpuService.FindByIdAsync(preRequisiteComputerPart.Id);
                        if (selectedCPU != null)
                        {
                            bestWC_CPU_Block = bestWC_CPU_Block
                                .Where(c => c.CPUSockets.Contains(selectedCPU.CPUSocket))
                                .ToList();
                        }
                    }
                }
            }

            // Check for Manufacturer preference
            if (build.Parameter.PreferredManufacturer != null)
            {
                if (bestWC_CPU_Block.Where(c => c.Manufacturer == build.Parameter.PreferredManufacturer).Any())
                {
                    bestWC_CPU_Block = bestWC_CPU_Block
                        .Where(c => c.Manufacturer == build.Parameter.PreferredManufacturer)
                        .ToList();
                }
            }

            return bestWC_CPU_Block.FirstOrDefault();
        }

        public bool WC_CPU_BlockExists(int id)
        {
            return _context.WC_CPU_Block.Any(e => e.Id == id);
        }
    }
}
