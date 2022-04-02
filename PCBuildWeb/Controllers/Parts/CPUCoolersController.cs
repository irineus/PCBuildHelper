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
    public class CPUCoolersController : Controller
    {
        private readonly PCBuildWebContext _context;
        private readonly CPUCoolerService _cpuCoolerService;

        public CPUCoolersController(PCBuildWebContext context)
        {
            _context = context;
        }

        // GET: CPUCoolers
        public async Task<IActionResult> Index()
        {
            var pCBuildWebContext = await _cpuCoolerService.FindAllAsync();
            return View(pCBuildWebContext);
        }

        // GET: CPUCoolers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cPUCooler = await _cpuCoolerService.FindByIdAsync(id.Value);
            if (cPUCooler == null)
            {
                return NotFound();
            }

            return View(cPUCooler);
        }

        // GET: CPUCoolers/Create
        public IActionResult Create()
        {
            ViewData["ManufacturerId"] = new SelectList(_context.Manufacturer, "Id", "Name");
            return View();
        }

        // POST: CPUCoolers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("WaterCooler,Passive,AirFlow,Height,RadiatorSize,RadiatorSlots,RadiatorThickness,AirPressure,Id,Name,PartType,ManufacturerId,Price,SellPrice,LevelUnlock,LevelPercent,Lighting")] CPUCooler cPUCooler)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cPUCooler);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ManufacturerId"] = new SelectList(_context.Manufacturer, "Id", "Name", cPUCooler.ManufacturerId);
            return View(cPUCooler);
        }

        // GET: CPUCoolers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cPUCooler = await _context.CPUCooler.FindAsync(id);
            if (cPUCooler == null)
            {
                return NotFound();
            }
            ViewData["ManufacturerId"] = new SelectList(_context.Manufacturer, "Id", "Name", cPUCooler.ManufacturerId);
            return View(cPUCooler);
        }

        // POST: CPUCoolers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("WaterCooler,Passive,AirFlow,Height,RadiatorSize,RadiatorSlots,RadiatorThickness,AirPressure,Id,Name,PartType,ManufacturerId,Price,SellPrice,LevelUnlock,LevelPercent,Lighting")] CPUCooler cPUCooler)
        {
            if (id != cPUCooler.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cPUCooler);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_cpuCoolerService.CPUCoolerExists(cPUCooler.Id))
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
            ViewData["ManufacturerId"] = new SelectList(_context.Manufacturer, "Id", "Name", cPUCooler.ManufacturerId);
            return View(cPUCooler);
        }

        // GET: CPUCoolers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cPUCooler = await _context.CPUCooler
                .Include(c => c.Manufacturer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cPUCooler == null)
            {
                return NotFound();
            }

            return View(cPUCooler);
        }

        // POST: CPUCoolers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cPUCooler = await _context.CPUCooler.FindAsync(id);
            _context.CPUCooler.Remove(cPUCooler);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


    }
}
