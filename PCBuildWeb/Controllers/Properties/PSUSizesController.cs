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
    public class PSUSizesController : Controller
    {
        private readonly PCBuildWebContext _context;

        public PSUSizesController(PCBuildWebContext context)
        {
            _context = context;
        }

        // GET: PSUSizes
        public async Task<IActionResult> Index()
        {
            return View(await _context.PSUSize.ToListAsync());
        }

        // GET: PSUSizes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pSUSize = await _context.PSUSize
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pSUSize == null)
            {
                return NotFound();
            }

            return View(pSUSize);
        }

        // GET: PSUSizes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PSUSizes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] PSUSize pSUSize)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pSUSize);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pSUSize);
        }

        // GET: PSUSizes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pSUSize = await _context.PSUSize.FindAsync(id);
            if (pSUSize == null)
            {
                return NotFound();
            }
            return View(pSUSize);
        }

        // POST: PSUSizes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] PSUSize pSUSize)
        {
            if (id != pSUSize.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pSUSize);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PSUSizeExists(pSUSize.Id))
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
            return View(pSUSize);
        }

        // GET: PSUSizes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pSUSize = await _context.PSUSize
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pSUSize == null)
            {
                return NotFound();
            }

            return View(pSUSize);
        }

        // POST: PSUSizes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pSUSize = await _context.PSUSize.FindAsync(id);
            _context.PSUSize.Remove(pSUSize);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PSUSizeExists(int id)
        {
            return _context.PSUSize.Any(e => e.Id == id);
        }
    }
}
