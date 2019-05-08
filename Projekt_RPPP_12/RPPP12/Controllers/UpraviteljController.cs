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
    public class UpraviteljController : Controller
    {
        private readonly RPPP12Context _context;

        public UpraviteljController(RPPP12Context context)
        {
            _context = context;
        }

        // GET: Upravitelj
        public async Task<IActionResult> Index()
        {
            var rPPP12Context = _context.Upravitelj.Include(u => u.SifraSjedistaNavigation);
            return View(await rPPP12Context.ToListAsync());
        }

        // GET: Upravitelj/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var upravitelj = await _context.Upravitelj
                .Include(u => u.SifraSjedistaNavigation)
                .FirstOrDefaultAsync(m => m.SifraUpravitelja == id);
            if (upravitelj == null)
            {
                return NotFound();
            }

            return View(upravitelj);
        }

        // GET: Upravitelj/Create
        public IActionResult Create()
        {
            ViewData["SifraSjedista"] = new SelectList(_context.Sjediste, "SifraSjedista", "Adresa");
            return View();
        }

        // POST: Upravitelj/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SifraUpravitelja,Oib,Ime,SifraSjedista,Email,Telefon")] Upravitelj upravitelj)
        {
            if (ModelState.IsValid)
            {
                _context.Add(upravitelj);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SifraSjedista"] = new SelectList(_context.Sjediste, "SifraSjedista", "Adresa", upravitelj.SifraSjedista);
            return View(upravitelj);
        }

        // GET: Upravitelj/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var upravitelj = await _context.Upravitelj.FindAsync(id);
            if (upravitelj == null)
            {
                return NotFound();
            }
            ViewData["SifraSjedista"] = new SelectList(_context.Sjediste, "SifraSjedista", "Adresa", upravitelj.SifraSjedista);
            return View(upravitelj);
        }

        // POST: Upravitelj/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SifraUpravitelja,Oib,Ime,SifraSjedista,Email,Telefon")] Upravitelj upravitelj)
        {
            if (id != upravitelj.SifraUpravitelja)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(upravitelj);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UpraviteljExists(upravitelj.SifraUpravitelja))
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
            ViewData["SifraSjedista"] = new SelectList(_context.Sjediste, "SifraSjedista", "Adresa", upravitelj.SifraSjedista);
            return View(upravitelj);
        }

        // GET: Upravitelj/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var upravitelj = await _context.Upravitelj
                .Include(u => u.SifraSjedistaNavigation)
                .FirstOrDefaultAsync(m => m.SifraUpravitelja == id);
            if (upravitelj == null)
            {
                return NotFound();
            }

            return View(upravitelj);
        }

        // POST: Upravitelj/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var upravitelj = await _context.Upravitelj.FindAsync(id);
            _context.Upravitelj.Remove(upravitelj);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UpraviteljExists(int id)
        {
            return _context.Upravitelj.Any(e => e.SifraUpravitelja == id);
        }
    }
}
