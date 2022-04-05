using Microsoft.EntityFrameworkCore;
using PCBuildWeb.Data;
using PCBuildWeb.Models.Building;
using PCBuildWeb.Models.Entities.Bases;
using PCBuildWeb.Models.Entities.Parts;
using PCBuildWeb.Models.Enums;

namespace PCBuildWeb.Services.Entities.Parts
{
    public class PSUService
    {
        private readonly PCBuildWebContext _context;
        private readonly CPUService _cpuService;
        private readonly GPUService _gpuService;

        public PSUService(PCBuildWebContext context, CPUService cpuService, GPUService gpuService)
        {
            _context = context;
            _cpuService = cpuService;
            _gpuService = gpuService;
        }

        public async Task<List<PSU>> FindAllAsync()
        {
            return await _context.PSU
                .Include(p => p.Manufacturer)
                .Include(p => p.PSUSize)
                .ToListAsync();
        }

        public async Task<PSU?> FindByIdAsync(int id)
        {
            return await _context.PSU
                .Include(p => p.Manufacturer)
                .Include(p => p.PSUSize)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        //Find best PSU for the build parameters
        public async Task<PSU?> FindBestPSU(Build build, Component component)
        {
            List<PSU> bestPSU = await FindAllAsync();
            bestPSU = bestPSU
                .Where(c => c.Price <= component.BudgetValue)
                .Where(c => c.LevelUnlock <= build.Parameter.CurrentLevel)
                .Where(c => c.LevelPercent <= build.Parameter.CurrentLevelPercent)
                .OrderByDescending(c => c.Type) //Prioritize Modular
                .ThenByDescending(c => c.Wattage) //Prioritize Power Output
                .ThenByDescending(c => c.Price)
                .ToList();

            // Check for Manufator preference
            if (build.Parameter.PreferredManufacturer != null)
            {
                if (bestPSU.Where(c => c.Manufacturer == build.Parameter.PreferredManufacturer).Any())
                {
                    bestPSU = bestPSU
                        .Where(c => c.Manufacturer == build.Parameter.PreferredManufacturer)
                        .ToList();
                }
            }

            int neededPower = 0;
            // Check if there's any selected build part in the component list
            List<Component>? componentsWithBuildParts = build.Components.Where(c => c.BuildPart is not null).ToList();
            if (componentsWithBuildParts.Any())
            {
                // Sum used power from CPU
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
                            neededPower += selectedCPU.Wattage;
                        }
                    }
                }

                // Add GPU Wattage (Should consider Dual GPU Builds)
                List<Component>? preRequisiteComponents = build.Components.Where(c => c.BuildPart!.PartType == PartType.GPU).ToList();
                if (preRequisiteComponents != null)
                {
                    foreach (Component innerComponent in preRequisiteComponents)
                    {
                        ComputerPart? preRequisiteComputerPart = null;
                        preRequisiteComputerPart = innerComponent.BuildPart;
                        if (preRequisiteComputerPart != null)
                        {
                            GPU? selectedGPU = await _gpuService.FindByIdAsync(preRequisiteComputerPart.Id);
                            if (selectedGPU != null)
                            {
                                neededPower += selectedGPU.Wattage;
                            }
                        }
                    }
                }
            }
            
            // Add 10% power margin
            bestPSU = bestPSU
                .Where(p => p.Wattage >= (neededPower * 1.1))
                .ToList();

            return bestPSU.FirstOrDefault();
        }

        public bool PSUExists(int id)
        {
            return _context.PSU.Any(e => e.Id == id);
        }
    }
}
