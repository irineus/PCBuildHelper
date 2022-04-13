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
                new SelectListItem { Text = "--Select Channel Qty--", Value = "" },
                new SelectListItem { Text = "Single Channel", Value = "1" },
                new SelectListItem { Text = "Dual Channel", Value = "2" },
                new SelectListItem { Text = "Quad Channel", Value = "4" }
            };
            ViewData["MemoryChannels"] = new SelectList(memoryChannels, "Value", "Text");
            //var buildTypeList = Enum.GetValues(typeof(BuildType))
            //    .Cast<BuildType>()
            //    .Select(x => new SelectListItem { Text = x.ToString(), Value = ((int)x).ToString() });
            ViewData["BuildType"] = null; // buildTypeList;
            Build build = new Build()
            {
                Parameter = new Parameter()
            };
            return View(build);
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
                var memoryChannels = new[]
                {
                    new SelectListItem { Text = "Single Channel", Value = "1" },
                    new SelectListItem { Text = "Dual Channel", Value = "2" },
                    new SelectListItem { Text = "Quad Channel", Value = "4" }
                };
                ViewData["MemoryChannels"] = new SelectList(memoryChannels, "Value", "Text");
                //var buildTypeList = Enum.GetValues(typeof(BuildType))
                //    .Cast<BuildType>()
                //    .Select(x => new SelectListItem { Text = x.ToString(), Value = ((int)x).ToString() });
                ViewData["BuildType"] = null; // buildTypeList;
                RedirectToAction(nameof(Build));
            }
            if (build.Parameter is null)
            {
                return NotFound();
            }
            if (build.Components is null)
            {
                // Make a new build
                Build buildResult = await _buildService.BuildNewPC(build.Parameter);
                if (buildResult.Components.Any())
                {
                    return View(buildResult);
                }
            }
            else
            {
                // Update the build
                Build buildResult = await _buildService.ReBuildPC(build);
                if (buildResult.Components.Any())
                {
                    return View(buildResult);
                }
            }
            return RedirectToAction(nameof(Index));
        }

        //// POST: Builds/BuildRedo
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> BuildRedo(Build build)
        //{
        //    if (build.Parameter is null)
        //    {
        //        return NotFound();
        //    }
        //    if (!ModelState.IsValid)
        //    {
        //        RedirectToAction(nameof(Build), build);
        //    }

        //    return RedirectToAction(nameof(Index));
        //}

    }
}
