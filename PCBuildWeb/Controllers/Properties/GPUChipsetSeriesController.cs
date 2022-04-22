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
    public class GPUChipsetSeriesController : Controller
    {
        private readonly PCBuildWebContext _context;

        public GPUChipsetSeriesController(PCBuildWebContext context)
        {
            _context = context;
        }

        // GET: GPUChipsetSeries
        public async Task<IActionResult> Index()
        {
            return View(await _context.GPUChipsetSeries.ToListAsync());
        }

        // GET: GPUChipsetSeries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gPUChipsetSeries = await _context.GPUChipsetSeries
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gPUChipsetSeries == null)
            {
                return NotFound();
            }

            return View(gPUChipsetSeries);
        }

        // GET: GPUChipsetSeries/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: GPUChipsetSeries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] GPUChipsetSeries gPUChipsetSeries)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gPUChipsetSeries);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(gPUChipsetSeries);
        }

        // GET: GPUChipsetSeries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gPUChipsetSeries = await _context.GPUChipsetSeries.FindAsync(id);
            if (gPUChipsetSeries == null)
            {
                return NotFound();
            }
            return View(gPUChipsetSeries);
        }

        // POST: GPUChipsetSeries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] GPUChipsetSeries gPUChipsetSeries)
        {
            if (id != gPUChipsetSeries.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gPUChipsetSeries);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GPUChipsetSeriesExists(gPUChipsetSeries.Id))
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
            return View(gPUChipsetSeries);
        }

        // GET: GPUChipsetSeries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gPUChipsetSeries = await _context.GPUChipsetSeries
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gPUChipsetSeries == null)
            {
                return NotFound();
            }

            return View(gPUChipsetSeries);
        }

        // POST: GPUChipsetSeries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var gPUChipsetSeries = await _context.GPUChipsetSeries.FindAsync(id);
            if (gPUChipsetSeries is not null)
            {
                _context.GPUChipsetSeries.Remove(gPUChipsetSeries);
                await _context.SaveChangesAsync();
            }            
            return RedirectToAction(nameof(Index));
        }

        private bool GPUChipsetSeriesExists(int id)
        {
            return _context.GPUChipsetSeries.Any(e => e.Id == id);
        }
    }
}
