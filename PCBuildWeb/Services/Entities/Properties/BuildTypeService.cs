using Microsoft.EntityFrameworkCore;
using PCBuildWeb.Data;
using PCBuildWeb.Models.Building;
using PCBuildWeb.Models.Entities.Bases;
using PCBuildWeb.Models.Entities.Parts;
using PCBuildWeb.Models.Entities.Properties;
using PCBuildWeb.Models.Enums;

namespace PCBuildWeb.Services.Entities.Properties
{
    public class BuildTypeService
    {
        private readonly PCBuildWebContext _context;

        public BuildTypeService(PCBuildWebContext context)
        {
            _context = context;
        }

        public async Task<List<BuildType>> FindAllAsync()
        {
            return await _context.BuildType.ToListAsync();
        }

        public async Task<BuildType?> FindByIdAsync(int id)
        {
            return await _context.BuildType
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public bool BuildTypeExists(int id)
        {
            return _context.BuildType.Any(e => e.Id == id);
        }
    }
}
