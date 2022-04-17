using Microsoft.EntityFrameworkCore;
using PCBuildWeb.Data;
using PCBuildWeb.Models.Building;
using PCBuildWeb.Models.Entities.Bases;
using PCBuildWeb.Models.Entities.Parts;
using PCBuildWeb.Models.Entities.Properties;
using PCBuildWeb.Models.Enums;

namespace PCBuildWeb.Services.Entities.Properties
{
    public class BuildTypeStructureService
    {
        private readonly PCBuildWebContext _context;

        public BuildTypeStructureService(PCBuildWebContext context)
        {
            _context = context;
        }

        public async Task<List<BuildTypeStructure>> FindAllAsync()
        {
            return await _context.BuildTypeStructure.Include(b => b.BuildType).ToListAsync();
        }

        public async Task<BuildTypeStructure?> FindByIdAsync(int id)
        {
            return await _context.BuildTypeStructure
                .Include(b => b.BuildType)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<List<BuildTypeStructure>> FindBuildTypeComponentsAsync(BuildType buildType)
        {
            return await _context.BuildTypeStructure
                .Include(b => b.BuildType)
                .Where(b => b.BuildType == buildType)
                .ToListAsync();
        }

        public async Task<BuildTypeStructure?> FindBuildTypeStructureComponentAsync(BuildType buildType, PartType partType)
        {
            if (buildType == null)
            {
                throw new ArgumentNullException(nameof(buildType));
            }

            return await _context.BuildTypeStructure
                .Include(b => b.BuildType)
                .Where(b => b.BuildType == buildType)
                .FirstOrDefaultAsync(b => b.PartType == partType);
        }

        public bool BuildTypeStructureExists(int id)
        {
            return _context.BuildTypeStructure.Any(e => e.Id == id);
        }
    }
}
