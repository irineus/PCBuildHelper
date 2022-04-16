using Microsoft.EntityFrameworkCore;
using PCBuildWeb.Data;
using PCBuildWeb.Models.Building;
using PCBuildWeb.Models.Entities.Bases;
using PCBuildWeb.Models.Entities.Parts;
using PCBuildWeb.Models.Enums;
using PCBuildWeb.Services.Interfaces;
using PCBuildWeb.Utils.Filters;

namespace PCBuildWeb.Services.Entities.Parts
{
    public class StorageService : IBuildPartService<Storage>
    {
        private readonly PCBuildWebContext _context;

        public StorageService(PCBuildWebContext context)
        {
            _context = context;
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
        public async Task<Storage?> FindBestStorage(Build build, double budgetValue, MotherboardService _motherboardService)
        {
            List<Storage> bestStorage = await FindAllAsync();
            bestStorage = bestStorage
                .Where(c => c.Price <= budgetValue)
                .Where(c => c.LevelUnlock < build.Parameter.CurrentLevel)
                .OrderByDescending(c => c.Speed)
                .ThenByDescending(c => c.Size)
                .ThenByDescending(c => c.Price)
                .ToList();

            Motherboard? prerequisiteMobo = await BuildFilters.FindPrerequisitePartAsync(build.Components, PartType.Storage, PartType.Motherboard, _motherboardService);
            if (prerequisiteMobo is not null)
            {
                Storage? currentSelectedStorage = bestStorage.FirstOrDefault();
                if (currentSelectedStorage != null)
                {
                    //If the best storage at this point is a M.2, should match mobo support or else downgrade
                    if (currentSelectedStorage.Type == StorageType.M_2)
                    {
                        // No support for M.2 from Motherboard => downgrade type
                        if (prerequisiteMobo.M2Slots == 0)
                        {
                            bestStorage = BuildFilters.SimpleFilter(bestStorage, s => s.Type != StorageType.M_2).ToList();
                        }  
                        else
                        {
                            // Mobo supports M.2. Check for heatsink support
                            if (prerequisiteMobo.M2SlotsSupportingHeatsinks == 0)
                            {
                                // Remove Heatsinked M.2 from list
                                bestStorage = BuildFilters.SimpleFilter(bestStorage, s => !s.IncludesHeatsink).ToList();
                            }
                        }                            
                    } 
                }
            }
            

            // Check for Manufacturer preference
            bestStorage = BuildFilters.IfAnyFilter(bestStorage, c => c.Manufacturer == build.Parameter.PreferredManufacturer).ToList();

            return bestStorage.FirstOrDefault();
        }

        public bool StorageExists(int id)
        {
            return _context.Storage.Any(e => e.Id == id);
        }
    }
}
