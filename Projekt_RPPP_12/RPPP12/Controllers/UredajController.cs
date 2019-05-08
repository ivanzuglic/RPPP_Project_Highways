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
    public class UredajController : Controller
    {
        private readonly RPPP12Context _context;

        public UredajController(RPPP12Context context)
        {
            _context = context;
        }

        // GET: Uredaj
        public async Task<IActionResult> Index()
        {
            var rPPP12Context = _context.Uredaj.Include(u => u.SifraObjektaNavigation).Include(u => u.SifraVrsteUredajaNavigation);
            return View(await rPPP12Context.ToListAsync());
        }

        // GET: Uredaj/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var uredaj = await _context.Uredaj
                .Include(u => u.SifraObjektaNavigation)
                .Include(u => u.SifraVrsteUredajaNavigation)
                .FirstOrDefaultAsync(m => m.SifraUredaja == id);
            if (uredaj == null)
            {
                return NotFound();
            }

            return View(uredaj);
        }

        // GET: Uredaj/Create
        public IActionResult Create()
        {
            ViewData["SifraObjekta"] = new SelectList(_context.Objekt, "SifraObjekta", "SifraObjekta");
            ViewData["SifraVrsteUredaja"] = new SelectList(_context.VrstaUredaja, "SifraVrsteUredaja", "SifraVrsteUredaja");
            return View();
        }

        // POST: Uredaj/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SifraUredaja,Status,SifraObjekta,SifraVrsteUredaja")] Uredaj uredaj)
        {
            if (ModelState.IsValid)
            {
                _context.Add(uredaj);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SifraObjekta"] = new SelectList(_context.Objekt, "SifraObjekta", "SifraObjekta", uredaj.SifraObjekta);
            ViewData["SifraVrsteUredaja"] = new SelectList(_context.VrstaUredaja, "SifraVrsteUredaja", "SifraVrsteUredaja", uredaj.SifraVrsteUredaja);
            return View(uredaj);
        }

        // GET: Uredaj/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var uredaj = await _context.Uredaj.FindAsync(id);
            if (uredaj == null)
            {
                return NotFound();
            }
            ViewData["SifraObjekta"] = new SelectList(_context.Objekt, "SifraObjekta", "SifraObjekta", uredaj.SifraObjekta);
            ViewData["SifraVrsteUredaja"] = new SelectList(_context.VrstaUredaja, "SifraVrsteUredaja", "SifraVrsteUredaja", uredaj.SifraVrsteUredaja);
            return View(uredaj);
        }

        // POST: Uredaj/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SifraUredaja,Status,SifraObjekta,SifraVrsteUredaja")] Uredaj uredaj)
        {
            if (id != uredaj.SifraUredaja)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(uredaj);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UredajExists(uredaj.SifraUredaja))
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
            ViewData["SifraObjekta"] = new SelectList(_context.Objekt, "SifraObjekta", "SifraObjekta", uredaj.SifraObjekta);
            ViewData["SifraVrsteUredaja"] = new SelectList(_context.VrstaUredaja, "SifraVrsteUredaja", "SifraVrsteUredaja", uredaj.SifraVrsteUredaja);
            return View(uredaj);
        }

        // GET: Uredaj/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var uredaj = await _context.Uredaj
                .Include(u => u.SifraObjektaNavigation)
                .Include(u => u.SifraVrsteUredajaNavigation)
                .FirstOrDefaultAsync(m => m.SifraUredaja == id);
            if (uredaj == null)
            {
                return NotFound();
            }

            return View(uredaj);
        }

        // POST: Uredaj/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var uredaj = await _context.Uredaj.FindAsync(id);
            _context.Uredaj.Remove(uredaj);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UredajExists(int id)
        {
            return _context.Uredaj.Any(e => e.SifraUredaja == id);
        }
    }
}
