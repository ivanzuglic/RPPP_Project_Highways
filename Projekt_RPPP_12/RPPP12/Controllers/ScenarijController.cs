using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RPPP12.Models;

namespace RPPP12.Controllers
{
    public class ScenarijController : Controller
    {
        private readonly RPPP12Context _context;

        public ScenarijController(RPPP12Context context)
        {
            _context = context;
        }

        // GET: Scenarij
        public async Task<IActionResult> Index()
        {
            var rPPP12Context = _context.Scenarij.Include(s => s.SifraVrsteScenarijaNavigation);
            return View(await rPPP12Context.ToListAsync());
        }

        // GET: Scenarij/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var scenarij = await _context.Scenarij
                .Include(s => s.SifraVrsteScenarijaNavigation)
                .FirstOrDefaultAsync(m => m.SifraScenarija == id);
            if (scenarij == null)
            {
                return NotFound();
            }

            return View(scenarij);
        }

        // GET: Scenarij/Create
        public IActionResult Create()
        {
            ViewData["SifraVrsteScenarija"] = new SelectList(_context.KategorijaScenarija, "SifraKategorijeScenarija", "NazivKategorijeScenarija");
            return View();
        }

        // POST: Scenarij/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SifraScenarija,NazivScenarija,Procedura,SifraVrsteObjekta,SifraVrsteScenarija")] Scenarij scenarij)
        {
            if (ModelState.IsValid)
            {
                _context.Add(scenarij);
                await _context.SaveChangesAsync();
                TempData["create"] = "Create";
                return RedirectToAction(nameof(Index));
            }
            ViewData["SifraVrsteScenarija"] = new SelectList(_context.KategorijaScenarija, "SifraKategorijeScenarija", "NazivKategorijeScenarija", scenarij.SifraVrsteScenarija);
            return View(scenarij);
        }

        // GET: Scenarij/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var scenarij = await _context.Scenarij.FindAsync(id);
            if (scenarij == null)
            {
                return NotFound();
            }
            ViewData["SifraVrsteScenarija"] = new SelectList(_context.KategorijaScenarija, "SifraKategorijeScenarija", "NazivKategorijeScenarija", scenarij.SifraVrsteScenarija);
            return View(scenarij);
        }

        // POST: Scenarij/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SifraScenarija,NazivScenarija,Procedura,SifraVrsteObjekta,SifraVrsteScenarija")] Scenarij scenarij)
        {
            if (id != scenarij.SifraScenarija)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(scenarij);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ScenarijExists(scenarij.SifraScenarija))
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
            ViewData["SifraVrsteScenarija"] = new SelectList(_context.KategorijaScenarija, "SifraKategorijeScenarija", "NazivKategorijeScenarija", scenarij.SifraVrsteScenarija);
            return View(scenarij);
        }

        // GET: Scenarij/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var scenarij = await _context.Scenarij
                .Include(s => s.SifraVrsteScenarijaNavigation)
                .FirstOrDefaultAsync(m => m.SifraScenarija == id);
            if (scenarij == null)
            {
                return NotFound();
            }

            return View(scenarij);
        }

        // POST: Scenarij/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var scenarij = await _context.Scenarij.FindAsync(id);
            _context.Scenarij.Remove(scenarij);
            await _context.SaveChangesAsync();
            TempData["delete"] = "Delete";
            return RedirectToAction(nameof(Index));
        }

        private bool ScenarijExists(int id)
        {
            return _context.Scenarij.Any(e => e.SifraScenarija == id);
        }
    }
}
