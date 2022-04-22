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
    public class MoboChipsetsController : Controller
    {
        private readonly PCBuildWebContext _context;

        public MoboChipsetsController(PCBuildWebContext context)
        {
            _context = context;
        }

        // GET: MoboChipsets
        public async Task<IActionResult> Index()
        {
            return View(await _context.MoboChipset.ToListAsync());
        }

        // GET: MoboChipsets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var moboChipset = await _context.MoboChipset
                .FirstOrDefaultAsync(m => m.Id == id);
            if (moboChipset == null)
            {
                return NotFound();
            }

            return View(moboChipset);
        }

        // GET: MoboChipsets/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MoboChipsets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] MoboChipset moboChipset)
        {
            if (ModelState.IsValid)
            {
                _context.Add(moboChipset);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(moboChipset);
        }

        // GET: MoboChipsets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var moboChipset = await _context.MoboChipset.FindAsync(id);
            if (moboChipset == null)
            {
                return NotFound();
            }
            return View(moboChipset);
        }

        // POST: MoboChipsets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] MoboChipset moboChipset)
        {
            if (id != moboChipset.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(moboChipset);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MoboChipsetExists(moboChipset.Id))
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
            return View(moboChipset);
        }

        // GET: MoboChipsets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var moboChipset = await _context.MoboChipset
                .FirstOrDefaultAsync(m => m.Id == id);
            if (moboChipset == null)
            {
                return NotFound();
            }

            return View(moboChipset);
        }

        // POST: MoboChipsets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var moboChipset = await _context.MoboChipset.FindAsync(id);
            if (moboChipset is not null)
            {
                _context.MoboChipset.Remove(moboChipset);
                await _context.SaveChangesAsync();
            }            
            return RedirectToAction(nameof(Index));
        }

        private bool MoboChipsetExists(int id)
        {
            return _context.MoboChipset.Any(e => e.Id == id);
        }
    }
}
