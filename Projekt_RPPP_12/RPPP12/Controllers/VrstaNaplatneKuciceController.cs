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
    public class VrstaNaplatneKuciceController : Controller
    {
        private readonly RPPP12Context _context;

        public VrstaNaplatneKuciceController(RPPP12Context context)
        {
            _context = context;
        }

        // GET: VrstaNaplatneKucice
        public async Task<IActionResult> Index()
        {
            return View(await _context.VrstaNaplatneKucice.ToListAsync());
        }

        // GET: VrstaNaplatneKucice/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vrstaNaplatneKucice = await _context.VrstaNaplatneKucice
                .FirstOrDefaultAsync(m => m.VrstaNaplatneKucice1 == id);
            if (vrstaNaplatneKucice == null)
            {
                return NotFound();
            }

            return View(vrstaNaplatneKucice);
        }

        // GET: VrstaNaplatneKucice/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: VrstaNaplatneKucice/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VrstaNaplatneKucice1")] VrstaNaplatneKucice vrstaNaplatneKucice)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vrstaNaplatneKucice);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vrstaNaplatneKucice);
        }

        // GET: VrstaNaplatneKucice/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vrstaNaplatneKucice = await _context.VrstaNaplatneKucice.FindAsync(id);
            if (vrstaNaplatneKucice == null)
            {
                return NotFound();
            }
            return View(vrstaNaplatneKucice);
        }

        // POST: VrstaNaplatneKucice/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("VrstaNaplatneKucice1")] VrstaNaplatneKucice vrstaNaplatneKucice)
        {
            if (id != vrstaNaplatneKucice.VrstaNaplatneKucice1)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vrstaNaplatneKucice);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VrstaNaplatneKuciceExists(vrstaNaplatneKucice.VrstaNaplatneKucice1))
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
            return View(vrstaNaplatneKucice);
        }

        // GET: VrstaNaplatneKucice/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vrstaNaplatneKucice = await _context.VrstaNaplatneKucice
                .FirstOrDefaultAsync(m => m.VrstaNaplatneKucice1 == id);
            if (vrstaNaplatneKucice == null)
            {
                return NotFound();
            }

            return View(vrstaNaplatneKucice);
        }

        // POST: VrstaNaplatneKucice/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var vrstaNaplatneKucice = await _context.VrstaNaplatneKucice.FindAsync(id);
            _context.VrstaNaplatneKucice.Remove(vrstaNaplatneKucice);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VrstaNaplatneKuciceExists(string id)
        {
            return _context.VrstaNaplatneKucice.Any(e => e.VrstaNaplatneKucice1 == id);
        }
    }
}
