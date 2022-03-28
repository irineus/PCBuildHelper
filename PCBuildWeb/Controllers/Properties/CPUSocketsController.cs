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
    public class CPUSocketsController : Controller
    {
        private readonly PCBuildWebContext _context;

        public CPUSocketsController(PCBuildWebContext context)
        {
            _context = context;
        }

        // GET: CPUSockets
        public async Task<IActionResult> Index()
        {
            return View(await _context.CPUSocket.ToListAsync());
        }

        // GET: CPUSockets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cPUSocket = await _context.CPUSocket
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cPUSocket == null)
            {
                return NotFound();
            }

            return View(cPUSocket);
        }

        // GET: CPUSockets/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CPUSockets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] CPUSocket cPUSocket)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cPUSocket);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cPUSocket);
        }

        // GET: CPUSockets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cPUSocket = await _context.CPUSocket.FindAsync(id);
            if (cPUSocket == null)
            {
                return NotFound();
            }
            return View(cPUSocket);
        }

        // POST: CPUSockets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] CPUSocket cPUSocket)
        {
            if (id != cPUSocket.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cPUSocket);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CPUSocketExists(cPUSocket.Id))
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
            return View(cPUSocket);
        }

        // GET: CPUSockets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cPUSocket = await _context.CPUSocket
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cPUSocket == null)
            {
                return NotFound();
            }

            return View(cPUSocket);
        }

        // POST: CPUSockets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cPUSocket = await _context.CPUSocket.FindAsync(id);
            _context.CPUSocket.Remove(cPUSocket);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CPUSocketExists(int id)
        {
            return _context.CPUSocket.Any(e => e.Id == id);
        }
    }
}
