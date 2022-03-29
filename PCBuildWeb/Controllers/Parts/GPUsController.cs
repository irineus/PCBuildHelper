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
    public class GPUsController : Controller
    {
        private readonly PCBuildWebContext _context;

        public GPUsController(PCBuildWebContext context)
        {
            _context = context;
        }

        // GET: GPUs
        public async Task<IActionResult> Index()
        {
            var pCBuildWebContext = _context.GPU.Include(g => g.GPUChipset).Include(g => g.Manufacturer).Include(g => g.MultiGPU);
            return View(await pCBuildWebContext.ToListAsync());
        }

        // GET: GPUs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gPU = await _context.GPU
                .Include(g => g.GPUChipset)
                .Include(g => g.Manufacturer)
                .Include(g => g.MultiGPU)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gPU == null)
            {
                return NotFound();
            }

            return View(gPU);
        }

        // GET: GPUs/Create
        public IActionResult Create()
        {
            ViewData["GPUChipsetId"] = new SelectList(_context.GPUChipset, "Id", "Name");
            ViewData["ManufacturerId"] = new SelectList(_context.Manufacturer, "Id", "Name");
            ViewData["MultiGPUId"] = new SelectList(_context.MultiGPU, "Id", "Name");
            return View();
        }

        // POST: GPUs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ChipsetBrand,GPUChipsetId,IsWaterCooled,RankingScore,VRAM,MinCoreFrequency,BaseCoreFrequency,OverclockedCoreFrequency,MaxCoreFrequency,MinMemFrequency,BaseMemFrequency,OverclockedMemFrequency,MaxMemFrequency,Length,Wattage,MultiGPUId,SlotSize,ScoreToValueRatio,SingleGPUScore,DualGPUScore,DualGPUPerformanceIncrease,OverclockedSingleGPUScore,OverclockedDualGPUScore,Id,Name,PartType,ManufacturerId,Price,SellPrice,LevelUnlock,LevelPercent,Lighting")] GPU gPU)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gPU);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GPUChipsetId"] = new SelectList(_context.GPUChipset, "Id", "Name", gPU.GPUChipsetId);
            ViewData["ManufacturerId"] = new SelectList(_context.Manufacturer, "Id", "Name", gPU.ManufacturerId);
            ViewData["MultiGPUId"] = new SelectList(_context.MultiGPU, "Id", "Name", gPU.MultiGPUId);
            return View(gPU);
        }

        // GET: GPUs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gPU = await _context.GPU.FindAsync(id);
            if (gPU == null)
            {
                return NotFound();
            }
            ViewData["GPUChipsetId"] = new SelectList(_context.GPUChipset, "Id", "Name", gPU.GPUChipsetId);
            ViewData["ManufacturerId"] = new SelectList(_context.Manufacturer, "Id", "Name", gPU.ManufacturerId);
            ViewData["MultiGPUId"] = new SelectList(_context.MultiGPU, "Id", "Name", gPU.MultiGPUId);
            return View(gPU);
        }

        // POST: GPUs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ChipsetBrand,GPUChipsetId,IsWaterCooled,RankingScore,VRAM,MinCoreFrequency,BaseCoreFrequency,OverclockedCoreFrequency,MaxCoreFrequency,MinMemFrequency,BaseMemFrequency,OverclockedMemFrequency,MaxMemFrequency,Length,Wattage,MultiGPUId,SlotSize,ScoreToValueRatio,SingleGPUScore,DualGPUScore,DualGPUPerformanceIncrease,OverclockedSingleGPUScore,OverclockedDualGPUScore,Id,Name,PartType,ManufacturerId,Price,SellPrice,LevelUnlock,LevelPercent,Lighting")] GPU gPU)
        {
            if (id != gPU.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gPU);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GPUExists(gPU.Id))
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
            ViewData["GPUChipsetId"] = new SelectList(_context.GPUChipset, "Id", "Name", gPU.GPUChipsetId);
            ViewData["ManufacturerId"] = new SelectList(_context.Manufacturer, "Id", "Name", gPU.ManufacturerId);
            ViewData["MultiGPUId"] = new SelectList(_context.MultiGPU, "Id", "Name", gPU.MultiGPUId);
            return View(gPU);
        }

        // GET: GPUs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gPU = await _context.GPU
                .Include(g => g.GPUChipset)
                .Include(g => g.Manufacturer)
                .Include(g => g.MultiGPU)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gPU == null)
            {
                return NotFound();
            }

            return View(gPU);
        }

        // POST: GPUs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var gPU = await _context.GPU.FindAsync(id);
            _context.GPU.Remove(gPU);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GPUExists(int id)
        {
            return _context.GPU.Any(e => e.Id == id);
        }
    }
}
