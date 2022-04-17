using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PCBuildWeb.Data;
using PCBuildWeb.Models.Building;
using PCBuildWeb.Models.Entities.Properties;
using PCBuildWeb.Models.Enums;
using PCBuildWeb.Models.ViewModels;
using PCBuildWeb.Services.Building;
using PCBuildWeb.Services.Entities.Properties;

namespace PCBuildWeb.Controllers.Building
{
    public class BuildsController : Controller
    {

        private readonly PCBuildWebContext _context;
        private readonly BuildService _buildService;
        private readonly ManufacturerService _manufacturerService;
        private readonly BuildTypeService _buildTypeService;

        public BuildsController(PCBuildWebContext context, BuildService buildService, ManufacturerService manufacturerService, BuildTypeService buildTypeService)
        {
            _context = context;
            _buildService = buildService;
            _manufacturerService = manufacturerService;
            _buildTypeService = buildTypeService;
        }

        public IActionResult Index()
        {
            return View();
        }

        // GET: Builds/Build
        public async Task<IActionResult> Build()
        {
            ViewData["ManufacturerId"] = new SelectList(await _manufacturerService.FindAllAsync("--Select a Manufacturer--"), "Id", "Name");
            ViewData["BuildType"] = new SelectList(await _buildTypeService.FindAllAsync("--Select a Build Type--"), "Id", "Name");
            var memoryChannels = new[]
            {
                new SelectListItem { Text = "--Select Channel Qty--", Value = "" },
                new SelectListItem { Text = "Single Channel", Value = "1" },
                new SelectListItem { Text = "Dual Channel", Value = "2" },
                new SelectListItem { Text = "Quad Channel", Value = "4" }
            };
            ViewData["MemoryChannels"] = new SelectList(memoryChannels, "Value", "Text");
            return View();
        }

        // POST: Builds/BuildResult
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BuildResult(Build build)
        {
            if (!ModelState.IsValid)
            {
                ViewData["ManufacturerId"] = new SelectList(await _manufacturerService.FindAllAsync("--Select a Manufacturer--"), "Id", "Name");
                ViewData["BuildType"] = new SelectList(await _buildTypeService.FindAllAsync("--Select a Build Type--"), "Id", "Name");
                var memoryChannels = new[]
                {
                    new SelectListItem { Text = "--Select Channel Qty--", Value = "" },
                    new SelectListItem { Text = "Single Channel", Value = "1" },
                    new SelectListItem { Text = "Dual Channel", Value = "2" },
                    new SelectListItem { Text = "Quad Channel", Value = "4" }
                };
                ViewData["MemoryChannels"] = new SelectList(memoryChannels, "Value", "Text");
                RedirectToAction(nameof(Build));
            }
            if (build is null)
            {
                return NotFound();
            }
            if (build.Parameter is null)
            {
                return NotFound();
            }
            Build buildResult = new Build();
            buildResult = await _buildService.BuildPC(build);
            if (buildResult.Components is not null)
            {
                return View(buildResult);
            }            
            return RedirectToAction(nameof(Index));
        }
    }
}
