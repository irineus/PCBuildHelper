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
    public class BuildTypeStructuresController : Controller
    {
        private readonly PCBuildWebContext _context;
        private readonly BuildTypeStructureService _buildTypeStructureService;

        public BuildTypeStructuresController(PCBuildWebContext context, BuildTypeStructureService buildTypeStructureService)
        {
            _context = context;
            _buildTypeStructureService = buildTypeStructureService;
        }

        // GET: BuildTypeStructures
        public async Task<IActionResult> Index()
        {
            var pCBuildWebContext = await _buildTypeStructureService.FindAllAsync();
            return View(pCBuildWebContext);
        }

        // GET: BuildTypeStructures/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var buildTypeStructure = await _buildTypeStructureService.FindByIdAsync(id.Value);
            if (buildTypeStructure == null)
            {
                return NotFound();
            }

            return View(buildTypeStructure);
        }

        // GET: BuildTypeStructures/Create
        public IActionResult Create()
        {
            ViewData["BuildTypeId"] = new SelectList(_context.BuildTypeStructure, "Id", "Name");
            return View();
        }

        // POST: BuildTypeStructures/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BuildTypeId,PartType,Priority,BudgetPercent,Id,Name")] BuildTypeStructure buildTypeStructure)
        {
            if (ModelState.IsValid)
            {
                _context.Add(buildTypeStructure);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BuildTypeId"] = new SelectList(_context.BuildTypeStructure, "Id", "Name", buildTypeStructure.BuildTypeId);
            return View(buildTypeStructure);
        }

        // GET: BuildTypeStructures/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var buildTypeStructure = await _buildTypeStructureService.FindByIdAsync(id.Value);
            if (buildTypeStructure == null)
            {
                return NotFound();
            }
            ViewData["BuildTypeId"] = new SelectList(_context.BuildTypeStructure, "Id", "Name", buildTypeStructure.BuildTypeId);
            return View(buildTypeStructure);
        }

        // POST: BuildTypeStructures/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BuildTypeId,PartType,Priority,BudgetPercent,Id,Name")] BuildTypeStructure buildTypeStructure)
        {
            if (id != buildTypeStructure.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(buildTypeStructure);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BuildTypeStructureExists(buildTypeStructure.Id))
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
            ViewData["BuildTypeId"] = new SelectList(_context.BuildTypeStructure, "Id", "Name", buildTypeStructure.BuildTypeId);
            return View(buildTypeStructure);
        }

        // GET: BuildTypeStructures/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var buildTypeStructure = await _buildTypeStructureService.FindByIdAsync(id.Value);
            if (buildTypeStructure == null)
            {
                return NotFound();
            }

            return View(buildTypeStructure);
        }

        // POST: BuildTypeStructures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var buildTypeStructure = await _context.BuildType.FindAsync(id);
            _context.BuildType.Remove(buildTypeStructure);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BuildTypeStructureExists(int id)
        {
            return _context.BuildType.Any(e => e.Id == id);
        }
    }
}
