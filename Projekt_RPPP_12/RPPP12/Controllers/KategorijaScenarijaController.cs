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
    public class KategorijaScenarijaController : Controller
    {
        private readonly RPPP12Context _context;

        public KategorijaScenarijaController(RPPP12Context context)
        {
            _context = context;
        }

        // GET: KategorijaScenarija
        public async Task<IActionResult> Index()
        {
            return View(await _context.KategorijaScenarija.ToListAsync());
        }

        // GET: KategorijaScenarija/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kategorijaScenarija = await _context.KategorijaScenarija
                .FirstOrDefaultAsync(m => m.SifraKategorijeScenarija == id);
            if (kategorijaScenarija == null)
            {
                return NotFound();
            }

            return View(kategorijaScenarija);
        }

        // GET: KategorijaScenarija/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: KategorijaScenarija/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SifraKategorijeScenarija,NazivKategorijeScenarija")] KategorijaScenarija kategorijaScenarija)
        {
            if (ModelState.IsValid)
            {
                _context.Add(kategorijaScenarija);
                await _context.SaveChangesAsync();
                TempData["create"] = "Create";
                return RedirectToAction(nameof(Index));
            }
            return View(kategorijaScenarija);
        }

        // GET: KategorijaScenarija/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kategorijaScenarija = await _context.KategorijaScenarija.FindAsync(id);
            if (kategorijaScenarija == null)
            {
                return NotFound();
            }
            return View(kategorijaScenarija);
        }

        // POST: KategorijaScenarija/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SifraKategorijeScenarija,NazivKategorijeScenarija")] KategorijaScenarija kategorijaScenarija)
        {
            if (id != kategorijaScenarija.SifraKategorijeScenarija)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(kategorijaScenarija);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KategorijaScenarijaExists(kategorijaScenarija.SifraKategorijeScenarija))
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
            return View(kategorijaScenarija);
        }

        // GET: KategorijaScenarija/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kategorijaScenarija = await _context.KategorijaScenarija
                .FirstOrDefaultAsync(m => m.SifraKategorijeScenarija == id);
            if (kategorijaScenarija == null)
            {
                return NotFound();
            }

            return View(kategorijaScenarija);
        }

        // POST: KategorijaScenarija/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var kategorijaScenarija = await _context.KategorijaScenarija.FindAsync(id);
            _context.KategorijaScenarija.Remove(kategorijaScenarija);
            await _context.SaveChangesAsync();
            TempData["delete"] = "Delete";
            return RedirectToAction(nameof(Index));
        }

        private bool KategorijaScenarijaExists(int id)
        {
            return _context.KategorijaScenarija.Any(e => e.SifraKategorijeScenarija == id);
        }
    }
}
