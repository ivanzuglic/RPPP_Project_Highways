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
    public class ObjektController : Controller
    {
        private readonly RPPP12Context _context;

        public ObjektController(RPPP12Context context)
        {
            _context = context;
        }

        // GET: Objekt
        public async Task<IActionResult> Index()
        {
            var rPPP12Context = _context.Objekt.Include(o => o.SifraDioniceNavigation).Include(o => o.SifraVrstaObjektaNavigation);
            return View(await rPPP12Context.ToListAsync());
        }

        // GET: Objekt/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var objekt = await _context.Objekt
                .Include(o => o.SifraDioniceNavigation)
                .Include(o => o.SifraVrstaObjektaNavigation)
                .FirstOrDefaultAsync(m => m.SifraObjekta == id);
            if (objekt == null)
            {
                return NotFound();
            }

            return View(objekt);
        }

        // GET: Objekt/Create
        public IActionResult Create()
        {
            ViewData["SifraDionice"] = new SelectList(_context.Dionica, "SifraDionice", "Naziv");
            ViewData["SifraVrstaObjekta"] = new SelectList(_context.VrstaObjekta, "SifraVrsteObjekta", "NazivObjekta");
            return View();
        }

        // POST: Objekt/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SifraObjekta,SifraDionice,SifraVrstaObjekta")] Objekt objekt)
        {
            if (ModelState.IsValid)
            {
                _context.Add(objekt);
                await _context.SaveChangesAsync();
                TempData["create"] = "Create";
                return RedirectToAction(nameof(Index));
            }
            ViewData["SifraDionice"] = new SelectList(_context.Dionica, "SifraDionice", "Naziv", objekt.SifraDionice);
            ViewData["SifraVrstaObjekta"] = new SelectList(_context.VrstaObjekta, "SifraVrsteObjekta", "NazivObjekta", objekt.SifraVrstaObjekta);
            return View(objekt);
        }

        // GET: Objekt/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var objekt = await _context.Objekt.FindAsync(id);
            if (objekt == null)
            {
                return NotFound();
            }
            ViewData["SifraDionice"] = new SelectList(_context.Dionica, "SifraDionice", "Naziv", objekt.SifraDionice);
            ViewData["SifraVrstaObjekta"] = new SelectList(_context.VrstaObjekta, "SifraVrsteObjekta", "NazivObjekta", objekt.SifraVrstaObjekta);
            return View(objekt);
        }

        // POST: Objekt/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SifraObjekta,SifraDionice,SifraVrstaObjekta")] Objekt objekt)
        {
            if (id != objekt.SifraObjekta)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(objekt);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ObjektExists(objekt.SifraObjekta))
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
            ViewData["SifraDionice"] = new SelectList(_context.Dionica, "SifraDionice", "Naziv", objekt.SifraDionice);
            ViewData["SifraVrstaObjekta"] = new SelectList(_context.VrstaObjekta, "SifraVrsteObjekta", "NazivObjekta", objekt.SifraVrstaObjekta);
            return View(objekt);
        }

        // GET: Objekt/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var objekt = await _context.Objekt
                .Include(o => o.SifraDioniceNavigation)
                .Include(o => o.SifraVrstaObjektaNavigation)
                .FirstOrDefaultAsync(m => m.SifraObjekta == id);
            if (objekt == null)
            {
                return NotFound();
            }

            return View(objekt);
        }

        // POST: Objekt/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var objekt = await _context.Objekt.FindAsync(id);
            _context.Objekt.Remove(objekt);
            await _context.SaveChangesAsync();
            TempData["delete"] = "Delete";
            return RedirectToAction(nameof(Index));
        }

        private bool ObjektExists(int id)
        {
            return _context.Objekt.Any(e => e.SifraObjekta == id);
        }
    }
}
