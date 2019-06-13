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
    public class DogadajController : Controller
    {
        private readonly RPPP12Context _context;
        private readonly AppSettings appData;

        public DogadajController(RPPP12Context context, IOptionsSnapshot<AppSettings> options)
        {
            _context = context;
            appData = options.Value;
        }

        // GET: Dogadaj
        public async Task<IActionResult> Index(int page = 1, int sort = 1, bool ascending = true)
        {
            //var rPPP12Context = _context.NaplatnaKucica.Include(n => n.SifraBlagajnikaNavigation).Include(n => n.SifraPostajaNavigation).Include(n => n.VrstaNaplatneKuciceNavigation);
            //return View(await rPPP12Context.ToListAsync());
            int pagesize = appData.PageSize;

            var query = _context.Dogadaj
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

            System.Linq.Expressions.Expression<Func<Dogadaj, object>> orderSelector = null;
            switch (sort)
            {
                case 1:
                    orderSelector = d => d.SifraDionicaNavigation.Naziv;
                    break;
                case 2:
                    orderSelector = d => d.DatumVrijeme;
                    break;
                case 3:
                    orderSelector = d => d.Link;
                    break;
                case 4:
                    orderSelector = d => d.Opis;
                    break;
                case 5:
                    orderSelector = d => d.SifraRazinaOpasnostiNavigation.NazivRazinaOpasnosti;
                    break;
            }
            if (orderSelector != null)
            {
                query = ascending ?
                       query.OrderBy(orderSelector) :
                       query.OrderByDescending(orderSelector);
            }
            var dogadaji = query
                        .Include(d => d.SifraDionicaNavigation)
                        .Include(d => d.SifraRazinaOpasnostiNavigation)
                        .Include(n => n.Stanje)
                        .Skip((page - 1) * pagesize)
                        .Take(pagesize)
                        .ToList();
            var model = new DogadajViewModel
            {
                Dogadaji = dogadaji,
                PagingInfo = pagingInfo
            };

            return View(model);
        }


        // GET: Dogadaj/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dogadaj = await _context.Dogadaj
                .Include(d => d.SifraDionicaNavigation)
                .Include(d => d.SifraRazinaOpasnostiNavigation)
                .Include(n => n.Stanje)
                .FirstOrDefaultAsync(m => m.SifraDogadaj == id);
            if (dogadaj == null)
            {
                return NotFound();
            }

            return View(dogadaj);
        }

        // GET: Dogadaj/Create
        public IActionResult Create()
        {
            ViewData["SifraDionica"] = new SelectList(_context.Dionica, "SifraDionice", "Naziv");
            ViewData["SifraRazinaOpasnosti"] = new SelectList(_context.RazinaOpasnosti, "SifraRazinaOpasnosti", "NazivRazinaOpasnosti");
            return View();
        }

        // POST: Dogadaj/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SifraDogadaj,DatumVrijeme,Link,SifraRazinaOpasnosti,SifraDionica,Opis")] Dogadaj dogadaj)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dogadaj);
                await _context.SaveChangesAsync();
                TempData["create"] = "Create";
                return RedirectToAction(nameof(Index));
            }
            ViewData["SifraDionica"] = new SelectList(_context.Dionica, "SifraDionice", "Naziv", dogadaj.SifraDionica);
            ViewData["SifraRazinaOpasnosti"] = new SelectList(_context.RazinaOpasnosti, "SifraRazinaOpasnosti", "NazivRazinaOpasnosti", dogadaj.SifraRazinaOpasnosti);
            return View(dogadaj);
        }

        // GET: Dogadaj/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dogadaj = await _context.Dogadaj.FindAsync(id);
            if (dogadaj == null)
            {
                return NotFound();
            }
            ViewData["SifraDionica"] = new SelectList(_context.Dionica, "SifraDionice", "Naziv", dogadaj.SifraDionica);
            ViewData["SifraRazinaOpasnosti"] = new SelectList(_context.RazinaOpasnosti, "SifraRazinaOpasnosti", "NazivRazinaOpasnosti", dogadaj.SifraRazinaOpasnosti);
            return View(dogadaj);
        }

        // POST: Dogadaj/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SifraDogadaj,DatumVrijeme,Link,SifraRazinaOpasnosti,SifraDionica,Opis")] Dogadaj dogadaj)
        {
            if (id != dogadaj.SifraDogadaj)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dogadaj);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DogadajExists(dogadaj.SifraDogadaj))
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
            ViewData["SifraDionica"] = new SelectList(_context.Dionica, "SifraDionice", "Naziv", dogadaj.SifraDionica);
            ViewData["SifraRazinaOpasnosti"] = new SelectList(_context.RazinaOpasnosti, "SifraRazinaOpasnosti", "NazivRazinaOpasnosti", dogadaj.SifraRazinaOpasnosti);
            return View(dogadaj);
        }

        // GET: Dogadaj/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dogadaj = await _context.Dogadaj
                .Include(d => d.SifraDionicaNavigation)
                .Include(d => d.SifraRazinaOpasnostiNavigation)
                .FirstOrDefaultAsync(m => m.SifraDogadaj == id);
            if (dogadaj == null)
            {
                return NotFound();
            }

            return View(dogadaj);
        }

        // POST: Dogadaj/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dogadaj = await _context.Dogadaj.FindAsync(id);
            _context.Dogadaj.Remove(dogadaj);
            await _context.SaveChangesAsync();
            TempData["delete"] = "Delete";
            return RedirectToAction(nameof(Index));
        }

        private bool DogadajExists(int id)
        {
            return _context.Dogadaj.Any(e => e.SifraDogadaj == id);
        }
    }
}
