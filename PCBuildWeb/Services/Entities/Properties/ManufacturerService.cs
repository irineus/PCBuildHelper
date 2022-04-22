using Microsoft.EntityFrameworkCore;
using PCBuildWeb.Data;
using PCBuildWeb.Models.Building;
using PCBuildWeb.Models.Entities.Bases;
using PCBuildWeb.Models.Entities.Parts;
using PCBuildWeb.Models.Entities.Properties;
using PCBuildWeb.Models.Enums;

namespace PCBuildWeb.Services.Entities.Properties
{
    public class ManufacturerService
    {
        private readonly PCBuildWebContext _context;

        public ManufacturerService(PCBuildWebContext context)
        {
            _context = context;
        }

        public async Task<List<Manufacturer>> FindAllAsync()
        {
            return await _context.Manufacturer.ToListAsync();
        }

        public async Task<List<Manufacturer>> FindAllAsync(string? defaultValue)
        {
            var manufaturerList = await _context.Manufacturer.ToListAsync();
            if (defaultValue is not null)
            {
                manufaturerList.Insert(0, new Manufacturer { Id = 0, Name = defaultValue });
            }            
            return manufaturerList;
        }

        public async Task<Manufacturer?> FindByIdAsync(int id)
        {
            return await _context.Manufacturer
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public bool ManufacturerExists(int id)
        {
            return _context.Manufacturer.Any(e => e.Id == id);
        }
    }
}
