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
    public class SjedisteController : Controller
    {
        private readonly RPPP12Context _context;

        public SjedisteController(RPPP12Context context)
        {
            _context = context;
        }

        // GET: Sjediste
        public async Task<IActionResult> Index()
        {
            return View(await _context.Sjediste.ToListAsync());
        }

        // GET: Sjediste/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sjediste = await _context.Sjediste
                .FirstOrDefaultAsync(m => m.SifraSjedista == id);
            if (sjediste == null)
            {
                return NotFound();
            }

            return View(sjediste);
        }

        // GET: Sjediste/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Sjediste/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SifraSjedista,ImeSjedista,Adresa")] Sjediste sjediste)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sjediste);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sjediste);
        }

        // GET: Sjediste/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sjediste = await _context.Sjediste.FindAsync(id);
            if (sjediste == null)
            {
                return NotFound();
            }
            return View(sjediste);
        }

        // POST: Sjediste/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SifraSjedista,ImeSjedista,Adresa")] Sjediste sjediste)
        {
            if (id != sjediste.SifraSjedista)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sjediste);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SjedisteExists(sjediste.SifraSjedista))
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
            return View(sjediste);
        }

        // GET: Sjediste/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sjediste = await _context.Sjediste
                .FirstOrDefaultAsync(m => m.SifraSjedista == id);
            if (sjediste == null)
            {
                return NotFound();
            }

            return View(sjediste);
        }

        // POST: Sjediste/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sjediste = await _context.Sjediste.FindAsync(id);
            _context.Sjediste.Remove(sjediste);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SjedisteExists(int id)
        {
            return _context.Sjediste.Any(e => e.SifraSjedista == id);
        }
    }
}
