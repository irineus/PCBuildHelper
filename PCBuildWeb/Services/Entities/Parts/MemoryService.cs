﻿using Microsoft.EntityFrameworkCore;
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
            double memoryBudget = component.BudgetValue / build.Parameter.MemoryChannels;
            int? memorySize = build.Parameter.TargetMemorySize / build.Parameter.MemoryChannels;
            if (memorySize is null)
            {
                memorySize = 0;
            }
            component.BudgetValue = memoryBudget;

            List<Memory> bestMemory = await FindAllAsync();
            bestMemory = bestMemory
                .Where(c => c.Price <= memoryBudget)
                .Where(c => c.LevelUnlock < build.Parameter.CurrentLevel)
                .Where(c => c.Size >= memorySize)
                .OrderByDescending(c => c.Lighting.HasValue)
                .ThenBy(c => c.Lighting)
                .ThenByDescending(c => c.Size)
                .ThenByDescending(c => c.Frequency)
                .ThenByDescending(c => c.Price)
                .ToList();

            // Check for Manufator preference
            if (build.Parameter.PreferredManufacturer != null)
            {
                if (bestMemory.Where(c => c.Manufacturer == build.Parameter.PreferredManufacturer).Any())
                {
                    bestMemory = bestMemory
                        .Where(c => c.Manufacturer == build.Parameter.PreferredManufacturer)
                        .ToList();
                }
            }

            // Check if there's any selected build part in the component list
            List<Component>? componentsWithBuildParts = build.Components.Where(c => c.BuildPart is not null).ToList();
            if (componentsWithBuildParts.Any())
            {
                Component? preRequisiteComponent = build.Components.Where(c => c.BuildPart!.PartType == PartType.Motherboard).FirstOrDefault();
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
                                .ToList();
                        }
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
