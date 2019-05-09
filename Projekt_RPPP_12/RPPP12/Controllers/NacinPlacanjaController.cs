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
    public class NacinPlacanjaController : Controller
    {
        private readonly RPPP12Context _context;

        public NacinPlacanjaController(RPPP12Context context)
        {
            _context = context;
        }

        // GET: NacinPlacanja
        public async Task<IActionResult> Index()
        {
            return View(await _context.NacinPlacanja.ToListAsync());
        }

        // GET: NacinPlacanja/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nacinPlacanja = await _context.NacinPlacanja
                .FirstOrDefaultAsync(m => m.SifraNacinPlacanja == id);
            if (nacinPlacanja == null)
            {
                return NotFound();
            }

            return View(nacinPlacanja);
        }

        // GET: NacinPlacanja/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: NacinPlacanja/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SifraNacinPlacanja,NacinPlacanja1")] NacinPlacanja nacinPlacanja)
        {
            if (ModelState.IsValid)
            {
                _context.Add(nacinPlacanja);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(nacinPlacanja);
        }

        // GET: NacinPlacanja/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nacinPlacanja = await _context.NacinPlacanja.FindAsync(id);
            if (nacinPlacanja == null)
            {
                return NotFound();
            }
            return View(nacinPlacanja);
        }

        // POST: NacinPlacanja/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SifraNacinPlacanja,NacinPlacanja1")] NacinPlacanja nacinPlacanja)
        {
            if (id != nacinPlacanja.SifraNacinPlacanja)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(nacinPlacanja);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NacinPlacanjaExists(nacinPlacanja.SifraNacinPlacanja))
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
            return View(nacinPlacanja);
        }

        // GET: NacinPlacanja/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nacinPlacanja = await _context.NacinPlacanja
                .FirstOrDefaultAsync(m => m.SifraNacinPlacanja == id);
            if (nacinPlacanja == null)
            {
                return NotFound();
            }

            return View(nacinPlacanja);
        }

        // POST: NacinPlacanja/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var nacinPlacanja = await _context.NacinPlacanja.FindAsync(id);
            _context.NacinPlacanja.Remove(nacinPlacanja);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NacinPlacanjaExists(int id)
        {
            return _context.NacinPlacanja.Any(e => e.SifraNacinPlacanja == id);
        }
    }
}
