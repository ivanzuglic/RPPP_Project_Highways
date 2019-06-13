using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RPPP12.Models;
using Microsoft.Extensions.Options;
using RPPP12.ViewModels;

namespace RPPP12.Controllers
{
    public class UpraviteljController : Controller
    {
        private readonly RPPP12Context _context;
        private readonly AppSettings appData;

        public UpraviteljController(RPPP12Context context, IOptionsSnapshot<AppSettings> options)
        {
            _context = context;
            appData = options.Value;
        }

        // GET: Upravitelj
        //var rPPP12Context = _context.Upravitelj.Include(u => u.SifraSjedistaNavigation);
        //return View(await rPPP12Context.ToListAsync());
        public async Task<IActionResult> Index(int page = 1, int sort = 1, bool ascending = true)
        {
            //var rPPP12Context = _context.NaplatnaKucica.Include(n => n.SifraBlagajnikaNavigation).Include(n => n.SifraPostajaNavigation).Include(n => n.VrstaNaplatneKuciceNavigation);
            //return View(await rPPP12Context.ToListAsync());
            int pagesize = appData.PageSize;

            var query = _context.Upravitelj
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

            System.Linq.Expressions.Expression<Func<Upravitelj, object>> orderSelector = null;
            switch (sort)
            {
                case 1:
                    orderSelector = d => d.Oib;
                    break;
                case 2:
                    orderSelector = d => d.Ime;
                    break;
                case 3:
                    orderSelector = d => d.Email;
                    break;
                case 4:
                    orderSelector = d => d.Telefon;
                    break;
                case 5:
                    orderSelector = d => d.SifraSjedistaNavigation.Adresa;
                    break;
            }
            if (orderSelector != null)
            {
                query = ascending ?
                       query.OrderBy(orderSelector) :
                       query.OrderByDescending(orderSelector);
            }
            var upravitelji = query
                        .Include(u => u.SifraSjedistaNavigation)
                        .Include(u => u.Autocesta)
                        .Skip((page - 1) * pagesize)
                        .Take(pagesize)
                        .ToList();
            var model = new UpraviteljViewModel
            {
                Upravitelji = upravitelji,
                PagingInfo = pagingInfo
            };

            return View(model);
        }


        // GET: Upravitelj/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var upravitelj = await _context.Upravitelj
                .Include(u => u.SifraSjedistaNavigation)
                .Include(u => u.Autocesta)
                .ThenInclude(n => n.SifraNacinaPlacanjaNavigation)
                .Include(u => u.Autocesta)
                .ThenInclude(n => n.SifraPocetkaNavigation)
                .Include(u => u.Autocesta)
                .ThenInclude(n => n.SifraZavrsetkaNavigation)
                .FirstOrDefaultAsync(m => m.SifraUpravitelja == id);
            if (upravitelj == null)
            {
                return NotFound();
            }

            return View(upravitelj);
        }

        // GET: Upravitelj/Create
        public IActionResult Create()
        {
            ViewData["SifraSjedista"] = new SelectList(_context.Sjediste, "SifraSjedista", "Adresa");
            return View();
        }

        // POST: Upravitelj/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SifraUpravitelja,Oib,Ime,SifraSjedista,Email,Telefon")] Upravitelj upravitelj)
        {
            if (ModelState.IsValid)
            {
                _context.Add(upravitelj);
                await _context.SaveChangesAsync();
                TempData["create"] = "Create";
                return RedirectToAction(nameof(Index));
            }
            ViewData["SifraSjedista"] = new SelectList(_context.Sjediste, "SifraSjedista", "Adresa", upravitelj.SifraSjedista);
            return View(upravitelj);
        }

        // GET: Upravitelj/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var upravitelj = await _context.Upravitelj.FindAsync(id);
            if (upravitelj == null)
            {
                return NotFound();
            }
            ViewData["SifraSjedista"] = new SelectList(_context.Sjediste, "SifraSjedista", "Adresa", upravitelj.SifraSjedista);
            return View(upravitelj);
        }

        // POST: Upravitelj/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SifraUpravitelja,Oib,Ime,SifraSjedista,Email,Telefon")] Upravitelj upravitelj)
        {
            if (id != upravitelj.SifraUpravitelja)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(upravitelj);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UpraviteljExists(upravitelj.SifraUpravitelja))
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
            ViewData["SifraSjedista"] = new SelectList(_context.Sjediste, "SifraSjedista", "Adresa", upravitelj.SifraSjedista);
            return View(upravitelj);
        }

        // GET: Upravitelj/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var upravitelj = await _context.Upravitelj
                .Include(u => u.SifraSjedistaNavigation)
                .FirstOrDefaultAsync(m => m.SifraUpravitelja == id);
            if (upravitelj == null)
            {
                return NotFound();
            }

            return View(upravitelj);
        }

        // POST: Upravitelj/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var upravitelj = await _context.Upravitelj.FindAsync(id);
            _context.Upravitelj.Remove(upravitelj);
            await _context.SaveChangesAsync();
            TempData["delete"] = "Delete";
            return RedirectToAction(nameof(Index));
        }

        private bool UpraviteljExists(int id)
        {
            return _context.Upravitelj.Any(e => e.SifraUpravitelja == id);
        }
    }
}
