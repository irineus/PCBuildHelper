using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PCBuildWeb.Data;
using PCBuildWeb.Models.Building;
using PCBuildWeb.Services.Building;

namespace PCBuildWeb.Controllers.Building
{
    public class BuildsController : Controller
    {

        private readonly PCBuildWebContext _context;
        private readonly BuildService _buildService;

        public BuildsController(PCBuildWebContext context, BuildService buildService)
        {
            _context = context;
            _buildService = buildService;
        }

        public async Task<IActionResult> Index()
        {
            //await _buildService.BuildNewPC();
            ViewData["ManufacturerId"] = new SelectList(_context.Manufacturer, "Id", "Name");
            return View();
        }
    }
}
