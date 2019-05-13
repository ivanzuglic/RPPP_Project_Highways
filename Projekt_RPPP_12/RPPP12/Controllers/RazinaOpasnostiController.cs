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
    public class RazinaOpasnostiController : Controller
    {
        private readonly RPPP12Context _context;

        public RazinaOpasnostiController(RPPP12Context context)
        {
            _context = context;
        }

        // GET: RazinaOpasnosti
        public async Task<IActionResult> Index()
        {
            return View(await _context.RazinaOpasnosti.ToListAsync());
        }

        // GET: RazinaOpasnosti/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var razinaOpasnosti = await _context.RazinaOpasnosti
                .FirstOrDefaultAsync(m => m.SifraRazinaOpasnosti == id);
            if (razinaOpasnosti == null)
            {
                return NotFound();
            }

            return View(razinaOpasnosti);
        }

        // GET: RazinaOpasnosti/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: RazinaOpasnosti/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SifraRazinaOpasnosti,NazivRazinaOpasnosti")] RazinaOpasnosti razinaOpasnosti)
        {
            if (ModelState.IsValid)
            {
                _context.Add(razinaOpasnosti);
                await _context.SaveChangesAsync();
                TempData["create"] = "Create";
                return RedirectToAction(nameof(Index));
            }
            return View(razinaOpasnosti);
        }

        // GET: RazinaOpasnosti/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var razinaOpasnosti = await _context.RazinaOpasnosti.FindAsync(id);
            if (razinaOpasnosti == null)
            {
                return NotFound();
            }
            return View(razinaOpasnosti);
        }

        // POST: RazinaOpasnosti/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SifraRazinaOpasnosti,NazivRazinaOpasnosti")] RazinaOpasnosti razinaOpasnosti)
        {
            if (id != razinaOpasnosti.SifraRazinaOpasnosti)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(razinaOpasnosti);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RazinaOpasnostiExists(razinaOpasnosti.SifraRazinaOpasnosti))
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
            return View(razinaOpasnosti);
        }

        // GET: RazinaOpasnosti/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var razinaOpasnosti = await _context.RazinaOpasnosti
                .FirstOrDefaultAsync(m => m.SifraRazinaOpasnosti == id);
            if (razinaOpasnosti == null)
            {
                return NotFound();
            }

            return View(razinaOpasnosti);
        }

        // POST: RazinaOpasnosti/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var razinaOpasnosti = await _context.RazinaOpasnosti.FindAsync(id);
            _context.RazinaOpasnosti.Remove(razinaOpasnosti);
            await _context.SaveChangesAsync();
            TempData["delete"] = "Delete";
            return RedirectToAction(nameof(Index));
        }

        private bool RazinaOpasnostiExists(int id)
        {
            return _context.RazinaOpasnosti.Any(e => e.SifraRazinaOpasnosti == id);
        }
    }
}
