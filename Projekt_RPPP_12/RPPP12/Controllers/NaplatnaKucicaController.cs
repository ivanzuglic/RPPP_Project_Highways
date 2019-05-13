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
    public class NaplatnaKucicaController : Controller
    {
        private readonly RPPP12Context _context;

        public NaplatnaKucicaController(RPPP12Context context)
        {
            _context = context;
        }

        // GET: NaplatnaKucica
        public async Task<IActionResult> Index()
        {
            var rPPP12Context = _context.NaplatnaKucica.Include(n => n.SifraBlagajnikaNavigation).Include(n => n.SifraPostajaNavigation).Include(n => n.VrstaNaplatneKuciceNavigation);
            return View(await rPPP12Context.ToListAsync());
        }

        // GET: NaplatnaKucica/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var naplatnaKucica = await _context.NaplatnaKucica
                .Include(n => n.SifraBlagajnikaNavigation)
                .Include(n => n.SifraPostajaNavigation)
                .Include(n => n.VrstaNaplatneKuciceNavigation)
                .FirstOrDefaultAsync(m => m.SifraKucica == id);
            if (naplatnaKucica == null)
            {
                return NotFound();
            }

            return View(naplatnaKucica);
        }

        // GET: NaplatnaKucica/Create
        public IActionResult Create()
        {
            ViewData["SifraBlagajnika"] = new SelectList(_context.Zaposlenik, "SifraZaposlenika", "Ime");
            ViewData["SifraPostaja"] = new SelectList(_context.NaplatnaPostaja, "SifraPostaje", "SifraPostaje");
            ViewData["VrstaNaplatneKucice"] = new SelectList(_context.VrstaNaplatneKucice, "VrstaNaplatneKucice1", "VrstaNaplatneKucice1");
            return View();
        }

        // POST: NaplatnaKucica/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SifraKucica,SifraPostaja,SifraBlagajnika,VrstaNaplatneKucice")] NaplatnaKucica naplatnaKucica)
        {
            if (ModelState.IsValid)
            {
                _context.Add(naplatnaKucica);
                await _context.SaveChangesAsync();
                TempData["create"] = "Create";
                return RedirectToAction(nameof(Index));
            }
            ViewData["SifraBlagajnika"] = new SelectList(_context.Zaposlenik, "SifraZaposlenika", "Ime", naplatnaKucica.SifraBlagajnika);
            ViewData["SifraPostaja"] = new SelectList(_context.NaplatnaPostaja, "SifraPostaje", "SifraPostaje", naplatnaKucica.SifraPostaja);
            ViewData["VrstaNaplatneKucice"] = new SelectList(_context.VrstaNaplatneKucice, "VrstaNaplatneKucice1", "VrstaNaplatneKucice1", naplatnaKucica.VrstaNaplatneKucice);
            return View(naplatnaKucica);
        }

        // GET: NaplatnaKucica/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var naplatnaKucica = await _context.NaplatnaKucica.FindAsync(id);
            if (naplatnaKucica == null)
            {
                return NotFound();
            }
            ViewData["SifraBlagajnika"] = new SelectList(_context.Zaposlenik, "SifraZaposlenika", "Ime", naplatnaKucica.SifraBlagajnika);
            ViewData["SifraPostaja"] = new SelectList(_context.NaplatnaPostaja, "SifraPostaje", "SifraPostaje", naplatnaKucica.SifraPostaja);
            ViewData["VrstaNaplatneKucice"] = new SelectList(_context.VrstaNaplatneKucice, "VrstaNaplatneKucice1", "VrstaNaplatneKucice1", naplatnaKucica.VrstaNaplatneKucice);
            return View(naplatnaKucica);
        }

        // POST: NaplatnaKucica/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SifraKucica,SifraPostaja,SifraBlagajnika,VrstaNaplatneKucice")] NaplatnaKucica naplatnaKucica)
        {
            if (id != naplatnaKucica.SifraKucica)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(naplatnaKucica);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NaplatnaKucicaExists(naplatnaKucica.SifraKucica))
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
            ViewData["SifraBlagajnika"] = new SelectList(_context.Zaposlenik, "SifraZaposlenika", "Ime", naplatnaKucica.SifraBlagajnika);
            ViewData["SifraPostaja"] = new SelectList(_context.NaplatnaPostaja, "SifraPostaje", "SifraPostaje", naplatnaKucica.SifraPostaja);
            ViewData["VrstaNaplatneKucice"] = new SelectList(_context.VrstaNaplatneKucice, "VrstaNaplatneKucice1", "VrstaNaplatneKucice1", naplatnaKucica.VrstaNaplatneKucice);
            return View(naplatnaKucica);
        }

        // GET: NaplatnaKucica/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var naplatnaKucica = await _context.NaplatnaKucica
                .Include(n => n.SifraBlagajnikaNavigation)
                .Include(n => n.SifraPostajaNavigation)
                .Include(n => n.VrstaNaplatneKuciceNavigation)
                .FirstOrDefaultAsync(m => m.SifraKucica == id);
            if (naplatnaKucica == null)
            {
                return NotFound();
            }

            return View(naplatnaKucica);
        }

        // POST: NaplatnaKucica/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var naplatnaKucica = await _context.NaplatnaKucica.FindAsync(id);
            _context.NaplatnaKucica.Remove(naplatnaKucica);
            await _context.SaveChangesAsync();
            TempData["delete"] = "Delete";
            return RedirectToAction(nameof(Index));
        }

        private bool NaplatnaKucicaExists(int id)
        {
            return _context.NaplatnaKucica.Any(e => e.SifraKucica == id);
        }
    }
}
