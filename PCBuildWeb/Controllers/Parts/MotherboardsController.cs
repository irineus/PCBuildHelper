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
    public class MotherboardsController : Controller
    {
        private readonly PCBuildWebContext _context;

        public MotherboardsController(PCBuildWebContext context)
        {
            _context = context;
        }

        // GET: Motherboards
        public async Task<IActionResult> Index()
        {
            var pCBuildWebContext = _context.Motherboard.Include(m => m.CPUSocket).Include(m => m.Manufacturer).Include(m => m.MoboChipset).Include(m => m.Size);
            return View(await pCBuildWebContext.ToListAsync());
        }

        // GET: Motherboards/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var motherboard = await _context.Motherboard
                .Include(m => m.CPUSocket)
                .Include(m => m.Manufacturer)
                .Include(m => m.MoboChipset)
                .Include(m => m.Size)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (motherboard == null)
            {
                return NotFound();
            }

            return View(motherboard);
        }

        // GET: Motherboards/Create
        public IActionResult Create()
        {
            ViewData["CPUSocketId"] = new SelectList(_context.CPUSocket, "Id", "Name");
            ViewData["ManufacturerId"] = new SelectList(_context.Manufacturer, "Id", "Name");
            ViewData["MoboChipsetId"] = new SelectList(_context.MoboChipset, "Id", "Name");
            ViewData["MoboSizeId"] = new SelectList(_context.MoboSize, "Id", "Name");
            return View();
        }

        // POST: Motherboards/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MoboChipsetId,CPUSocketId,MoboSizeId,MaxRamSpeed,DualGPUMaxSlotSize,Overclockable,M2Slots,M2SlotsSupportingHeatsinks,RamSlots,SATASlots,IncludesCPUBlock,DefaultRamSpeed,Id,Name,PartType,ManufacturerId,Price,SellPrice,LevelUnlock,LevelPercent,Lighting")] Motherboard motherboard)
        {
            if (ModelState.IsValid)
            {
                _context.Add(motherboard);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CPUSocketId"] = new SelectList(_context.CPUSocket, "Id", "Name", motherboard.CPUSocketId);
            ViewData["ManufacturerId"] = new SelectList(_context.Manufacturer, "Id", "Name", motherboard.ManufacturerId);
            ViewData["MoboChipsetId"] = new SelectList(_context.MoboChipset, "Id", "Name", motherboard.MoboChipsetId);
            ViewData["MoboSizeId"] = new SelectList(_context.MoboSize, "Id", "Name", motherboard.MoboSizeId);
            return View(motherboard);
        }

        // GET: Motherboards/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var motherboard = await _context.Motherboard.FindAsync(id);
            if (motherboard == null)
            {
                return NotFound();
            }
            ViewData["CPUSocketId"] = new SelectList(_context.CPUSocket, "Id", "Name", motherboard.CPUSocketId);
            ViewData["ManufacturerId"] = new SelectList(_context.Manufacturer, "Id", "Name", motherboard.ManufacturerId);
            ViewData["MoboChipsetId"] = new SelectList(_context.MoboChipset, "Id", "Name", motherboard.MoboChipsetId);
            ViewData["MoboSizeId"] = new SelectList(_context.MoboSize, "Id", "Name", motherboard.MoboSizeId);
            return View(motherboard);
        }

        // POST: Motherboards/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MoboChipsetId,CPUSocketId,MoboSizeId,MaxRamSpeed,DualGPUMaxSlotSize,Overclockable,M2Slots,M2SlotsSupportingHeatsinks,RamSlots,SATASlots,IncludesCPUBlock,DefaultRamSpeed,Id,Name,PartType,ManufacturerId,Price,SellPrice,LevelUnlock,LevelPercent,Lighting")] Motherboard motherboard)
        {
            if (id != motherboard.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(motherboard);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MotherboardExists(motherboard.Id))
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
            ViewData["CPUSocketId"] = new SelectList(_context.CPUSocket, "Id", "Name", motherboard.CPUSocketId);
            ViewData["ManufacturerId"] = new SelectList(_context.Manufacturer, "Id", "Name", motherboard.ManufacturerId);
            ViewData["MoboChipsetId"] = new SelectList(_context.MoboChipset, "Id", "Name", motherboard.MoboChipsetId);
            ViewData["MoboSizeId"] = new SelectList(_context.MoboSize, "Id", "Name", motherboard.MoboSizeId);
            return View(motherboard);
        }

        // GET: Motherboards/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var motherboard = await _context.Motherboard
                .Include(m => m.CPUSocket)
                .Include(m => m.Manufacturer)
                .Include(m => m.MoboChipset)
                .Include(m => m.Size)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (motherboard == null)
            {
                return NotFound();
            }

            return View(motherboard);
        }

        // POST: Motherboards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var motherboard = await _context.Motherboard.FindAsync(id);
            _context.Motherboard.Remove(motherboard);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MotherboardExists(int id)
        {
            return _context.Motherboard.Any(e => e.Id == id);
        }
    }
}
