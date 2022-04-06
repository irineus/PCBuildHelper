using Microsoft.EntityFrameworkCore;
using PCBuildWeb.Data;
using PCBuildWeb.Models.Building;
using PCBuildWeb.Models.Entities.Bases;
using PCBuildWeb.Models.Entities.Parts;
using PCBuildWeb.Models.Enums;

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
        public async Task<CPUCooler?> FindBestCPUCooler(Build build, Component component)
        {
            List<CPUCooler> bestCPUCooler = await FindAllAsync();
            bestCPUCooler = bestCPUCooler
                .Where(c => c.Price <= component.BudgetValue)
                .Where(c => c.LevelUnlock < build.Parameter.CurrentLevel)
                .OrderByDescending(c => c.AirFlow)
                .ThenByDescending(c => c.WaterCooler) // After AirFlow, Priority to WaterCooler
                .ThenByDescending(c => c.Lighting.HasValue) // Prefer something with lightning
                .ThenBy(c => c.Lighting) // Prefer RGB
                .ThenByDescending(c => c.Price)
                .ToList();

            // Check for Manufator preference
            if (build.Parameter.PreferredManufacturer != null)
            {
                if (bestCPUCooler.Where(c => c.Manufacturer == build.Parameter.PreferredManufacturer).Any())
                {
                    bestCPUCooler = bestCPUCooler
                        .Where(c => c.Manufacturer == build.Parameter.PreferredManufacturer)
                        .ToList();
                }
            }

            // If MustHaveAIOCooler is selected, enforce WaterCooler. If not, it's yet possible to have a WC, but it's not mandatory
            bestCPUCooler = build.Parameter.MustHaveAIOCooler ?
                bestCPUCooler.Where(c => c.WaterCooler).ToList() :
                bestCPUCooler;

            // Check if there's any selected build part in the component list
            List<Component>? componentsWithBuildParts = build.Components.Where(c => c.BuildPart is not null).ToList();
            if (componentsWithBuildParts.Any())
            {
                // Check if there's a CPU in the build
                Component? preRequisiteComponent = build.Components.Where(c => c.BuildPart!.PartType == PartType.CPU).FirstOrDefault();
                if (preRequisiteComponent != null)
                {
                    ComputerPart? preRequisiteComputerPart = null;
                    preRequisiteComputerPart = preRequisiteComponent.BuildPart;
                    if (preRequisiteComputerPart != null)
                    {
                        // Filter Cooler socket by the selected CPU Socket
                        CPU? selectedCPU = await _cpuService.FindByIdAsync(preRequisiteComputerPart.Id);
                        if (selectedCPU != null)
                        {
                            bestCPUCooler = bestCPUCooler.Where(c => c.CPUSockets.Contains(selectedCPU.CPUSocket)).ToList();
                        }
                    }
                }
            }
            return bestCPUCooler.FirstOrDefault();
        }

        public bool CPUCoolerExists(int id)
        {
            return _context.CPUCooler.Any(e => e.Id == id);
        }
    }
}
