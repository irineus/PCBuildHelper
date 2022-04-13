#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PCBuildWeb.Data;
using PCBuildWeb.Models.Entities.Properties;
using PCBuildWeb.Services.Entities.Properties;

namespace PCBuildWeb.Controllers.Parts
{
    public class BuildTypesController : Controller
    {
        private readonly PCBuildWebContext _context;
        private readonly BuildTypeService _buildTypeService;

        public BuildTypesController(PCBuildWebContext context, BuildTypeService buildTypeService)
        {
            _context = context;
            _buildTypeService = buildTypeService;
        }

        // GET: BuildTypes
        public async Task<IActionResult> Index()
        {
            return View(await _buildTypeService.FindAllAsync());
        }

        // GET: BuildTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var buildType = await _buildTypeService.FindByIdAsync(id.Value);
            if (buildType == null)
            {
                return NotFound();
            }

            return View(buildType);
        }

        // GET: BuildTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BuildTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PartType,Priority,BudgetPercent,Id,Name")] BuildType buildType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(buildType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(buildType);
        }

        // GET: BuildTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var buildType = await _context.BuildType.FindAsync(id);
            if (buildType == null)
            {
                return NotFound();
            }
            return View(buildType);
        }

        // POST: BuildTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PartType,Priority,BudgetPercent,Id,Name")] BuildType buildType)
        {
            if (id != buildType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(buildType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_buildTypeService.BuildTypeExists(buildType.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(buildType);
        }

        // GET: BuildTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var buildType = await _context.BuildType
                .FirstOrDefaultAsync(m => m.Id == id);
            if (buildType == null)
            {
                return NotFound();
            }

            return View(buildType);
        }

        // POST: BuildTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var buildType = await _context.BuildType.FindAsync(id);
            _context.BuildType.Remove(buildType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


    }
}
