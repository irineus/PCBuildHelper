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
    public class PowerConnectorsController : Controller
    {
        private readonly PCBuildWebContext _context;

        public PowerConnectorsController(PCBuildWebContext context)
        {
            _context = context;
        }

        // GET: PowerConnectors
        public async Task<IActionResult> Index()
        {
            return View(await _context.PowerConnector.ToListAsync());
        }

        // GET: PowerConnectors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var powerConnector = await _context.PowerConnector
                .FirstOrDefaultAsync(m => m.Id == id);
            if (powerConnector == null)
            {
                return NotFound();
            }

            return View(powerConnector);
        }

        // GET: PowerConnectors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PowerConnectors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] PowerConnector powerConnector)
        {
            if (ModelState.IsValid)
            {
                _context.Add(powerConnector);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(powerConnector);
        }

        // GET: PowerConnectors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var powerConnector = await _context.PowerConnector.FindAsync(id);
            if (powerConnector == null)
            {
                return NotFound();
            }
            return View(powerConnector);
        }

        // POST: PowerConnectors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] PowerConnector powerConnector)
        {
            if (id != powerConnector.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(powerConnector);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PowerConnectorExists(powerConnector.Id))
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
            return View(powerConnector);
        }

        // GET: PowerConnectors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var powerConnector = await _context.PowerConnector
                .FirstOrDefaultAsync(m => m.Id == id);
            if (powerConnector == null)
            {
                return NotFound();
            }

            return View(powerConnector);
        }

        // POST: PowerConnectors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var powerConnector = await _context.PowerConnector.FindAsync(id);
            _context.PowerConnector.Remove(powerConnector);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PowerConnectorExists(int id)
        {
            return _context.PowerConnector.Any(e => e.Id == id);
        }
    }
}
