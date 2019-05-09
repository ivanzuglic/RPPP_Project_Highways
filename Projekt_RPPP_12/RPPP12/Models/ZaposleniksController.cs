using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace RPPP12.Models
{
    public class ZaposleniksController : Controller
    {
        private readonly RPPP12Context _context;

        public ZaposleniksController(RPPP12Context context)
        {
            _context = context;
        }

        // GET: Zaposleniks
        public async Task<IActionResult> Index()
        {
            var rPPP12Context = _context.Zaposlenik.Include(z => z.SifraPostajeNavigation).Include(z => z.SifraVrsteZaposlenikaNavigation);
            return View(await rPPP12Context.ToListAsync());
        }

        // GET: Zaposleniks/Details/5
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

        // GET: Zaposleniks/Create
        public IActionResult Create()
        {
            ViewData["SifraPostaje"] = new SelectList(_context.NaplatnaPostaja, "SifraPostaje", "SifraPostaje");
            ViewData["SifraVrsteZaposlenika"] = new SelectList(_context.VrstaZaposlenika, "SifraVrsteZaposlenika", "Naziv");
            return View();
        }

        // POST: Zaposleniks/Create
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
                return RedirectToAction(nameof(Index));
            }
            ViewData["SifraPostaje"] = new SelectList(_context.NaplatnaPostaja, "SifraPostaje", "SifraPostaje", zaposlenik.SifraPostaje);
            ViewData["SifraVrsteZaposlenika"] = new SelectList(_context.VrstaZaposlenika, "SifraVrsteZaposlenika", "Naziv", zaposlenik.SifraVrsteZaposlenika);
            return View(zaposlenik);
        }

        // GET: Zaposleniks/Edit/5
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

        // POST: Zaposleniks/Edit/5
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

        // GET: Zaposleniks/Delete/5
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

        // POST: Zaposleniks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var zaposlenik = await _context.Zaposlenik.FindAsync(id);
            _context.Zaposlenik.Remove(zaposlenik);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ZaposlenikExists(int id)
        {
            return _context.Zaposlenik.Any(e => e.SifraZaposlenika == id);
        }
    }
}
