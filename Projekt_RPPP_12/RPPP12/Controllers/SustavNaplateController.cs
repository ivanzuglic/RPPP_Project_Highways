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
    public class SustavNaplateController : Controller
    {
        private readonly RPPP12Context _context;

        public SustavNaplateController(RPPP12Context context)
        {
            _context = context;
        }

        // GET: SustavNaplate
        public async Task<IActionResult> Index()
        {
            return View(await _context.SustavNaplate.ToListAsync());
        }

        // GET: SustavNaplate/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sustavNaplate = await _context.SustavNaplate
                .FirstOrDefaultAsync(m => m.SifraNacinaPlacanja == id);
            if (sustavNaplate == null)
            {
                return NotFound();
            }

            return View(sustavNaplate);
        }

        // GET: SustavNaplate/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SustavNaplate/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SifraNacinaPlacanja,NacinPlacanja")] SustavNaplate sustavNaplate)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sustavNaplate);
                await _context.SaveChangesAsync();
                TempData["create"] = "Create";
                return RedirectToAction(nameof(Index));
            }
            return View(sustavNaplate);
        }

        // GET: SustavNaplate/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sustavNaplate = await _context.SustavNaplate.FindAsync(id);
            if (sustavNaplate == null)
            {
                return NotFound();
            }
            return View(sustavNaplate);
        }

        // POST: SustavNaplate/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SifraNacinaPlacanja,NacinPlacanja")] SustavNaplate sustavNaplate)
        {
            if (id != sustavNaplate.SifraNacinaPlacanja)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sustavNaplate);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SustavNaplateExists(sustavNaplate.SifraNacinaPlacanja))
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
            return View(sustavNaplate);
        }

        // GET: SustavNaplate/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sustavNaplate = await _context.SustavNaplate
                .FirstOrDefaultAsync(m => m.SifraNacinaPlacanja == id);
            if (sustavNaplate == null)
            {
                return NotFound();
            }

            return View(sustavNaplate);
        }

        // POST: SustavNaplate/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sustavNaplate = await _context.SustavNaplate.FindAsync(id);
            _context.SustavNaplate.Remove(sustavNaplate);
            await _context.SaveChangesAsync();
            TempData["delete"] = "Delete";
            return RedirectToAction(nameof(Index));
        }

        private bool SustavNaplateExists(int id)
        {
            return _context.SustavNaplate.Any(e => e.SifraNacinaPlacanja == id);
        }
    }
}
