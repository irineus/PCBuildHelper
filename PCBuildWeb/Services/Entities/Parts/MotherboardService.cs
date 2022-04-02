using Microsoft.EntityFrameworkCore;
using PCBuildWeb.Data;
using PCBuildWeb.Models.Building;
using PCBuildWeb.Models.Entities.Bases;
using PCBuildWeb.Models.Entities.Parts;
using PCBuildWeb.Models.Enums;

namespace PCBuildWeb.Services.Entities.Parts
{
    public class MotherboardService
    {
        private readonly PCBuildWebContext _context;
        private readonly CPUService _cpuService;

        public MotherboardService(PCBuildWebContext context, CPUService cpuService)
        {
            _context = context;
            _cpuService = cpuService;
        }

        public async Task<List<Motherboard>> FindAllAsync()
        {
            return await _context.Motherboard
                .Include(m => m.CPUSocket)
                .Include(m => m.Manufacturer)
                .Include(m => m.MoboChipset)
                .Include(m => m.Size)
                .ToListAsync();
        }

        public async Task<Motherboard?> FindByIdAsync(int id)
        {
            return await _context.Motherboard
                .Include(m => m.CPUSocket)
                .Include(m => m.Manufacturer)
                .Include(m => m.MoboChipset)
                .Include(m => m.Size)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        //Find best Motherboard for the build parameters
        public async Task<Motherboard?> FindBestMotherboard(Build build, Component component)
        {
            List<Motherboard> bestMobo = await FindAllAsync();
            bestMobo = bestMobo.Where(c => c.Price <= component.BudgetValue)
                            .Where(c => c.LevelUnlock <= build.CurrentLevel)
                            .Where(c => c.LevelPercent <= build.CurrentLevelPercent)
                            .OrderByDescending(c => c.Price).ToList();

            // Check for Manufator preference
            if (build.PreferredManufacturer != null)
            {
                if (bestMobo.Where(c => c.Manufacturer == build.PreferredManufacturer).Any())
                {
                    bestMobo = bestMobo
                        .Where(c => c.Manufacturer == build.PreferredManufacturer)
                        .OrderByDescending(c => c.Price)
                        .ToList();
                }
            }

            Component? preRequisiteComponent = build.Components.Where(c => c.Type == PartType.CPU).FirstOrDefault();
            if (preRequisiteComponent != null)
            {
                ComputerPart? preRequisiteComputerPart = null;
                preRequisiteComputerPart = preRequisiteComponent.BuildPart;
                if (preRequisiteComputerPart != null)
                {
                    CPU selectedCPU = await _cpuService.FindByIdAsync(preRequisiteComputerPart.Id);
                    bestMobo = bestMobo
                        .Where(m => m.CPUSocket == selectedCPU.CPUSocket)
                        .OrderByDescending(m => m.Price)
                        .ToList();
                }
            }
            if (build.MemoryChannels > 0)
            {
                bestMobo = bestMobo
                    .Where(m => m.RamSlots >= build.MemoryChannels)
                    .OrderByDescending(c => c.Price)
                    .ToList();
            }

            return bestMobo.FirstOrDefault();
        }

        public bool MotherboardExists(int id)
        {
            return _context.Motherboard.Any(e => e.Id == id);
        }
    }
}
