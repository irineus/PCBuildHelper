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
    public class PSUsController : Controller
    {
        private readonly PCBuildWebContext _context;
        private readonly PSUService _psuService;

        public PSUsController(PCBuildWebContext context, PSUService psuService)
        {
            _context = context;
            _psuService = psuService;
        }

        // GET: PSUs
        public async Task<IActionResult> Index()
        {
            var pCBuildWebContext = await _psuService.FindAllAsync();
            return View(pCBuildWebContext);
        }

        // GET: PSUs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pSU = await _psuService.FindByIdAsync(id.Value);
            if (pSU == null)
            {
                return NotFound();
            }

            return View(pSU);
        }

        // GET: PSUs/Create
        public IActionResult Create()
        {
            ViewData["ManufacturerId"] = new SelectList(_context.Manufacturer, "Id", "Name");
            ViewData["PSUSizeId"] = new SelectList(_context.PSUSize, "Id", "Name");
            return View();
        }

        // POST: PSUs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Wattage,Length,Type,PSUSizeId,Id,Name,PartType,ManufacturerId,Price,SellPrice,LevelUnlock,LevelPercent,Lighting")] PSU pSU)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pSU);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ManufacturerId"] = new SelectList(_context.Manufacturer, "Id", "Name", pSU.ManufacturerId);
            ViewData["PSUSizeId"] = new SelectList(_context.PSUSize, "Id", "Name", pSU.PSUSizeId);
            return View(pSU);
        }

        // GET: PSUs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pSU = await _context.PSU.FindAsync(id);
            if (pSU == null)
            {
                return NotFound();
            }
            ViewData["ManufacturerId"] = new SelectList(_context.Manufacturer, "Id", "Name", pSU.ManufacturerId);
            ViewData["PSUSizeId"] = new SelectList(_context.PSUSize, "Id", "Name", pSU.PSUSizeId);
            return View(pSU);
        }

        // POST: PSUs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Wattage,Length,Type,PSUSizeId,Id,Name,PartType,ManufacturerId,Price,SellPrice,LevelUnlock,LevelPercent,Lighting")] PSU pSU)
        {
            if (id != pSU.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pSU);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_psuService.PSUExists(pSU.Id))
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
            ViewData["ManufacturerId"] = new SelectList(_context.Manufacturer, "Id", "Name", pSU.ManufacturerId);
            ViewData["PSUSizeId"] = new SelectList(_context.PSUSize, "Id", "Name", pSU.PSUSizeId);
            return View(pSU);
        }

        // GET: PSUs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pSU = await _context.PSU
                .Include(p => p.Manufacturer)
                .Include(p => p.PSUSize)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pSU == null)
            {
                return NotFound();
            }

            return View(pSU);
        }

        // POST: PSUs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pSU = await _context.PSU.FindAsync(id);
            _context.PSU.Remove(pSU);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
