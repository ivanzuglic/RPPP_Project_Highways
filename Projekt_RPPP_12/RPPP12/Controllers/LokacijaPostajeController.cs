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
    public class LokacijaPostajeController : Controller
    {
        private readonly RPPP12Context _context;

        public LokacijaPostajeController(RPPP12Context context)
        {
            _context = context;
        }

        // GET: LokacijaPostaje
        public async Task<IActionResult> Index()
        {
            return View(await _context.LokacijaPostaje.ToListAsync());
        }

        // GET: LokacijaPostaje/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lokacijaPostaje = await _context.LokacijaPostaje
                .FirstOrDefaultAsync(m => m.SifraLokacije == id);
            if (lokacijaPostaje == null)
            {
                return NotFound();
            }

            return View(lokacijaPostaje);
        }

        // GET: LokacijaPostaje/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LokacijaPostaje/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SifraLokacije,NazivLokacije")] LokacijaPostaje lokacijaPostaje)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lokacijaPostaje);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(lokacijaPostaje);
        }

        // GET: LokacijaPostaje/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lokacijaPostaje = await _context.LokacijaPostaje.FindAsync(id);
            if (lokacijaPostaje == null)
            {
                return NotFound();
            }
            return View(lokacijaPostaje);
        }

        // POST: LokacijaPostaje/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SifraLokacije,NazivLokacije")] LokacijaPostaje lokacijaPostaje)
        {
            if (id != lokacijaPostaje.SifraLokacije)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lokacijaPostaje);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LokacijaPostajeExists(lokacijaPostaje.SifraLokacije))
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
            return View(lokacijaPostaje);
        }

        // GET: LokacijaPostaje/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lokacijaPostaje = await _context.LokacijaPostaje
                .FirstOrDefaultAsync(m => m.SifraLokacije == id);
            if (lokacijaPostaje == null)
            {
                return NotFound();
            }

            return View(lokacijaPostaje);
        }

        // POST: LokacijaPostaje/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lokacijaPostaje = await _context.LokacijaPostaje.FindAsync(id);
            _context.LokacijaPostaje.Remove(lokacijaPostaje);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LokacijaPostajeExists(int id)
        {
            return _context.LokacijaPostaje.Any(e => e.SifraLokacije == id);
        }
    }
}
