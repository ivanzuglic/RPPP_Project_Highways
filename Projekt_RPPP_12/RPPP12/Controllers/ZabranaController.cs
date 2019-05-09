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
    public class ZabranaController : Controller
    {
        private readonly RPPP12Context _context;

        public ZabranaController(RPPP12Context context)
        {
            _context = context;
        }

        // GET: Zabrana
        public async Task<IActionResult> Index()
        {
            return View(await _context.Zabrana.ToListAsync());
        }

        // GET: Zabrana/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zabrana = await _context.Zabrana
                .FirstOrDefaultAsync(m => m.SifraZabrana == id);
            if (zabrana == null)
            {
                return NotFound();
            }

            return View(zabrana);
        }

        // GET: Zabrana/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Zabrana/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SifraZabrana,VrstaZabrane")] Zabrana zabrana)
        {
            if (ModelState.IsValid)
            {
                _context.Add(zabrana);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(zabrana);
        }

        // GET: Zabrana/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zabrana = await _context.Zabrana.FindAsync(id);
            if (zabrana == null)
            {
                return NotFound();
            }
            return View(zabrana);
        }

        // POST: Zabrana/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SifraZabrana,VrstaZabrane")] Zabrana zabrana)
        {
            if (id != zabrana.SifraZabrana)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(zabrana);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ZabranaExists(zabrana.SifraZabrana))
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
            return View(zabrana);
        }

        // GET: Zabrana/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zabrana = await _context.Zabrana
                .FirstOrDefaultAsync(m => m.SifraZabrana == id);
            if (zabrana == null)
            {
                return NotFound();
            }

            return View(zabrana);
        }

        // POST: Zabrana/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var zabrana = await _context.Zabrana.FindAsync(id);
            _context.Zabrana.Remove(zabrana);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ZabranaExists(int id)
        {
            return _context.Zabrana.Any(e => e.SifraZabrana == id);
        }
    }
}
