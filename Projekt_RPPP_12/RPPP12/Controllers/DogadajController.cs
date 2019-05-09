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
    public class DogadajController : Controller
    {
        private readonly RPPP12Context _context;

        public DogadajController(RPPP12Context context)
        {
            _context = context;
        }

        // GET: Dogadaj
        public async Task<IActionResult> Index()
        {
            var rPPP12Context = _context.Dogadaj.Include(d => d.SifraDionicaNavigation).Include(d => d.SifraRazinaOpasnostiNavigation);
            return View(await rPPP12Context.ToListAsync());
        }

        // GET: Dogadaj/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dogadaj = await _context.Dogadaj
                .Include(d => d.SifraDionicaNavigation)
                .Include(d => d.SifraRazinaOpasnostiNavigation)
                .FirstOrDefaultAsync(m => m.SifraDogadaj == id);
            if (dogadaj == null)
            {
                return NotFound();
            }

            return View(dogadaj);
        }

        // GET: Dogadaj/Create
        public IActionResult Create()
        {
            ViewData["SifraDionica"] = new SelectList(_context.Dionica, "SifraDionice", "Naziv");
            ViewData["SifraRazinaOpasnosti"] = new SelectList(_context.RazinaOpasnosti, "SifraRazinaOpasnosti", "NazivRazinaOpasnosti");
            return View();
        }

        // POST: Dogadaj/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SifraDogadaj,DatumVrijeme,Link,SifraRazinaOpasnosti,SifraDionica,Opis")] Dogadaj dogadaj)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dogadaj);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SifraDionica"] = new SelectList(_context.Dionica, "SifraDionice", "Naziv", dogadaj.SifraDionica);
            ViewData["SifraRazinaOpasnosti"] = new SelectList(_context.RazinaOpasnosti, "SifraRazinaOpasnosti", "NazivRazinaOpasnosti", dogadaj.SifraRazinaOpasnosti);
            return View(dogadaj);
        }

        // GET: Dogadaj/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dogadaj = await _context.Dogadaj.FindAsync(id);
            if (dogadaj == null)
            {
                return NotFound();
            }
            ViewData["SifraDionica"] = new SelectList(_context.Dionica, "SifraDionice", "Naziv", dogadaj.SifraDionica);
            ViewData["SifraRazinaOpasnosti"] = new SelectList(_context.RazinaOpasnosti, "SifraRazinaOpasnosti", "NazivRazinaOpasnosti", dogadaj.SifraRazinaOpasnosti);
            return View(dogadaj);
        }

        // POST: Dogadaj/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SifraDogadaj,DatumVrijeme,Link,SifraRazinaOpasnosti,SifraDionica,Opis")] Dogadaj dogadaj)
        {
            if (id != dogadaj.SifraDogadaj)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dogadaj);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DogadajExists(dogadaj.SifraDogadaj))
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
            ViewData["SifraDionica"] = new SelectList(_context.Dionica, "SifraDionice", "Naziv", dogadaj.SifraDionica);
            ViewData["SifraRazinaOpasnosti"] = new SelectList(_context.RazinaOpasnosti, "SifraRazinaOpasnosti", "NazivRazinaOpasnosti", dogadaj.SifraRazinaOpasnosti);
            return View(dogadaj);
        }

        // GET: Dogadaj/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dogadaj = await _context.Dogadaj
                .Include(d => d.SifraDionicaNavigation)
                .Include(d => d.SifraRazinaOpasnostiNavigation)
                .FirstOrDefaultAsync(m => m.SifraDogadaj == id);
            if (dogadaj == null)
            {
                return NotFound();
            }

            return View(dogadaj);
        }

        // POST: Dogadaj/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dogadaj = await _context.Dogadaj.FindAsync(id);
            _context.Dogadaj.Remove(dogadaj);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DogadajExists(int id)
        {
            return _context.Dogadaj.Any(e => e.SifraDogadaj == id);
        }
    }
}
