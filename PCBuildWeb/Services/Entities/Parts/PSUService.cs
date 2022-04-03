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
                .Where(c => c.LevelUnlock <= build.CurrentLevel)
                .Where(c => c.LevelPercent <= build.CurrentLevelPercent)
                .OrderByDescending(c => c.Type) //Prioritize Modular
                .ThenByDescending(c => c.Wattage) //Prioritize Power Output
                .ThenByDescending(c => c.Price)
                .ToList();

            // Check for Manufator preference
            if (build.PreferredManufacturer != null)
            {
                if (bestPSU.Where(c => c.Manufacturer == build.PreferredManufacturer).Any())
                {
                    bestPSU = bestPSU
                        .Where(c => c.Manufacturer == build.PreferredManufacturer)
                        .ToList();
                }
            }
            
            // Sum used power from CPU
            int neededPower = 0;
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
                        neededPower += selectedCPU.Wattage;
                    }
                }
            }

            // Add GPU Wattage (Should consider Dual GPU Builds)
            List<Component>? preRequisiteComponents = build.Components.Where(c => c.Type == PartType.GPU).ToList();
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
