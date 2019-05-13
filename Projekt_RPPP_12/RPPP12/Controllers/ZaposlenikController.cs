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
    public class ZaposlenikController : Controller
    {
        private readonly RPPP12Context _context;

        public ZaposlenikController(RPPP12Context context)
        {
            _context = context;
        }

        // GET: Zaposlenik
        public async Task<IActionResult> Index()
        {
            var rPPP12Context = _context.Zaposlenik.Include(z => z.SifraPostajeNavigation).Include(z => z.SifraVrsteZaposlenikaNavigation);
            return View(await rPPP12Context.ToListAsync());
        }

        // GET: Zaposlenik/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zaposlenik = await _context.Zaposlenik
                .Include(z => z.SifraPostajeNavigation)
                .Include(z => z.SifraVrsteZaposlenikaNavigation)
                .FirstOrDefaultAsync(m => m.SifraZaposlenika == id);
            if (zaposlenik == null)
            {
                return NotFound();
            }

            return View(zaposlenik);
        }

        // GET: Zaposlenik/Create
        public IActionResult Create()
        {
            ViewData["SifraPostaje"] = new SelectList(_context.NaplatnaPostaja, "SifraPostaje", "SifraPostaje");
            ViewData["SifraVrsteZaposlenika"] = new SelectList(_context.VrstaZaposlenika, "SifraVrsteZaposlenika", "Naziv");
            return View();
        }

        // POST: Zaposlenik/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SifraZaposlenika,Ime,Prezime,Telefon,SifraPostaje,SifraVrsteZaposlenika")] Zaposlenik zaposlenik)
        {
            if (ModelState.IsValid)
            {
                _context.Add(zaposlenik);
                await _context.SaveChangesAsync();
                TempData["create"] = "Create";
                return RedirectToAction(nameof(Index));
            }
            ViewData["SifraPostaje"] = new SelectList(_context.NaplatnaPostaja, "SifraPostaje", "SifraPostaje", zaposlenik.SifraPostaje);
            ViewData["SifraVrsteZaposlenika"] = new SelectList(_context.VrstaZaposlenika, "SifraVrsteZaposlenika", "Naziv", zaposlenik.SifraVrsteZaposlenika);
            return View(zaposlenik);
        }

        // GET: Zaposlenik/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zaposlenik = await _context.Zaposlenik.FindAsync(id);
            if (zaposlenik == null)
            {
                return NotFound();
            }
            ViewData["SifraPostaje"] = new SelectList(_context.NaplatnaPostaja, "SifraPostaje", "SifraPostaje", zaposlenik.SifraPostaje);
            ViewData["SifraVrsteZaposlenika"] = new SelectList(_context.VrstaZaposlenika, "SifraVrsteZaposlenika", "Naziv", zaposlenik.SifraVrsteZaposlenika);
            return View(zaposlenik);
        }

        // POST: Zaposlenik/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SifraZaposlenika,Ime,Prezime,Telefon,SifraPostaje,SifraVrsteZaposlenika")] Zaposlenik zaposlenik)
        {
            if (id != zaposlenik.SifraZaposlenika)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(zaposlenik);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ZaposlenikExists(zaposlenik.SifraZaposlenika))
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
            ViewData["SifraPostaje"] = new SelectList(_context.NaplatnaPostaja, "SifraPostaje", "SifraPostaje", zaposlenik.SifraPostaje);
            ViewData["SifraVrsteZaposlenika"] = new SelectList(_context.VrstaZaposlenika, "SifraVrsteZaposlenika", "Naziv", zaposlenik.SifraVrsteZaposlenika);
            return View(zaposlenik);
        }

        // GET: Zaposlenik/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zaposlenik = await _context.Zaposlenik
                .Include(z => z.SifraPostajeNavigation)
                .Include(z => z.SifraVrsteZaposlenikaNavigation)
                .FirstOrDefaultAsync(m => m.SifraZaposlenika == id);
            if (zaposlenik == null)
            {
                return NotFound();
            }

            return View(zaposlenik);
        }

        // POST: Zaposlenik/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var zaposlenik = await _context.Zaposlenik.FindAsync(id);
            _context.Zaposlenik.Remove(zaposlenik);
            await _context.SaveChangesAsync();
            TempData["delete"] = "Delete";
            return RedirectToAction(nameof(Index));
        }

        private bool ZaposlenikExists(int id)
        {
            return _context.Zaposlenik.Any(e => e.SifraZaposlenika == id);
        }
    }
}
