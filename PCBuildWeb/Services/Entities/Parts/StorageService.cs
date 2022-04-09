using Microsoft.EntityFrameworkCore;
using PCBuildWeb.Data;
using PCBuildWeb.Models.Building;
using PCBuildWeb.Models.Entities.Bases;
using PCBuildWeb.Models.Entities.Parts;
using PCBuildWeb.Models.Enums;

namespace PCBuildWeb.Services.Entities.Parts
{
    public class StorageService
    {
        private readonly PCBuildWebContext _context;
        private readonly MotherboardService _motherboardService;

        public StorageService(PCBuildWebContext context, MotherboardService motherboardService)
        {
            _context = context;
            _motherboardService = motherboardService;
        }

        public async Task<List<Storage>> FindAllAsync()
        {
            return await _context.Storage
                .Include(s => s.Manufacturer)
                .ToListAsync();
        }

        public async Task<Storage?> FindByIdAsync(int id)
        {
            return await _context.Storage
                .Include(s => s.Manufacturer)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        //Find best storage for the build parameters
        public async Task<Storage?> FindBestStorage(Build build, double budgetValue)
        {
            List<Storage> bestStorage = await FindAllAsync();
            bestStorage = bestStorage
                .Where(c => c.Price <= budgetValue)
                .Where(c => c.LevelUnlock < build.Parameter.CurrentLevel)
                .OrderByDescending(c => c.Speed)
                .ThenByDescending(c => c.Size)
                .ThenByDescending(c => c.Price)
                .ToList();

            Storage? currentSelectedStorage = bestStorage.FirstOrDefault();
            if (currentSelectedStorage != null)
            {
                //If the best storage is a M.2, should match mobo support or else downgrade
                if (currentSelectedStorage.Type == StorageType.M_2)
                {
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
                                    if (selectedMobo.M2Slots == 0)
                                    {
                                        // No M.2 support => downgrade type
                                        bestStorage = bestStorage
                                            .Where(s => s.Type != StorageType.M_2)
                                            .ToList();
                                    }
                                    else
                                    {
                                        // Mobo supports M.2. Check for heatsink support
                                        if (selectedMobo.M2SlotsSupportingHeatsinks == 0)
                                        {
                                            // Remove Heatsinked M.2 from list
                                            bestStorage = bestStorage
                                                .Where(s => !s.IncludesHeatsink)
                                                .ToList();
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            // Check for Manufacturer preference
            if (build.Parameter.PreferredManufacturer != null)
            {
                if (bestStorage.Where(c => c.Manufacturer == build.Parameter.PreferredManufacturer).Any())
                {
                    bestStorage = bestStorage
                        .Where(c => c.Manufacturer == build.Parameter.PreferredManufacturer)
                        .ToList();
                }
            }

            return bestStorage.FirstOrDefault();
        }

        public bool StorageExists(int id)
        {
            return _context.Storage.Any(e => e.Id == id);
        }
    }
}
