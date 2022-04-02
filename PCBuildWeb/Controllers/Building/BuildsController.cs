using Microsoft.AspNetCore.Mvc;
using PCBuildWeb.Data;
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

        public IActionResult Index()
        {
            _buildService.BuildNewPC();
            return View();
        }
    }
}
