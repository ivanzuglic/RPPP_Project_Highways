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
    public class StanjeController : Controller
    {
        private readonly RPPP12Context _context;

        public StanjeController(RPPP12Context context)
        {
            _context = context;
        }

        // GET: Stanje
        public async Task<IActionResult> Index()
        {
            var rPPP12Context = _context.Stanje.Include(s => s.SifraDogadajNavigation).Include(s => s.SifraZabranaNavigation);
            return View(await rPPP12Context.ToListAsync());
        }

        // GET: Stanje/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stanje = await _context.Stanje
                .Include(s => s.SifraDogadajNavigation)
                .Include(s => s.SifraZabranaNavigation)
                .FirstOrDefaultAsync(m => m.SifraStanje == id);
            if (stanje == null)
            {
                return NotFound();
            }

            return View(stanje);
        }

        // GET: Stanje/Create
        public IActionResult Create()
        {
            ViewData["SifraDogadaj"] = new SelectList(_context.Dogadaj, "SifraDogadaj", "SifraDogadaj");
            ViewData["SifraZabrana"] = new SelectList(_context.Zabrana, "SifraZabrana", "SifraZabrana");
            return View();
        }

        // POST: Stanje/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SifraStanje,VrijemePocetka,VrijemeZavrsetka,SifraDogadaj,SifraZabrana,Opis")] Stanje stanje)
        {
            if (ModelState.IsValid)
            {
                _context.Add(stanje);
                await _context.SaveChangesAsync();
                TempData["create"] = "Create";
                return RedirectToAction(nameof(Index));
            }
            ViewData["SifraDogadaj"] = new SelectList(_context.Dogadaj, "SifraDogadaj", "SifraDogadaj", stanje.SifraDogadaj);
            ViewData["SifraZabrana"] = new SelectList(_context.Zabrana, "SifraZabrana", "SifraZabrana", stanje.SifraZabrana);
            return View(stanje);
        }

        // GET: Stanje/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stanje = await _context.Stanje.FindAsync(id);
            if (stanje == null)
            {
                return NotFound();
            }
            ViewData["SifraDogadaj"] = new SelectList(_context.Dogadaj, "SifraDogadaj", "SifraDogadaj", stanje.SifraDogadaj);
            ViewData["SifraZabrana"] = new SelectList(_context.Zabrana, "SifraZabrana", "SifraZabrana", stanje.SifraZabrana);
            return View(stanje);
        }

        // POST: Stanje/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SifraStanje,VrijemePocetka,VrijemeZavrsetka,SifraDogadaj,SifraZabrana,Opis")] Stanje stanje)
        {
            if (id != stanje.SifraStanje)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(stanje);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StanjeExists(stanje.SifraStanje))
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
            ViewData["SifraDogadaj"] = new SelectList(_context.Dogadaj, "SifraDogadaj", "SifraDogadaj", stanje.SifraDogadaj);
            ViewData["SifraZabrana"] = new SelectList(_context.Zabrana, "SifraZabrana", "SifraZabrana", stanje.SifraZabrana);
            return View(stanje);
        }

        // GET: Stanje/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stanje = await _context.Stanje
                .Include(s => s.SifraDogadajNavigation)
                .Include(s => s.SifraZabranaNavigation)
                .FirstOrDefaultAsync(m => m.SifraStanje == id);
            if (stanje == null)
            {
                return NotFound();
            }

            return View(stanje);
        }

        // POST: Stanje/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var stanje = await _context.Stanje.FindAsync(id);
            _context.Stanje.Remove(stanje);
            await _context.SaveChangesAsync();
            TempData["delete"] = "Delete";
            return RedirectToAction(nameof(Index));
        }

        private bool StanjeExists(int id)
        {
            return _context.Stanje.Any(e => e.SifraStanje == id);
        }
    }
}
