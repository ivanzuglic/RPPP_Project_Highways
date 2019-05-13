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
    public class LokacijaAutocesteController : Controller
    {
        private readonly RPPP12Context _context;

        public LokacijaAutocesteController(RPPP12Context context)
        {
            _context = context;
        }

        // GET: LokacijaAutoceste
        public async Task<IActionResult> Index()
        {
            return View(await _context.LokacijaAutoceste.ToListAsync());
        }

        // GET: LokacijaAutoceste/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lokacijaAutoceste = await _context.LokacijaAutoceste
                .FirstOrDefaultAsync(m => m.SifraLokacije == id);
            if (lokacijaAutoceste == null)
            {
                return NotFound();
            }

            return View(lokacijaAutoceste);
        }

        // GET: LokacijaAutoceste/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LokacijaAutoceste/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SifraLokacije,ImeLokacije")] LokacijaAutoceste lokacijaAutoceste)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lokacijaAutoceste);
                await _context.SaveChangesAsync();
                TempData["create"] = "Create";
                return RedirectToAction(nameof(Index));
            }
            return View(lokacijaAutoceste);
        }

        // GET: LokacijaAutoceste/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lokacijaAutoceste = await _context.LokacijaAutoceste.FindAsync(id);
            if (lokacijaAutoceste == null)
            {
                return NotFound();
            }
            return View(lokacijaAutoceste);
        }

        // POST: LokacijaAutoceste/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SifraLokacije,ImeLokacije")] LokacijaAutoceste lokacijaAutoceste)
        {
            if (id != lokacijaAutoceste.SifraLokacije)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lokacijaAutoceste);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LokacijaAutocesteExists(lokacijaAutoceste.SifraLokacije))
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
            return View(lokacijaAutoceste);
        }

        // GET: LokacijaAutoceste/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lokacijaAutoceste = await _context.LokacijaAutoceste
                .FirstOrDefaultAsync(m => m.SifraLokacije == id);
            if (lokacijaAutoceste == null)
            {
                return NotFound();
            }

            return View(lokacijaAutoceste);
        }

        // POST: LokacijaAutoceste/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lokacijaAutoceste = await _context.LokacijaAutoceste.FindAsync(id);
            _context.LokacijaAutoceste.Remove(lokacijaAutoceste);
            await _context.SaveChangesAsync();
            TempData["delete"] = "Delete";
            return RedirectToAction(nameof(Index));
        }

        private bool LokacijaAutocesteExists(int id)
        {
            return _context.LokacijaAutoceste.Any(e => e.SifraLokacije == id);
        }
    }
}
