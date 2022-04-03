using Microsoft.EntityFrameworkCore;
using PCBuildWeb.Data;
using PCBuildWeb.Models.Building;
using PCBuildWeb.Models.Entities.Bases;
using PCBuildWeb.Models.Entities.Parts;
using PCBuildWeb.Models.Enums;

namespace PCBuildWeb.Services.Entities.Parts
{
    public class MemoryService
    {
        private readonly PCBuildWebContext _context;
        private readonly MotherboardService _motherboardService;

        public MemoryService(PCBuildWebContext context, MotherboardService motherboardService)
        {
            _context = context;
            _motherboardService = motherboardService;
        }

        public async Task<List<Memory>> FindAllAsync()
        {
            return await _context.Memory
                .Include(m => m.Manufacturer)
                .ToListAsync();
        }

        public async Task<Memory?> FindByIdAsync(int id)
        {
            return await _context.Memory
                .Include(m => m.Manufacturer)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        //Find best Memory for the build parameters
        public async Task<Memory?> FindBestMemory(Build build, Component component)
        {
            //Should consider multi-channel memory for budget and size of each chip
            double memoryBudget = component.BudgetValue / build.MemoryChannels;
            int? memorySize = build.TargetMemorySize / build.MemoryChannels;
            if (memorySize == null)
            {
                memorySize = 0;
            }

            List<Memory> bestMemory = await FindAllAsync();
            bestMemory = bestMemory
                .Where(c => c.Price <= memoryBudget)
                .Where(c => c.LevelUnlock <= build.CurrentLevel)
                .Where(c => c.LevelPercent <= build.CurrentLevelPercent)
                .Where(c => c.Size >= memorySize)
                .OrderByDescending(c => c.Price)
                .ToList();

            // Check for Manufator preference
            if (build.PreferredManufacturer != null)
            {
                if (bestMemory.Where(c => c.Manufacturer == build.PreferredManufacturer).Any())
                {
                    bestMemory = bestMemory
                        .Where(c => c.Manufacturer == build.PreferredManufacturer)
                        .OrderByDescending(c => c.Price)
                        .ToList();
                }
            }

            Component? preRequisiteComponent = build.Components.Where(c => c.Type == PartType.Motherboard).FirstOrDefault();
            if (preRequisiteComponent != null)
            {
                ComputerPart? preRequisiteComputerPart = null;
                preRequisiteComputerPart = preRequisiteComponent.BuildPart;
                if (preRequisiteComputerPart != null)
                {
                    Motherboard? selectedMobo = await _motherboardService.FindByIdAsync(preRequisiteComputerPart.Id);
                    if (selectedMobo != null)
                    {
                        bestMemory = bestMemory
                            .Where(m => m.Frequency <= selectedMobo.MaxRamSpeed)
                            .OrderByDescending(c => c.Price)
                            .ToList();
                    }
                }
            }

            return bestMemory.FirstOrDefault(); ;
        }

        public bool MemoryExists(int id)
        {
            return _context.Memory.Any(e => e.Id == id);
        }
    }
}
