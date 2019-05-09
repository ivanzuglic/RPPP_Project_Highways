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
    public class VrstaZaposlenikaController : Controller
    {
        private readonly RPPP12Context _context;

        public VrstaZaposlenikaController(RPPP12Context context)
        {
            _context = context;
        }

        // GET: VrstaZaposlenika
        public async Task<IActionResult> Index()
        {
            return View(await _context.VrstaZaposlenika.ToListAsync());
        }

        // GET: VrstaZaposlenika/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vrstaZaposlenika = await _context.VrstaZaposlenika
                .FirstOrDefaultAsync(m => m.SifraVrsteZaposlenika == id);
            if (vrstaZaposlenika == null)
            {
                return NotFound();
            }

            return View(vrstaZaposlenika);
        }

        // GET: VrstaZaposlenika/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: VrstaZaposlenika/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SifraVrsteZaposlenika,Naziv")] VrstaZaposlenika vrstaZaposlenika)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vrstaZaposlenika);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vrstaZaposlenika);
        }

        // GET: VrstaZaposlenika/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vrstaZaposlenika = await _context.VrstaZaposlenika.FindAsync(id);
            if (vrstaZaposlenika == null)
            {
                return NotFound();
            }
            return View(vrstaZaposlenika);
        }

        // POST: VrstaZaposlenika/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SifraVrsteZaposlenika,Naziv")] VrstaZaposlenika vrstaZaposlenika)
        {
            if (id != vrstaZaposlenika.SifraVrsteZaposlenika)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vrstaZaposlenika);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VrstaZaposlenikaExists(vrstaZaposlenika.SifraVrsteZaposlenika))
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
            return View(vrstaZaposlenika);
        }

        // GET: VrstaZaposlenika/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vrstaZaposlenika = await _context.VrstaZaposlenika
                .FirstOrDefaultAsync(m => m.SifraVrsteZaposlenika == id);
            if (vrstaZaposlenika == null)
            {
                return NotFound();
            }

            return View(vrstaZaposlenika);
        }

        // POST: VrstaZaposlenika/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vrstaZaposlenika = await _context.VrstaZaposlenika.FindAsync(id);
            _context.VrstaZaposlenika.Remove(vrstaZaposlenika);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VrstaZaposlenikaExists(int id)
        {
            return _context.VrstaZaposlenika.Any(e => e.SifraVrsteZaposlenika == id);
        }
    }
}
