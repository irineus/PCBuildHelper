using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PCBuildWeb.Data;
using PCBuildWeb.Models.Entities.Properties;

namespace PCBuildWeb.Controllers.Properties
{
    public class GPUChipsetsController : Controller
    {
        private readonly PCBuildWebContext _context;

        public GPUChipsetsController(PCBuildWebContext context)
        {
            _context = context;
        }

        // GET: GPUChipsets
        public async Task<IActionResult> Index()
        {
            var pCBuildWebContext = _context.GPUChipset.Include(g => g.ChipsetSeries);
            return View(await pCBuildWebContext.ToListAsync());
        }

        // GET: GPUChipsets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gPUChipset = await _context.GPUChipset
                .Include(g => g.ChipsetSeries)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gPUChipset == null)
            {
                return NotFound();
            }

            return View(gPUChipset);
        }

        // GET: GPUChipsets/Create
        public IActionResult Create()
        {
            ViewData["ChipsetSeriesId"] = new SelectList(_context.GPUChipsetSeries, "Id", "Id");
            return View();
        }

        // POST: GPUChipsets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ChipsetSeriesId,Id,Name")] GPUChipset gPUChipset)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gPUChipset);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ChipsetSeriesId"] = new SelectList(_context.GPUChipsetSeries, "Id", "Id", gPUChipset.ChipsetSeriesId);
            return View(gPUChipset);
        }

        // GET: GPUChipsets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gPUChipset = await _context.GPUChipset.FindAsync(id);
            if (gPUChipset == null)
            {
                return NotFound();
            }
            ViewData["ChipsetSeriesId"] = new SelectList(_context.GPUChipsetSeries, "Id", "Name", gPUChipset.ChipsetSeriesId);
            return View(gPUChipset);
        }

        // POST: GPUChipsets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ChipsetSeriesId,Id,Name")] GPUChipset gPUChipset)
        {
            if (id != gPUChipset.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gPUChipset);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GPUChipsetExists(gPUChipset.Id))
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
            ViewData["ChipsetSeriesId"] = new SelectList(_context.GPUChipsetSeries, "Id", "Id", gPUChipset.ChipsetSeriesId);
            return View(gPUChipset);
        }

        // GET: GPUChipsets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gPUChipset = await _context.GPUChipset
                .Include(g => g.ChipsetSeries)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gPUChipset == null)
            {
                return NotFound();
            }

            return View(gPUChipset);
        }

        // POST: GPUChipsets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var gPUChipset = await _context.GPUChipset.FindAsync(id);
            _context.GPUChipset.Remove(gPUChipset);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GPUChipsetExists(int id)
        {
            return _context.GPUChipset.Any(e => e.Id == id);
        }
    }
}
