#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PCBuildWeb.Data;
using PCBuildWeb.Models.Entities.Parts;

namespace PCBuildWeb.Controllers.Parts
{
    public class WC_RadiatorsController : Controller
    {
        private readonly PCBuildWebContext _context;

        public WC_RadiatorsController(PCBuildWebContext context)
        {
            _context = context;
        }

        // GET: WC_Radiators
        public async Task<IActionResult> Index()
        {
            var pCBuildWebContext = _context.WC_Radiator.Include(w => w.Manufacturer);
            return View(await pCBuildWebContext.ToListAsync());
        }

        // GET: WC_Radiators/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wC_Radiator = await _context.WC_Radiator
                .Include(w => w.Manufacturer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (wC_Radiator == null)
            {
                return NotFound();
            }

            return View(wC_Radiator);
        }

        // GET: WC_Radiators/Create
        public IActionResult Create()
        {
            ViewData["ManufacturerId"] = new SelectList(_context.Manufacturer, "Id", "Name");
            return View();
        }

        // POST: WC_Radiators/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AirFlow,RadiatorSize,RadiatorSlots,RadiatorThickness,AirPresure,Id,Name,PartType,ManufacturerId,Price,SellPrice,LevelUnlock,LevelPercent,Lighting")] WC_Radiator wC_Radiator)
        {
            if (ModelState.IsValid)
            {
                _context.Add(wC_Radiator);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ManufacturerId"] = new SelectList(_context.Manufacturer, "Id", "Name", wC_Radiator.ManufacturerId);
            return View(wC_Radiator);
        }

        // GET: WC_Radiators/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wC_Radiator = await _context.WC_Radiator.FindAsync(id);
            if (wC_Radiator == null)
            {
                return NotFound();
            }
            ViewData["ManufacturerId"] = new SelectList(_context.Manufacturer, "Id", "Name", wC_Radiator.ManufacturerId);
            return View(wC_Radiator);
        }

        // POST: WC_Radiators/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AirFlow,RadiatorSize,RadiatorSlots,RadiatorThickness,AirPresure,Id,Name,PartType,ManufacturerId,Price,SellPrice,LevelUnlock,LevelPercent,Lighting")] WC_Radiator wC_Radiator)
        {
            if (id != wC_Radiator.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(wC_Radiator);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WC_RadiatorExists(wC_Radiator.Id))
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
            ViewData["ManufacturerId"] = new SelectList(_context.Manufacturer, "Id", "Name", wC_Radiator.ManufacturerId);
            return View(wC_Radiator);
        }

        // GET: WC_Radiators/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wC_Radiator = await _context.WC_Radiator
                .Include(w => w.Manufacturer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (wC_Radiator == null)
            {
                return NotFound();
            }

            return View(wC_Radiator);
        }

        // POST: WC_Radiators/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var wC_Radiator = await _context.WC_Radiator.FindAsync(id);
            _context.WC_Radiator.Remove(wC_Radiator);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WC_RadiatorExists(int id)
        {
            return _context.WC_Radiator.Any(e => e.Id == id);
        }
    }
}
