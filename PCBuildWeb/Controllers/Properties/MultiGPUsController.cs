#nullable disable
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
    public class MultiGPUsController : Controller
    {
        private readonly PCBuildWebContext _context;

        public MultiGPUsController(PCBuildWebContext context)
        {
            _context = context;
        }

        // GET: MultiGPUs
        public async Task<IActionResult> Index()
        {
            return View(await _context.MultiGPU.ToListAsync());
        }

        // GET: MultiGPUs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var multiGPU = await _context.MultiGPU
                .FirstOrDefaultAsync(m => m.Id == id);
            if (multiGPU == null)
            {
                return NotFound();
            }

            return View(multiGPU);
        }

        // GET: MultiGPUs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MultiGPUs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] MultiGPU multiGPU)
        {
            if (ModelState.IsValid)
            {
                _context.Add(multiGPU);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(multiGPU);
        }

        // GET: MultiGPUs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var multiGPU = await _context.MultiGPU.FindAsync(id);
            if (multiGPU == null)
            {
                return NotFound();
            }
            return View(multiGPU);
        }

        // POST: MultiGPUs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] MultiGPU multiGPU)
        {
            if (id != multiGPU.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(multiGPU);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MultiGPUExists(multiGPU.Id))
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
            return View(multiGPU);
        }

        // GET: MultiGPUs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var multiGPU = await _context.MultiGPU
                .FirstOrDefaultAsync(m => m.Id == id);
            if (multiGPU == null)
            {
                return NotFound();
            }

            return View(multiGPU);
        }

        // POST: MultiGPUs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var multiGPU = await _context.MultiGPU.FindAsync(id);
            _context.MultiGPU.Remove(multiGPU);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MultiGPUExists(int id)
        {
            return _context.MultiGPU.Any(e => e.Id == id);
        }
    }
}
