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
using PCBuildWeb.Services.Entities.Parts;

namespace PCBuildWeb.Controllers.Parts
{
    public class WC_ReservoirsController : Controller
    {
        private readonly PCBuildWebContext _context;
        private readonly WC_ReservoirService _wcReservoirService;

        public WC_ReservoirsController(PCBuildWebContext context, WC_ReservoirService wcReservoirService)
        {
            _context = context;
            _wcReservoirService = wcReservoirService;
        }

        // GET: WC_Reservoirs
        public async Task<IActionResult> Index()
        {
            var pCBuildWebContext = await _wcReservoirService.FindAllAsync();
            return View(pCBuildWebContext);
        }

        // GET: WC_Reservoirs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wC_Reservoir = await _wcReservoirService.FindByIdAsync(id.Value);
            if (wC_Reservoir == null)
            {
                return NotFound();
            }

            return View(wC_Reservoir);
        }

        // GET: WC_Reservoirs/Create
        public IActionResult Create()
        {
            ViewData["ManufacturerId"] = new SelectList(_context.Manufacturer, "Id", "Name");
            return View();
        }

        // POST: WC_Reservoirs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Height,Id,Name,PartType,ManufacturerId,Price,SellPrice,LevelUnlock,LevelPercent,Lighting")] WC_Reservoir wC_Reservoir)
        {
            if (ModelState.IsValid)
            {
                _context.Add(wC_Reservoir);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ManufacturerId"] = new SelectList(_context.Manufacturer, "Id", "Name", wC_Reservoir.ManufacturerId);
            return View(wC_Reservoir);
        }

        // GET: WC_Reservoirs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wC_Reservoir = await _context.WC_Reservoir.FindAsync(id);
            if (wC_Reservoir == null)
            {
                return NotFound();
            }
            ViewData["ManufacturerId"] = new SelectList(_context.Manufacturer, "Id", "Name", wC_Reservoir.ManufacturerId);
            return View(wC_Reservoir);
        }

        // POST: WC_Reservoirs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Height,Id,Name,PartType,ManufacturerId,Price,SellPrice,LevelUnlock,LevelPercent,Lighting")] WC_Reservoir wC_Reservoir)
        {
            if (id != wC_Reservoir.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(wC_Reservoir);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_wcReservoirService.WC_ReservoirExists(wC_Reservoir.Id))
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
            ViewData["ManufacturerId"] = new SelectList(_context.Manufacturer, "Id", "Name", wC_Reservoir.ManufacturerId);
            return View(wC_Reservoir);
        }

        // GET: WC_Reservoirs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wC_Reservoir = await _context.WC_Reservoir
                .Include(w => w.Manufacturer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (wC_Reservoir == null)
            {
                return NotFound();
            }

            return View(wC_Reservoir);
        }

        // POST: WC_Reservoirs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var wC_Reservoir = await _context.WC_Reservoir.FindAsync(id);
            _context.WC_Reservoir.Remove(wC_Reservoir);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }        
    }
}
