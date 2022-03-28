using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PCBuildWeb.Data;
using PCBuildWeb.Models.Entities.Properties;

namespace PCBuildWeb.Controllers.Properties
{
    public class CPUSeriesController : Controller
    {
        private readonly PCBuildWebContext _context;

        public CPUSeriesController(PCBuildWebContext context)
        {
            _context = context;
        }

        // GET: CPUSeries
        public async Task<IActionResult> Index()
        {
            return View(await _context.CPUSeries.ToListAsync());
        }

        // GET: CPUSeries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cPUSeries = await _context.CPUSeries
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cPUSeries == null)
            {
                return NotFound();
            }

            return View(cPUSeries);
        }

        // GET: CPUSeries/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CPUSeries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] CPUSeries cPUSeries)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cPUSeries);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cPUSeries);
        }

        // GET: CPUSeries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cPUSeries = await _context.CPUSeries.FindAsync(id);
            if (cPUSeries == null)
            {
                return NotFound();
            }
            return View(cPUSeries);
        }

        // POST: CPUSeries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] CPUSeries cPUSeries)
        {
            if (id != cPUSeries.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cPUSeries);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CPUSeriesExists(cPUSeries.Id))
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
            return View(cPUSeries);
        }

        // GET: CPUSeries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cPUSeries = await _context.CPUSeries
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cPUSeries == null)
            {
                return NotFound();
            }

            return View(cPUSeries);
        }

        // POST: CPUSeries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cPUSeries = await _context.CPUSeries.FindAsync(id);
            _context.CPUSeries.Remove(cPUSeries);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CPUSeriesExists(int id)
        {
            return _context.CPUSeries.Any(e => e.Id == id);
        }
    }
}
