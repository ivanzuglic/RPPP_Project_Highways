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
    public class NaplatnaPostajaController : Controller
    {
        private readonly RPPP12Context _context;

        public NaplatnaPostajaController(RPPP12Context context)
        {
            _context = context;
        }

        // GET: NaplatnaPostaja
        public async Task<IActionResult> Index()
        {
            var rPPP12Context = _context.NaplatnaPostaja.Include(n => n.SifraDioniceNavigation).Include(n => n.SifraLokacijePostajeNavigation);
            return View(await rPPP12Context.ToListAsync());
        }

        // GET: NaplatnaPostaja/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var naplatnaPostaja = await _context.NaplatnaPostaja
                .Include(n => n.SifraDioniceNavigation)
                .Include(n => n.SifraLokacijePostajeNavigation)
                .FirstOrDefaultAsync(m => m.SifraPostaje == id);
            if (naplatnaPostaja == null)
            {
                return NotFound();
            }

            return View(naplatnaPostaja);
        }

        // GET: NaplatnaPostaja/Create
        public IActionResult Create()
        {
            ViewData["SifraDionice"] = new SelectList(_context.Dionica, "SifraDionice", "Naziv");
            ViewData["SifraLokacijePostaje"] = new SelectList(_context.LokacijaPostaje, "SifraLokacije", "NazivLokacije");
            return View();
        }

        // POST: NaplatnaPostaja/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SifraPostaje,SifraDionice,SifraLokacijePostaje,ImePostaje")] NaplatnaPostaja naplatnaPostaja)
        {
            if (ModelState.IsValid)
            {
                _context.Add(naplatnaPostaja);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SifraDionice"] = new SelectList(_context.Dionica, "SifraDionice", "Naziv", naplatnaPostaja.SifraDionice);
            ViewData["SifraLokacijePostaje"] = new SelectList(_context.LokacijaPostaje, "SifraLokacije", "NazivLokacije", naplatnaPostaja.SifraLokacijePostaje);
            return View(naplatnaPostaja);
        }

        // GET: NaplatnaPostaja/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var naplatnaPostaja = await _context.NaplatnaPostaja.FindAsync(id);
            if (naplatnaPostaja == null)
            {
                return NotFound();
            }
            ViewData["SifraDionice"] = new SelectList(_context.Dionica, "SifraDionice", "Naziv", naplatnaPostaja.SifraDionice);
            ViewData["SifraLokacijePostaje"] = new SelectList(_context.LokacijaPostaje, "SifraLokacije", "NazivLokacije", naplatnaPostaja.SifraLokacijePostaje);
            return View(naplatnaPostaja);
        }

        // POST: NaplatnaPostaja/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SifraPostaje,SifraDionice,SifraLokacijePostaje,ImePostaje")] NaplatnaPostaja naplatnaPostaja)
        {
            if (id != naplatnaPostaja.SifraPostaje)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(naplatnaPostaja);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NaplatnaPostajaExists(naplatnaPostaja.SifraPostaje))
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
            ViewData["SifraDionice"] = new SelectList(_context.Dionica, "SifraDionice", "Naziv", naplatnaPostaja.SifraDionice);
            ViewData["SifraLokacijePostaje"] = new SelectList(_context.LokacijaPostaje, "SifraLokacije", "NazivLokacije", naplatnaPostaja.SifraLokacijePostaje);
            return View(naplatnaPostaja);
        }

        // GET: NaplatnaPostaja/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var naplatnaPostaja = await _context.NaplatnaPostaja
                .Include(n => n.SifraDioniceNavigation)
                .Include(n => n.SifraLokacijePostajeNavigation)
                .FirstOrDefaultAsync(m => m.SifraPostaje == id);
            if (naplatnaPostaja == null)
            {
                return NotFound();
            }

            return View(naplatnaPostaja);
        }

        // POST: NaplatnaPostaja/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var naplatnaPostaja = await _context.NaplatnaPostaja.FindAsync(id);
            _context.NaplatnaPostaja.Remove(naplatnaPostaja);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NaplatnaPostajaExists(int id)
        {
            return _context.NaplatnaPostaja.Any(e => e.SifraPostaje == id);
        }
    }
}
