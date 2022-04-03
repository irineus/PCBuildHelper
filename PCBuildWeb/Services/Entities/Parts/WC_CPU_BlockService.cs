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
                .Include(w => w.SupportedCPUSockets)
                .ToListAsync();
        }

        public async Task<WC_CPU_Block?> FindByIdAsync(int id)
        {
            return await _context.WC_CPU_Block
                .Include(w => w.Manufacturer)
                .Include(w => w.SupportedCPUSockets)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        // Find best Custom WaterCooler CPU Block for the build
        public async Task<WC_CPU_Block?> FindBestWCCPUBlock(Build build, Component component)
        {
            List<WC_CPU_Block> bestWC_CPU_Block = await FindAllAsync();
            bestWC_CPU_Block = bestWC_CPU_Block
                .Where(c => c.Price <= component.BudgetValue)
                .Where(c => c.LevelUnlock <= build.CurrentLevel)
                .Where(c => c.LevelPercent <= build.CurrentLevelPercent)
                .OrderByDescending(c => c.Price)
                .ToList();

            // Check for Manufator preference
            if (build.PreferredManufacturer != null)
            {
                if (bestWC_CPU_Block.Where(c => c.Manufacturer == build.PreferredManufacturer).Any())
                {
                    bestWC_CPU_Block = bestWC_CPU_Block
                        .Where(c => c.Manufacturer == build.PreferredManufacturer)
                        .OrderByDescending(c => c.Price)
                        .ToList();
                }
            }

            // Check CPU Socket Type
            Component? preRequisiteComponent = build.Components.Where(c => c.Type == PartType.CPU).FirstOrDefault();
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
                            .Where(c => c.SupportedCPUSockets.Contains(selectedCPU.CPUSocket))
                            .OrderByDescending(c => c.Price)
                            .ToList();
                    }
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
