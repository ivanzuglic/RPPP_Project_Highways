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
    public class UredajController : Controller
    {
        private readonly RPPP12Context _context;
        private readonly AppSettings appData;
        

        public UredajController(RPPP12Context context, IOptionsSnapshot<AppSettings> options)
        {
            _context = context;
            appData = options.Value;
        }

        // GET: Uredaj/pagination-2
        //or
        //GET: Uredaj/all
        public IActionResult Index(int page = 1, int sort = 1, bool ascending = true)
        {
            int pagesize = appData.PageSize;

            var query = _context.Uredaj
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

            System.Linq.Expressions.Expression<Func<Uredaj, object>> orderSelector = null;
            switch (sort)
            {
                case 1:
                    orderSelector = d => d.SifraUredaja;
                    break;
                //sortiranje po drugim parametrima
                /*case 2:
                    orderSelector = d => d.NazDrzave;
                    break;
                case 3:
                    orderSelector = d => d.Iso3drzave;
                    break;
                case 4:
                    orderSelector = d => d.SifDrzave;
                    break; */
            }
            if (orderSelector != null)
            {
                query = ascending ?
                       query.OrderBy(orderSelector) :
                       query.OrderByDescending(orderSelector);
            }
            var uredaji = query
                        .Include(u => u.SifraObjektaNavigation)
                        .Include(u => u.SifraVrsteUredajaNavigation)
                        .Skip((page - 1) * pagesize)
                        .Take(pagesize)
                        .ToList();
            var model = new UredajViewModel
            {
                Uredaji = uredaji,
                PagingInfo = pagingInfo
            };

            return View(model);
        }
        
        // GET: Uredaj/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var uredaj = await _context.Uredaj
                .Include(u => u.SifraObjektaNavigation)
                .Include(u => u.SifraVrsteUredajaNavigation)
                .FirstOrDefaultAsync(m => m.SifraUredaja == id);
            if (uredaj == null)
            {
                return NotFound();
            }

            return View(uredaj);
        }

        // GET: Uredaj/Create
        public IActionResult Create()
        {
            ViewData["SifraObjekta"] = new SelectList(_context.Objekt, "SifraObjekta", "SifraObjekta");
            ViewData["SifraVrsteUredaja"] = new SelectList(_context.VrstaUredaja, "SifraVrsteUredaja", "SifraVrsteUredaja");
            return View();
        }

        // POST: Uredaj/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SifraUredaja,Status,SifraObjekta,SifraVrsteUredaja")] Uredaj uredaj)
        {
            if (ModelState.IsValid)
            {
                _context.Add(uredaj);
                await _context.SaveChangesAsync();
                TempData["create"] = "Create";
                return RedirectToAction(nameof(Index));
            }
            ViewData["SifraObjekta"] = new SelectList(_context.Objekt, "SifraObjekta", "SifraObjekta", uredaj.SifraObjekta);
            ViewData["SifraVrsteUredaja"] = new SelectList(_context.VrstaUredaja, "SifraVrsteUredaja", "SifraVrsteUredaja", uredaj.SifraVrsteUredaja);
            return View(uredaj);
        }

        // GET: Uredaj/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var uredaj = await _context.Uredaj.FindAsync(id);
            if (uredaj == null)
            {
                return NotFound();
            }
            ViewData["SifraObjekta"] = new SelectList(_context.Objekt, "SifraObjekta", "SifraObjekta", uredaj.SifraObjekta);
            ViewData["SifraVrsteUredaja"] = new SelectList(_context.VrstaUredaja, "SifraVrsteUredaja", "SifraVrsteUredaja", uredaj.SifraVrsteUredaja);
            return View(uredaj);
        }

        // POST: Uredaj/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SifraUredaja,Status,SifraObjekta,SifraVrsteUredaja")] Uredaj uredaj)
        {
            if (id != uredaj.SifraUredaja)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(uredaj);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UredajExists(uredaj.SifraUredaja))
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
            ViewData["SifraObjekta"] = new SelectList(_context.Objekt, "SifraObjekta", "SifraObjekta", uredaj.SifraObjekta);
            ViewData["SifraVrsteUredaja"] = new SelectList(_context.VrstaUredaja, "SifraVrsteUredaja", "SifraVrsteUredaja", uredaj.SifraVrsteUredaja);
            return View(uredaj);
        }

        // GET: Uredaj/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var uredaj = await _context.Uredaj
                .Include(u => u.SifraObjektaNavigation)
                .Include(u => u.SifraVrsteUredajaNavigation)
                .FirstOrDefaultAsync(m => m.SifraUredaja == id);
            if (uredaj == null)
            {
                return NotFound();
            }

            return View(uredaj);
        }

        // POST: Uredaj/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var uredaj = await _context.Uredaj.FindAsync(id);
            _context.Uredaj.Remove(uredaj);
            await _context.SaveChangesAsync();
            TempData["delete"] = "Delete";
            return RedirectToAction(nameof(Index));
        }

        private bool UredajExists(int id)
        {
            return _context.Uredaj.Any(e => e.SifraUredaja == id);
        }
    }
}
