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
    public class DionicaController : Controller
    {
        private readonly RPPP12Context _context;
    //  private readonly AppSettings appData;

        public DionicaController(RPPP12Context context/*, IOptionsSnapshot<AppSettings> options*/)
        {
            _context = context;
      //    appData = options.Value;
        }

        /*public IActionResult Index()
        { 
        var dionice = _context.Dionica
        .AsNoTracking()
        .OrderBy(d => d.Naziv)
        .ToList();
        return View("IndexSimple", dionice);
        }*/


        // GET: Dionica
        public async Task<IActionResult> Index()
        {
            var rPPP12Context = _context.Dionica.Include(d => d.SifraAutocesteNavigation).Include(d => d.SifraKrajaNavigation).Include(d => d.SifraPocetkaNavigation);
            return View(await rPPP12Context.ToListAsync());
        }

        // GET: Dionica/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dionica = await _context.Dionica
                .Include(d => d.SifraAutocesteNavigation)
                .Include(d => d.SifraKrajaNavigation)
                .Include(d => d.SifraPocetkaNavigation)
                .FirstOrDefaultAsync(m => m.SifraDionice == id);
            if (dionica == null)
            {
                return NotFound();
            }

            return View(dionica);
        }

        // GET: Dionica/Create
        public IActionResult Create()
        {
            ViewData["SifraAutoceste"] = new SelectList(_context.Autocesta, "SifraAutoceste", "ImeAutoceste");
            ViewData["SifraKraja"] = new SelectList(_context.LokacijaPostaje, "SifraLokacije", "NazivLokacije");
            ViewData["SifraPocetka"] = new SelectList(_context.LokacijaPostaje, "SifraLokacije", "NazivLokacije");
            return View();
        }

        // POST: Dionica/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SifraDionice,Naziv,SifraPocetka,SifraKraja,SifraAutoceste,Duljina")] Dionica dionica)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dionica);
                await _context.SaveChangesAsync();
                TempData["create"] = "Create";
                return RedirectToAction(nameof(Index));
            }
            ViewData["SifraAutoceste"] = new SelectList(_context.Autocesta, "SifraAutoceste", "ImeAutoceste", dionica.SifraAutoceste);
            ViewData["SifraKraja"] = new SelectList(_context.LokacijaPostaje, "SifraLokacije", "NazivLokacije", dionica.SifraKraja);
            ViewData["SifraPocetka"] = new SelectList(_context.LokacijaPostaje, "SifraLokacije", "NazivLokacije", dionica.SifraPocetka);
            return View(dionica);
        }

        // GET: Dionica/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dionica = await _context.Dionica.FindAsync(id);
            if (dionica == null)
            {
                return NotFound();
            }
            ViewData["SifraAutoceste"] = new SelectList(_context.Autocesta, "SifraAutoceste", "ImeAutoceste", dionica.SifraAutoceste);
            ViewData["SifraKraja"] = new SelectList(_context.LokacijaPostaje, "SifraLokacije", "NazivLokacije", dionica.SifraKraja);
            ViewData["SifraPocetka"] = new SelectList(_context.LokacijaPostaje, "SifraLokacije", "NazivLokacije", dionica.SifraPocetka);
            return View(dionica);
        }

        // POST: Dionica/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SifraDionice,Naziv,SifraPocetka,SifraKraja,SifraAutoceste,Duljina")] Dionica dionica)
        {
            if (id != dionica.SifraDionice)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dionica);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DionicaExists(dionica.SifraDionice))
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
            ViewData["SifraAutoceste"] = new SelectList(_context.Autocesta, "SifraAutoceste", "ImeAutoceste", dionica.SifraAutoceste);
            ViewData["SifraKraja"] = new SelectList(_context.LokacijaPostaje, "SifraLokacije", "NazivLokacije", dionica.SifraKraja);
            ViewData["SifraPocetka"] = new SelectList(_context.LokacijaPostaje, "SifraLokacije", "NazivLokacije", dionica.SifraPocetka);
            return View(dionica);
        }

        // GET: Dionica/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dionica = await _context.Dionica
                .Include(d => d.SifraAutocesteNavigation)
                .Include(d => d.SifraKrajaNavigation)
                .Include(d => d.SifraPocetkaNavigation)
                .FirstOrDefaultAsync(m => m.SifraDionice == id);
            if (dionica == null)
            {
                return NotFound();
            }

            return View(dionica);
        }

        // POST: Dionica/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dionica = await _context.Dionica.FindAsync(id);
            _context.Dionica.Remove(dionica);
            await _context.SaveChangesAsync();
            TempData["delete"] = "Delete";
            return RedirectToAction(nameof(Index));
        }

        private bool DionicaExists(int id)
        {
            return _context.Dionica.Any(e => e.SifraDionice == id);
        }
    }
}
