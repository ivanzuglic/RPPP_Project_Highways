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
    public class AutocestaController : Controller
    {
        private readonly RPPP12Context _context;

        public AutocestaController(RPPP12Context context)
        {
            _context = context;
        }

        // GET: Autocesta
        public async Task<IActionResult> Index()
        {
            var rPPP12Context = _context.Autocesta.Include(a => a.SifraNacinaPlacanjaNavigation).Include(a => a.SifraPocetkaNavigation).Include(a => a.SifraUpraviteljaNavigation).Include(a => a.SifraZavrsetkaNavigation);
            return View(await rPPP12Context.ToListAsync());
        }

        // GET: Autocesta/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var autocesta = await _context.Autocesta
                .Include(a => a.SifraNacinaPlacanjaNavigation)
                .Include(a => a.SifraPocetkaNavigation)
                .Include(a => a.SifraUpraviteljaNavigation)
                .Include(a => a.SifraZavrsetkaNavigation)
                .FirstOrDefaultAsync(m => m.SifraAutoceste == id);
            if (autocesta == null)
            {
                return NotFound();
            }

            return View(autocesta);
        }

        // GET: Autocesta/Create
        public IActionResult Create()
        {
            ViewData["SifraNacinaPlacanja"] = new SelectList(_context.SustavNaplate, "SifraNacinaPlacanja", "NacinPlacanja");
            ViewData["SifraPocetka"] = new SelectList(_context.LokacijaAutoceste, "SifraLokacije", "ImeLokacije");
            ViewData["SifraUpravitelja"] = new SelectList(_context.Upravitelj, "SifraUpravitelja", "Email");
            ViewData["SifraZavrsetka"] = new SelectList(_context.LokacijaAutoceste, "SifraLokacije", "ImeLokacije");
            return View();
        }

        // POST: Autocesta/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SifraAutoceste,ImeAutoceste,SifraPocetka,SifraZavrsetka,Nadimak,SifraUpravitelja,Kilometraza,SifraNacinaPlacanja")] Autocesta autocesta)
        {
            if (ModelState.IsValid)
            {
                _context.Add(autocesta);
                await _context.SaveChangesAsync();
                TempData["create"] = "Create";
                return RedirectToAction(nameof(Index));
            }
            ViewData["SifraNacinaPlacanja"] = new SelectList(_context.SustavNaplate, "SifraNacinaPlacanja", "NacinPlacanja", autocesta.SifraNacinaPlacanja);
            ViewData["SifraPocetka"] = new SelectList(_context.LokacijaAutoceste, "SifraLokacije", "ImeLokacije", autocesta.SifraPocetka);
            ViewData["SifraUpravitelja"] = new SelectList(_context.Upravitelj, "SifraUpravitelja", "Email", autocesta.SifraUpravitelja);
            ViewData["SifraZavrsetka"] = new SelectList(_context.LokacijaAutoceste, "SifraLokacije", "ImeLokacije", autocesta.SifraZavrsetka);
            return View(autocesta);
        }

        // GET: Autocesta/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var autocesta = await _context.Autocesta.FindAsync(id);
            if (autocesta == null)
            {
                return NotFound();
            }
            ViewData["SifraNacinaPlacanja"] = new SelectList(_context.SustavNaplate, "SifraNacinaPlacanja", "NacinPlacanja", autocesta.SifraNacinaPlacanja);
            ViewData["SifraPocetka"] = new SelectList(_context.LokacijaAutoceste, "SifraLokacije", "ImeLokacije", autocesta.SifraPocetka);
            ViewData["SifraUpravitelja"] = new SelectList(_context.Upravitelj, "SifraUpravitelja", "Email", autocesta.SifraUpravitelja);
            ViewData["SifraZavrsetka"] = new SelectList(_context.LokacijaAutoceste, "SifraLokacije", "ImeLokacije", autocesta.SifraZavrsetka);
            return View(autocesta);
        }

        // POST: Autocesta/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SifraAutoceste,ImeAutoceste,SifraPocetka,SifraZavrsetka,Nadimak,SifraUpravitelja,Kilometraza,SifraNacinaPlacanja")] Autocesta autocesta)
        {
            if (id != autocesta.SifraAutoceste)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(autocesta);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AutocestaExists(autocesta.SifraAutoceste))
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
            ViewData["SifraNacinaPlacanja"] = new SelectList(_context.SustavNaplate, "SifraNacinaPlacanja", "NacinPlacanja", autocesta.SifraNacinaPlacanja);
            ViewData["SifraPocetka"] = new SelectList(_context.LokacijaAutoceste, "SifraLokacije", "ImeLokacije", autocesta.SifraPocetka);
            ViewData["SifraUpravitelja"] = new SelectList(_context.Upravitelj, "SifraUpravitelja", "Email", autocesta.SifraUpravitelja);
            ViewData["SifraZavrsetka"] = new SelectList(_context.LokacijaAutoceste, "SifraLokacije", "ImeLokacije", autocesta.SifraZavrsetka);
            return View(autocesta);
        }

        // GET: Autocesta/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var autocesta = await _context.Autocesta
                .Include(a => a.SifraNacinaPlacanjaNavigation)
                .Include(a => a.SifraPocetkaNavigation)
                .Include(a => a.SifraUpraviteljaNavigation)
                .Include(a => a.SifraZavrsetkaNavigation)
                .FirstOrDefaultAsync(m => m.SifraAutoceste == id);
            if (autocesta == null)
            {
                return NotFound();
            }

            return View(autocesta);
        }

        // POST: Autocesta/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var autocesta = await _context.Autocesta.FindAsync(id);
            _context.Autocesta.Remove(autocesta);
            await _context.SaveChangesAsync();
            TempData["delete"] = "Delete";
            return RedirectToAction(nameof(Index));
        }

        private bool AutocestaExists(int id)
        {
            return _context.Autocesta.Any(e => e.SifraAutoceste == id);
        }
    }
}
