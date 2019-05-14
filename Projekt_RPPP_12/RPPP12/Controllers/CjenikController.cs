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
    public class CjenikController : Controller
    {
        private readonly RPPP12Context _context;

        public CjenikController(RPPP12Context context)
        {
            _context = context;
        }

        // GET: Cjenik
        public async Task<IActionResult> Index()
        {
            var rPPP12Context = _context.Cjenik.Include(c => c.SifraKategorijaVozilaNavigation).Include(c => c.SifraKucicaNavigation);
            return View(await rPPP12Context.ToListAsync());
        }

        // GET: Cjenik/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cjenik = await _context.Cjenik
                .Include(c => c.SifraKategorijaVozilaNavigation)
                .Include(c => c.SifraKucicaNavigation)
                .FirstOrDefaultAsync(m => m.SifraKucica == id);
            if (cjenik == null)
            {
                return NotFound();
            }

            return View(cjenik);
        }

        // GET: Cjenik/Create
        public IActionResult Create()
        {
            ViewData["SifraKategorijaVozila"] = new SelectList(_context.KategorijaVozila, "SifraKategorijaVozila", "Opis");
            ViewData["SifraKucica"] = new SelectList(_context.NaplatnaKucica, "SifraKucica", "SifraKucica");
            return View();
        }

        // POST: Cjenik/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SifraKucica,SifraKategorijaVozila,Cijena")] Cjenik cjenik)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cjenik);
                await _context.SaveChangesAsync();
                TempData["create"] = "Create";
                return RedirectToAction(nameof(Index));
            }
            ViewData["SifraKategorijaVozila"] = new SelectList(_context.KategorijaVozila, "SifraKategorijaVozila", "Opis", cjenik.SifraKategorijaVozila);
            ViewData["SifraKucica"] = new SelectList(_context.NaplatnaKucica, "SifraKucica", "SifraKucica", cjenik.SifraKucica);
            return View(cjenik);
        }

        // GET: Cjenik/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cjenik = await _context.Cjenik.FindAsync(id);
            if (cjenik == null)
            {
                return NotFound();
            }
            ViewData["SifraKategorijaVozila"] = new SelectList(_context.KategorijaVozila, "SifraKategorijaVozila", "Opis", cjenik.SifraKategorijaVozila);
            ViewData["SifraKucica"] = new SelectList(_context.NaplatnaKucica, "SifraKucica", "SifraKucica", cjenik.SifraKucica);
            return View(cjenik);
        }

        // POST: Cjenik/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SifraKucica,SifraKategorijaVozila,Cijena")] Cjenik cjenik)
        {
            if (id != cjenik.SifraKucica)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cjenik);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CjenikExists(cjenik.SifraKucica, cjenik.SifraKategorijaVozila))
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
            ViewData["SifraKategorijaVozila"] = new SelectList(_context.KategorijaVozila, "SifraKategorijaVozila", "Opis", cjenik.SifraKategorijaVozila);
            ViewData["SifraKucica"] = new SelectList(_context.NaplatnaKucica, "SifraKucica", "SifraKucica", cjenik.SifraKucica);
            return View(cjenik);
        }

        // GET: Cjenik/Delete/5
        public async Task<IActionResult> Delete(int? id1, int? id2)
        {
            if (id1 == null || id2==null)
            {
                return NotFound();
            }

            var cjenik = await _context.Cjenik
                .Include(c => c.SifraKategorijaVozilaNavigation)
                .Include(c => c.SifraKucicaNavigation)
                .FirstOrDefaultAsync(m => m.SifraKucica == id1 && m.SifraKategorijaVozila==id2);
            if (cjenik == null)
            {
                return NotFound();
            }

            return View(cjenik);
        }

        // POST: Cjenik/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id1, int? id2)
        {
            var cjenik = await _context.Cjenik.FindAsync(id1, id2);
            _context.Cjenik.Remove(cjenik);
            await _context.SaveChangesAsync();
            TempData["delete"] = "Delete";
            return RedirectToAction(nameof(Index));
        }

        private bool CjenikExists(int id1, int id2)
        {
            return _context.Cjenik.Any(e => e.SifraKucica == id1 && e.SifraKategorijaVozila == id2);
        }
    }
}
