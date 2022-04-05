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
        public async Task<GPU?> FindBestGPU(Build build, Component component)
        {
            List<GPU> bestGPU = await FindAllAsync();
            bestGPU = bestGPU
                .Where(c => c.Price <= component.BudgetValue)
                .Where(c => c.LevelUnlock <= build.Parameter.CurrentLevel)
                .Where(c => c.LevelPercent <= build.Parameter.CurrentLevelPercent)
                .OrderByDescending(c => c.Price)
                .ToList();

            // Check for Manufator preference
            if (build.Parameter.PreferredManufacturer != null)
            {
                if (bestGPU.Where(c => c.Manufacturer == build.Parameter.PreferredManufacturer).Any())
                {
                    bestGPU = bestGPU
                        .Where(c => c.Manufacturer == build.Parameter.PreferredManufacturer)
                        .OrderByDescending(c => c.Price)
                        .ToList();
                }
            }

            // Check if there's any selected build part in the component list
            List<Component>? componentsWithBuildParts = build.Components.Where(c => c.BuildPart is not null).ToList();
            if (componentsWithBuildParts.Any())
            {
                Component? preRequisiteComponent = build.Components.Where(c => c.BuildPart!.PartType == PartType.GPU).FirstOrDefault();
                if (preRequisiteComponent != null)
                {
                    ComputerPart? preRequisiteComputerPart = null;
                    preRequisiteComputerPart = preRequisiteComponent.BuildPart;
                    if (preRequisiteComputerPart != null)
                    {
                        //Dual GPU: select another identical one
                        GPU selectedGPU = (GPU)preRequisiteComputerPart;
                        bestGPU = bestGPU
                            .Where(g => g.Id == selectedGPU.Id)
                            .OrderByDescending(g => g.Price)
                            .ToList();
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
