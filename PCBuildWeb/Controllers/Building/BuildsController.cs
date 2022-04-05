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

        public BuildsController(PCBuildWebContext context, BuildService buildService, ManufacturerService manufacturerService)
        {
            _context = context;
            _buildService = buildService;
            _manufacturerService = manufacturerService;
        }

        public IActionResult Index()
        {
            return View();
        }

        // GET: Builds/Build
        public async Task<IActionResult> Build()
        {
            ViewData["ManufacturerId"] = new SelectList(await _manufacturerService.FindAllAsync("--Select a Manufacturer--"), "Id", "Name");
            var memoryChannels = new[]
            {
                new SelectListItem { Text = "Single Channel", Value = "1" },
                new SelectListItem { Text = "Dual Channel", Value = "2" },
                new SelectListItem { Text = "Quad Channel", Value = "4" }
            };
            ViewData["MemoryChannels"] = new SelectList(memoryChannels, "Value", "Text");
            var buildTypeList = Enum.GetValues(typeof(BuildTypeEnum))
                .Cast<BuildTypeEnum>()
                .Select(x => new SelectListItem { Text = x.ToString(), Value = ((int)x).ToString() });
            ViewData["BuildType"] = buildTypeList;
            return View();
        }

        // POST: Builds/Build
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BuildResult(Parameter buildParameter)
        {
            if (!ModelState.IsValid)
            {
                ViewData["ManufacturerId"] = new SelectList(await _manufacturerService.FindAllAsync("--Select a Manufacturer--"), "Id", "Name");
                var memoryChannels = new[]
                {
                new SelectListItem { Text = "Single Channel", Value = "1" },
                new SelectListItem { Text = "Dual Channel", Value = "2" },
                new SelectListItem { Text = "Quad Channel", Value = "4" }
            };
                ViewData["MemoryChannels"] = new SelectList(memoryChannels, "Value", "Text");
                var buildTypeList = Enum.GetValues(typeof(BuildTypeEnum))
                    .Cast<BuildTypeEnum>()
                    .Select(x => new SelectListItem { Text = x.ToString(), Value = ((int)x).ToString() });
                ViewData["BuildType"] = buildTypeList;
                RedirectToAction(nameof(Build));
            }
            if (buildParameter is null)
            {
                return NotFound();
            }
            Build buildResult = await _buildService.BuildNewPC(buildParameter);
            if (buildResult.Components.Any())
            {
                return View(buildResult);
            }
            return RedirectToAction(nameof(Index));
        }

    }
}
