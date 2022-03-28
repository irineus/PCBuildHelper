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
    public class CaseFansController : Controller
    {
        private readonly PCBuildWebContext _context;

        public CaseFansController(PCBuildWebContext context)
        {
            _context = context;
        }

        // GET: CaseFans
        public async Task<IActionResult> Index()
        {
            var pCBuildWebContext = _context.CaseFan.Include(c => c.Manufacturer);
            return View(await pCBuildWebContext.ToListAsync());
        }

        // GET: CaseFans/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var caseFan = await _context.CaseFan
                .Include(c => c.Manufacturer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (caseFan == null)
            {
                return NotFound();
            }

            return View(caseFan);
        }

        // GET: CaseFans/Create
        public IActionResult Create()
        {
            ViewData["ManufacturerId"] = new SelectList(_context.Manufacturer, "Id", "Name");
            return View();
        }

        // POST: CaseFans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AirFlow,Size,AirPressure,Id,Name,PartType,ManufacturerId,Price,SellPrice,LevelUnlock,LevelPercent,Lighting")] CaseFan caseFan)
        {
            if (ModelState.IsValid)
            {
                _context.Add(caseFan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ManufacturerId"] = new SelectList(_context.Manufacturer, "Id", "Name", caseFan.ManufacturerId);
            return View(caseFan);
        }

        // GET: CaseFans/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var caseFan = await _context.CaseFan.FindAsync(id);
            if (caseFan == null)
            {
                return NotFound();
            }
            ViewData["ManufacturerId"] = new SelectList(_context.Manufacturer, "Id", "Name", caseFan.ManufacturerId);
            return View(caseFan);
        }

        // POST: CaseFans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AirFlow,Size,AirPressure,Id,Name,PartType,ManufacturerId,Price,SellPrice,LevelUnlock,LevelPercent,Lighting")] CaseFan caseFan)
        {
            if (id != caseFan.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(caseFan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CaseFanExists(caseFan.Id))
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
            ViewData["ManufacturerId"] = new SelectList(_context.Manufacturer, "Id", "Name", caseFan.ManufacturerId);
            return View(caseFan);
        }

        // GET: CaseFans/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var caseFan = await _context.CaseFan
                .Include(c => c.Manufacturer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (caseFan == null)
            {
                return NotFound();
            }

            return View(caseFan);
        }

        // POST: CaseFans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var caseFan = await _context.CaseFan.FindAsync(id);
            _context.CaseFan.Remove(caseFan);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CaseFanExists(int id)
        {
            return _context.CaseFan.Any(e => e.Id == id);
        }
    }
}
