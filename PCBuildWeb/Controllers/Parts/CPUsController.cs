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
    public class CPUsController : Controller
    {
        private readonly PCBuildWebContext _context;
        private readonly CPUService _cpuService;

        public CPUsController(PCBuildWebContext context, CPUService cpuService)
        {
            _context = context;
            _cpuService = cpuService;
        }

        // GET: CPUs
        public async Task<IActionResult> Index()
        {
            var pCBuildWebContext = await _cpuService.FindAllAsync();
            return View(pCBuildWebContext);
        }

        // GET: CPUs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cPU = await _cpuService.FindByIdAsync(id.Value);
            if (cPU == null)
            {
                return NotFound();
            }

            return View(cPU);
        }

        // GET: CPUs/Create
        public IActionResult Create()
        {
            ViewData["CPUSocketId"] = new SelectList(_context.CPUSocket, "Id", "Name");
            ViewData["ManufacturerId"] = new SelectList(_context.Manufacturer, "Id", "Name");
            ViewData["SeriesId"] = new SelectList(_context.CPUSeries, "Id", "Name");
            return View();
        }

        // POST: CPUs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SeriesId,RankingScore,Frequency,Cores,CPUSocketId,Wattage,Overclockable,ThermalThrottling,Voltage,BasicCPUScore,ScoreToValueRatio,DefaultMemorySpeed,OverclockedCPUScore,MultiplierStep,NumberOfDies,MaxMemoryChannels,OverclockedVoltage,OverclockedFrequency,CoreClockMultiplier,MemChannelsMultiplier,MemClockMultiplier,Id,Name,PartType,ManufacturerId,Price,SellPrice,LevelUnlock,LevelPercent,Lighting")] CPU cPU)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cPU);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CPUSocketId"] = new SelectList(_context.CPUSocket, "Id", "Name", cPU.CPUSocketId);
            ViewData["ManufacturerId"] = new SelectList(_context.Manufacturer, "Id", "Name", cPU.ManufacturerId);
            ViewData["SeriesId"] = new SelectList(_context.CPUSeries, "Id", "Name", cPU.SeriesId);
            return View(cPU);
        }

        // GET: CPUs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cPU = await _context.CPU.FindAsync(id);
            if (cPU == null)
            {
                return NotFound();
            }
            ViewData["CPUSocketId"] = new SelectList(_context.CPUSocket, "Id", "Name", cPU.CPUSocketId);
            ViewData["ManufacturerId"] = new SelectList(_context.Manufacturer, "Id", "Name", cPU.ManufacturerId);
            ViewData["SeriesId"] = new SelectList(_context.CPUSeries, "Id", "Name", cPU.SeriesId);
            return View(cPU);
        }

        // POST: CPUs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SeriesId,RankingScore,Frequency,Cores,CPUSocketId,Wattage,Overclockable,ThermalThrottling,Voltage,BasicCPUScore,ScoreToValueRatio,DefaultMemorySpeed,OverclockedCPUScore,MultiplierStep,NumberOfDies,MaxMemoryChannels,OverclockedVoltage,OverclockedFrequency,CoreClockMultiplier,MemChannelsMultiplier,MemClockMultiplier,Id,Name,PartType,ManufacturerId,Price,SellPrice,LevelUnlock,LevelPercent,Lighting")] CPU cPU)
        {
            if (id != cPU.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cPU);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_cpuService.CPUExists(cPU.Id))
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
            ViewData["CPUSocketId"] = new SelectList(_context.CPUSocket, "Id", "Name", cPU.CPUSocketId);
            ViewData["ManufacturerId"] = new SelectList(_context.Manufacturer, "Id", "Name", cPU.ManufacturerId);
            ViewData["SeriesId"] = new SelectList(_context.CPUSeries, "Id", "Name", cPU.SeriesId);
            return View(cPU);
        }

        // GET: CPUs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cPU = await _context.CPU
                .Include(c => c.CPUSocket)
                .Include(c => c.Manufacturer)
                .Include(c => c.Series)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cPU == null)
            {
                return NotFound();
            }

            return View(cPU);
        }

        // POST: CPUs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cPU = await _context.CPU.FindAsync(id);
            _context.CPU.Remove(cPU);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
