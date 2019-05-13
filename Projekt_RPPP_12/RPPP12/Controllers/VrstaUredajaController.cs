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
    public class VrstaUredajaController : Controller
    {
        private readonly RPPP12Context _context;

        public VrstaUredajaController(RPPP12Context context)
        {
            _context = context;
        }

        // GET: VrstaUredaja
        public async Task<IActionResult> Index()
        {
            return View(await _context.VrstaUredaja.ToListAsync());
        }

        // GET: VrstaUredaja/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vrstaUredaja = await _context.VrstaUredaja
                .FirstOrDefaultAsync(m => m.SifraVrsteUredaja == id);
            if (vrstaUredaja == null)
            {
                return NotFound();
            }

            return View(vrstaUredaja);
        }

        // GET: VrstaUredaja/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: VrstaUredaja/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SifraVrsteUredaja,NazivVrsteUredaja")] VrstaUredaja vrstaUredaja)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vrstaUredaja);
                await _context.SaveChangesAsync();
                TempData["create"] = "Create";
                return RedirectToAction(nameof(Index));
            }
            return View(vrstaUredaja);
        }

        // GET: VrstaUredaja/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vrstaUredaja = await _context.VrstaUredaja.FindAsync(id);
            if (vrstaUredaja == null)
            {
                return NotFound();
            }
            return View(vrstaUredaja);
        }

        // POST: VrstaUredaja/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SifraVrsteUredaja,NazivVrsteUredaja")] VrstaUredaja vrstaUredaja)
        {
            if (id != vrstaUredaja.SifraVrsteUredaja)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vrstaUredaja);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VrstaUredajaExists(vrstaUredaja.SifraVrsteUredaja))
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
            return View(vrstaUredaja);
        }

        // GET: VrstaUredaja/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vrstaUredaja = await _context.VrstaUredaja
                .FirstOrDefaultAsync(m => m.SifraVrsteUredaja == id);
            if (vrstaUredaja == null)
            {
                return NotFound();
            }

            return View(vrstaUredaja);
        }

        // POST: VrstaUredaja/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vrstaUredaja = await _context.VrstaUredaja.FindAsync(id);
            _context.VrstaUredaja.Remove(vrstaUredaja);
            await _context.SaveChangesAsync();
            TempData["delete"] = "Delete";
            return RedirectToAction(nameof(Index));
        }

        private bool VrstaUredajaExists(int id)
        {
            return _context.VrstaUredaja.Any(e => e.SifraVrsteUredaja == id);
        }
    }
}
