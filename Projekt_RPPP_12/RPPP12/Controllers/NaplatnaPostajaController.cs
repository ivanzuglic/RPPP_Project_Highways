using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RPPP12.Models;
using RPPP12.ViewModels;

namespace RPPP12.Controllers
{
    public class NaplatnaPostajaController : Controller
    {
        private readonly RPPP12Context _context;
        private readonly AppSettings appData;

        public NaplatnaPostajaController(RPPP12Context context, IOptionsSnapshot<AppSettings> options)
        {
            _context = context;
            appData = options.Value;
        }

        // GET: NaplatnaPostaja
        public async Task<IActionResult> Index(int page = 1, int sort = 1, bool ascending = true) 
        {
            //var rPPP12Context = _context.NaplatnaPostaja.Include(n => n.SifraDioniceNavigation).Include(n => n.SifraLokacijePostajeNavigation).Include(n => n.Zaposlenik);
            //return View(await rPPP12Context.ToListAsync());
            //var rPPP12Context = _context.Dionica.Include(d => d.SifraAutocesteNavigation).Include(d => d.SifraKrajaNavigation).Include(d => d.SifraPocetkaNavigation);
            //return View(await rPPP12Context.ToListAsync());
            int pagesize = appData.PageSize;

            var query = _context.NaplatnaPostaja
                        .AsNoTracking();

            int count = query.Count();
            if (count == 0)
            {
                return RedirectToAction(nameof(Create));
            }

            var pagingInfo = new PagingInfo
            {
                CurrentPage = page,
                Sort = sort,
                Ascending = ascending,
                ItemsPerPage = pagesize,
                TotalItems = count
            };
            if (page > pagingInfo.TotalPages)
            {
                return RedirectToAction(nameof(Index), new { page = pagingInfo.TotalPages, sort, ascending });
            }

            System.Linq.Expressions.Expression<Func<NaplatnaPostaja, object>> orderSelector = null;
            switch (sort)
            {
                case 1:
                    orderSelector = d => d.ImePostaje;
                    break;
                case 2:
                    orderSelector = d => d.SifraDioniceNavigation.Naziv;
                    break;
                case 3:
                    orderSelector = d => d.SifraLokacijePostajeNavigation.NazivLokacije;
                    break;
            }
            if (orderSelector != null)
            {
                query = ascending ?
                       query.OrderBy(orderSelector) :
                       query.OrderByDescending(orderSelector);
            }
            var naplatnePostaje = query
                        .Include(n => n.SifraDioniceNavigation)
                        .Include(n => n.SifraLokacijePostajeNavigation)
                        .Include(n => n.Zaposlenik)
                        .Skip((page - 1) * pagesize)
                        .Take(pagesize)
                        .ToList();
            var model = new NaplatnaPostajaViewModel
            {
                NaplatnePostaje = naplatnePostaje,
                PagingInfo = pagingInfo
            };

            return View(model);
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
                .Include(n => n.Zaposlenik)
                .ThenInclude(z => z.SifraVrsteZaposlenikaNavigation)
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
                TempData["create"] = "Create";
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
            TempData["delete"] = "Delete";
            return RedirectToAction(nameof(Index));
        }

        private bool NaplatnaPostajaExists(int id)
        {
            return _context.NaplatnaPostaja.Any(e => e.SifraPostaje == id);
        }
    }
}
