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
                .Where(c => c.LevelUnlock <= build.Parameter.CurrentLevel)
                .Where(c => c.LevelPercent <= build.Parameter.CurrentLevelPercent)
                .OrderByDescending(c => c.Price)
                .ToList();

            // Check for Manufator preference
            if (build.Parameter.PreferredManufacturer != null)
            {
                if (bestCPUCooler.Where(c => c.Manufacturer == build.Parameter.PreferredManufacturer).Any())
                {
                    bestCPUCooler = bestCPUCooler
                        .Where(c => c.Manufacturer == build.Parameter.PreferredManufacturer)
                        .OrderByDescending(c => c.Price)
                        .ToList();
                }
            }

            // Filter AIO or AirCooler
            bestCPUCooler = build.Parameter.MustHaveAIOCooler ?
                bestCPUCooler.Where(c => c.WaterCooler).OrderByDescending(c => c.Price).ToList() :
                bestCPUCooler.Where(c => !c.WaterCooler).OrderByDescending(c => c.AirFlow).ToList();

            // Check if there's any selected build part in the component list
            List<Component>? componentsWithBuildParts = build.Components.Where(c => c.BuildPart is not null).ToList();
            if (componentsWithBuildParts.Any())
            {
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
                            bestCPUCooler = build.Parameter.MustHaveAIOCooler ?
                                bestCPUCooler.Where(c => c.CPUSockets.Contains(selectedCPU.CPUSocket)).OrderByDescending(c => c.Price).ToList() :
                                bestCPUCooler.Where(c => c.CPUSockets.Contains(selectedCPU.CPUSocket)).OrderByDescending(c => c.AirFlow).ToList();
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
