using Microsoft.EntityFrameworkCore;
using PCBuildWeb.Data;
using PCBuildWeb.Models.Building;
using PCBuildWeb.Models.Entities.Bases;
using PCBuildWeb.Models.Entities.Parts;
using PCBuildWeb.Models.Enums;

namespace PCBuildWeb.Services.Entities.Parts
{
    public class GPUService
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
        public async Task<GPU?> FindBestGPU(Build build, double budgetValue)
        {
            List<GPU> bestGPU = await FindAllAsync();
            bestGPU = bestGPU
                .Where(c => c.Price <= budgetValue)
                .Where(c => c.LevelUnlock < build.Parameter.CurrentLevel)
                .OrderByDescending(c => c.RankingScore)
                .ThenByDescending(c => c.Price)
                .ToList();

            // Check for Manufator preference
            if (build.Parameter.PreferredManufacturer is not null)
            {
                if (bestGPU.Where(c => c.Manufacturer == build.Parameter.PreferredManufacturer).Any())
                {
                    bestGPU = bestGPU
                        .Where(c => c.Manufacturer == build.Parameter.PreferredManufacturer)
                        .ToList();
                }
            }

            // Check for Custom WC requisite
            bestGPU = build.Parameter.MustHaveCustomWC ? bestGPU.Where(c => c.IsWaterCooled).ToList() : bestGPU.Where(c => !c.IsWaterCooled).ToList();

            // Check for Clock Target
            if (build.Parameter.TargetGPUClock is not null)
            {
                bestGPU = bestGPU
                    .Where(c => c.OverclockedCoreFrequency >= build.Parameter.TargetGPUClock)
                    .ToList();
            }

            // Check Dual GPU Requisites
            if (build.Parameter.MustHaveDualGPU)
            {
                // Check if theres any build part yet
                List<Component>? componentsWithBuildParts = build.Components.Where(c => c.BuildPart is not null).ToList();
                if (componentsWithBuildParts.Any())
                {
                    // Match Motherboard Dual GPU Support
                    Component? preRequisiteComponent = build.Components.Where(c => c.BuildPart!.PartType == PartType.Motherboard).FirstOrDefault();
                    if (preRequisiteComponent != null)
                    {
                        ComputerPart? preRequisiteComputerPart = null;
                        preRequisiteComputerPart = preRequisiteComponent.BuildPart;
                        if (preRequisiteComputerPart is not null)
                        {
                            Motherboard selectedMobo = (Motherboard)preRequisiteComputerPart;
                            bestGPU = bestGPU
                                    .Where(g => selectedMobo.MultiGPUs.Contains(g.MultiGPU))
                                    .ToList();
                        }
                    }
                }
            }
            return bestGPU.FirstOrDefault();
        }

        public bool GPUExists(int id)
        {
            return _context.GPU.Any(e => e.Id == id);
        }
    }
}
