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
    public class RacunController : Controller
    {
        private readonly RPPP12Context _context;

        public RacunController(RPPP12Context context)
        {
            _context = context;
        }

        // GET: Racun
        public async Task<IActionResult> Index()
        {
            var rPPP12Context = _context.Racun.Include(r => r.SifraKategorijaVozilaNavigation).Include(r => r.SifraKucicaNavigation).Include(r => r.SifraNacinPlacanjaNavigation);
            return View(await rPPP12Context.ToListAsync());
        }

        // GET: Racun/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var racun = await _context.Racun
                .Include(r => r.SifraKategorijaVozilaNavigation)
                .Include(r => r.SifraKucicaNavigation)
                .Include(r => r.SifraNacinPlacanjaNavigation)
                .FirstOrDefaultAsync(m => m.SifraRacun == id);
            if (racun == null)
            {
                return NotFound();
            }

            return View(racun);
        }

        // GET: Racun/Create
        public IActionResult Create()
        {
            ViewData["SifraKategorijaVozila"] = new SelectList(_context.KategorijaVozila, "SifraKategorijaVozila", "Opis");
            ViewData["SifraKucica"] = new SelectList(_context.NaplatnaKucica, "SifraKucica", "SifraKucica");
            ViewData["SifraNacinPlacanja"] = new SelectList(_context.NacinPlacanja, "SifraNacinPlacanja", "NacinPlacanja1");
            return View();
        }

        // POST: Racun/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SifraRacun,SifraKucica,RegistarskaOznaka,DatumVrijeme,SifraKategorijaVozila,SifraNacinPlacanja")] Racun racun)
        {
            if (ModelState.IsValid)
            {
                _context.Add(racun);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SifraKategorijaVozila"] = new SelectList(_context.KategorijaVozila, "SifraKategorijaVozila", "Opis", racun.SifraKategorijaVozila);
            ViewData["SifraKucica"] = new SelectList(_context.NaplatnaKucica, "SifraKucica", "SifraKucica", racun.SifraKucica);
            ViewData["SifraNacinPlacanja"] = new SelectList(_context.NacinPlacanja, "SifraNacinPlacanja", "NacinPlacanja1", racun.SifraNacinPlacanja);
            return View(racun);
        }

        // GET: Racun/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var racun = await _context.Racun.FindAsync(id);
            if (racun == null)
            {
                return NotFound();
            }
            ViewData["SifraKategorijaVozila"] = new SelectList(_context.KategorijaVozila, "SifraKategorijaVozila", "Opis", racun.SifraKategorijaVozila);
            ViewData["SifraKucica"] = new SelectList(_context.NaplatnaKucica, "SifraKucica", "SifraKucica", racun.SifraKucica);
            ViewData["SifraNacinPlacanja"] = new SelectList(_context.NacinPlacanja, "SifraNacinPlacanja", "NacinPlacanja1", racun.SifraNacinPlacanja);
            return View(racun);
        }

        // POST: Racun/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SifraRacun,SifraKucica,RegistarskaOznaka,DatumVrijeme,SifraKategorijaVozila,SifraNacinPlacanja")] Racun racun)
        {
            if (id != racun.SifraRacun)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(racun);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RacunExists(racun.SifraRacun))
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
            ViewData["SifraKategorijaVozila"] = new SelectList(_context.KategorijaVozila, "SifraKategorijaVozila", "Opis", racun.SifraKategorijaVozila);
            ViewData["SifraKucica"] = new SelectList(_context.NaplatnaKucica, "SifraKucica", "SifraKucica", racun.SifraKucica);
            ViewData["SifraNacinPlacanja"] = new SelectList(_context.NacinPlacanja, "SifraNacinPlacanja", "NacinPlacanja1", racun.SifraNacinPlacanja);
            return View(racun);
        }

        // GET: Racun/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var racun = await _context.Racun
                .Include(r => r.SifraKategorijaVozilaNavigation)
                .Include(r => r.SifraKucicaNavigation)
                .Include(r => r.SifraNacinPlacanjaNavigation)
                .FirstOrDefaultAsync(m => m.SifraRacun == id);
            if (racun == null)
            {
                return NotFound();
            }

            return View(racun);
        }

        // POST: Racun/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var racun = await _context.Racun.FindAsync(id);
            _context.Racun.Remove(racun);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RacunExists(int id)
        {
            return _context.Racun.Any(e => e.SifraRacun == id);
        }
    }
}
