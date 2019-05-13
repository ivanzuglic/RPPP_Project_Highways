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
    public class KategorijaVozilaController : Controller
    {
        private readonly RPPP12Context _context;

        public KategorijaVozilaController(RPPP12Context context)
        {
            _context = context;
        }

        // GET: KategorijaVozila
        public async Task<IActionResult> Index()
        {
            return View(await _context.KategorijaVozila.ToListAsync());
        }

        // GET: KategorijaVozila/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kategorijaVozila = await _context.KategorijaVozila
                .FirstOrDefaultAsync(m => m.SifraKategorijaVozila == id);
            if (kategorijaVozila == null)
            {
                return NotFound();
            }

            return View(kategorijaVozila);
        }

        // GET: KategorijaVozila/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: KategorijaVozila/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SifraKategorijaVozila,Oznaka,Opis")] KategorijaVozila kategorijaVozila)
        {
            if (ModelState.IsValid)
            {
                _context.Add(kategorijaVozila);
                await _context.SaveChangesAsync();
                TempData["create"] = "Create";
                return RedirectToAction(nameof(Index));
            }
            return View(kategorijaVozila);
        }

        // GET: KategorijaVozila/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kategorijaVozila = await _context.KategorijaVozila.FindAsync(id);
            if (kategorijaVozila == null)
            {
                return NotFound();
            }
            return View(kategorijaVozila);
        }

        // POST: KategorijaVozila/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SifraKategorijaVozila,Oznaka,Opis")] KategorijaVozila kategorijaVozila)
        {
            if (id != kategorijaVozila.SifraKategorijaVozila)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(kategorijaVozila);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KategorijaVozilaExists(kategorijaVozila.SifraKategorijaVozila))
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
            return View(kategorijaVozila);
        }

        // GET: KategorijaVozila/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kategorijaVozila = await _context.KategorijaVozila
                .FirstOrDefaultAsync(m => m.SifraKategorijaVozila == id);
            if (kategorijaVozila == null)
            {
                return NotFound();
            }

            return View(kategorijaVozila);
        }

        // POST: KategorijaVozila/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var kategorijaVozila = await _context.KategorijaVozila.FindAsync(id);
            _context.KategorijaVozila.Remove(kategorijaVozila);
            await _context.SaveChangesAsync();
            TempData["delete"] = "Delete";
            return RedirectToAction(nameof(Index));
        }

        private bool KategorijaVozilaExists(int id)
        {
            return _context.KategorijaVozila.Any(e => e.SifraKategorijaVozila == id);
        }
    }
}
