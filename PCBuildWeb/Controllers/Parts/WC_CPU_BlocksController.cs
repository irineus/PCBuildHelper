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
    public class WC_CPU_BlocksController : Controller
    {
        private readonly PCBuildWebContext _context;
        private readonly WC_CPU_BlockService _wcCPUBlockService;

        public WC_CPU_BlocksController(PCBuildWebContext context, WC_CPU_BlockService wcCPUBlockService)
        {
            _context = context;
            _wcCPUBlockService = wcCPUBlockService;
        }

        // GET: WC_CPU_Blocks
        public async Task<IActionResult> Index()
        {
            var pCBuildWebContext = await _wcCPUBlockService.FindAllAsync();
            return View(pCBuildWebContext);
        }

        // GET: WC_CPU_Blocks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wC_CPU_Block = await _wcCPUBlockService.FindByIdAsync(id.Value);
            if (wC_CPU_Block == null)
            {
                return NotFound();
            }

            return View(wC_CPU_Block);
        }

        // GET: WC_CPU_Blocks/Create
        public IActionResult Create()
        {
            ViewData["ManufacturerId"] = new SelectList(_context.Manufacturer, "Id", "Name");
            return View();
        }

        // POST: WC_CPU_Blocks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,PartType,ManufacturerId,Price,SellPrice,LevelUnlock,LevelPercent,Lighting")] WC_CPU_Block wC_CPU_Block)
        {
            if (ModelState.IsValid)
            {
                _context.Add(wC_CPU_Block);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ManufacturerId"] = new SelectList(_context.Manufacturer, "Id", "Name", wC_CPU_Block.ManufacturerId);
            return View(wC_CPU_Block);
        }

        // GET: WC_CPU_Blocks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wC_CPU_Block = await _context.WC_CPU_Block.FindAsync(id);
            if (wC_CPU_Block == null)
            {
                return NotFound();
            }
            ViewData["ManufacturerId"] = new SelectList(_context.Manufacturer, "Id", "Name", wC_CPU_Block.ManufacturerId);
            return View(wC_CPU_Block);
        }

        // POST: WC_CPU_Blocks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,PartType,ManufacturerId,Price,SellPrice,LevelUnlock,LevelPercent,Lighting")] WC_CPU_Block wC_CPU_Block)
        {
            if (id != wC_CPU_Block.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(wC_CPU_Block);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_wcCPUBlockService.WC_CPU_BlockExists(wC_CPU_Block.Id))
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
            ViewData["ManufacturerId"] = new SelectList(_context.Manufacturer, "Id", "Name", wC_CPU_Block.ManufacturerId);
            return View(wC_CPU_Block);
        }

        // GET: WC_CPU_Blocks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wC_CPU_Block = await _context.WC_CPU_Block
                .Include(w => w.Manufacturer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (wC_CPU_Block == null)
            {
                return NotFound();
            }

            return View(wC_CPU_Block);
        }

        // POST: WC_CPU_Blocks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var wC_CPU_Block = await _context.WC_CPU_Block.FindAsync(id);
            _context.WC_CPU_Block.Remove(wC_CPU_Block);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
