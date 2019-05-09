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
    public class VrstaObjektaController : Controller
    {
        private readonly RPPP12Context _context;

        public VrstaObjektaController(RPPP12Context context)
        {
            _context = context;
        }

        // GET: VrstaObjekta
        public async Task<IActionResult> Index()
        {
            return View(await _context.VrstaObjekta.ToListAsync());
        }

        // GET: VrstaObjekta/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vrstaObjekta = await _context.VrstaObjekta
                .FirstOrDefaultAsync(m => m.SifraVrsteObjekta == id);
            if (vrstaObjekta == null)
            {
                return NotFound();
            }

            return View(vrstaObjekta);
        }

        // GET: VrstaObjekta/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: VrstaObjekta/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SifraVrsteObjekta,NazivObjekta")] VrstaObjekta vrstaObjekta)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vrstaObjekta);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vrstaObjekta);
        }

        // GET: VrstaObjekta/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vrstaObjekta = await _context.VrstaObjekta.FindAsync(id);
            if (vrstaObjekta == null)
            {
                return NotFound();
            }
            return View(vrstaObjekta);
        }

        // POST: VrstaObjekta/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SifraVrsteObjekta,NazivObjekta")] VrstaObjekta vrstaObjekta)
        {
            if (id != vrstaObjekta.SifraVrsteObjekta)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vrstaObjekta);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VrstaObjektaExists(vrstaObjekta.SifraVrsteObjekta))
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
            return View(vrstaObjekta);
        }

        // GET: VrstaObjekta/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vrstaObjekta = await _context.VrstaObjekta
                .FirstOrDefaultAsync(m => m.SifraVrsteObjekta == id);
            if (vrstaObjekta == null)
            {
                return NotFound();
            }

            return View(vrstaObjekta);
        }

        // POST: VrstaObjekta/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vrstaObjekta = await _context.VrstaObjekta.FindAsync(id);
            _context.VrstaObjekta.Remove(vrstaObjekta);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VrstaObjektaExists(int id)
        {
            return _context.VrstaObjekta.Any(e => e.SifraVrsteObjekta == id);
        }
    }
}
