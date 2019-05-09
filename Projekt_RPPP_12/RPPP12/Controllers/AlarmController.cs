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
    public class AlarmController : Controller
    {
        private readonly RPPP12Context _context;

        public AlarmController(RPPP12Context context)
        {
            _context = context;
        }

        // GET: Alarms
        public async Task<IActionResult> Index()
        {
            var rPPP12Context = _context.Alarm.Include(a => a.SifraDogadajaNavigation).Include(a => a.SifraScenarijaNavigation).Include(a => a.SifraUredajaNavigation);
            return View(await rPPP12Context.ToListAsync());
        }

        // GET: Alarms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alarm = await _context.Alarm
                .Include(a => a.SifraDogadajaNavigation)
                .Include(a => a.SifraScenarijaNavigation)
                .Include(a => a.SifraUredajaNavigation)
                .FirstOrDefaultAsync(m => m.SifraUredaja == id);
            if (alarm == null)
            {
                return NotFound();
            }

            return View(alarm);
        }

        // GET: Alarms/Create
        public IActionResult Create()
        {
            ViewData["SifraDogadaja"] = new SelectList(_context.Dogadaj, "SifraDogadaj", "SifraDogadaj");
            ViewData["SifraScenarija"] = new SelectList(_context.Scenarij, "SifraScenarija", "NazivScenarija");
            ViewData["SifraUredaja"] = new SelectList(_context.Uredaj, "SifraUredaja", "SifraUredaja");
            return View();
        }

        // POST: Alarms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SifraUredaja,SifraDogadaja,SifraScenarija,SifraOperatera")] Alarm alarm)
        {
            if (ModelState.IsValid)
            {
                _context.Add(alarm);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SifraDogadaja"] = new SelectList(_context.Dogadaj, "SifraDogadaj", "SifraDogadaj", alarm.SifraDogadaja);
            ViewData["SifraScenarija"] = new SelectList(_context.Scenarij, "SifraScenarija", "NazivScenarija", alarm.SifraScenarija);
            ViewData["SifraUredaja"] = new SelectList(_context.Uredaj, "SifraUredaja", "SifraUredaja", alarm.SifraUredaja);
            return View(alarm);
        }

        // GET: Alarms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alarm = await _context.Alarm.FindAsync(id);
            if (alarm == null)
            {
                return NotFound();
            }
            ViewData["SifraDogadaja"] = new SelectList(_context.Dogadaj, "SifraDogadaj", "SifraDogadaj", alarm.SifraDogadaja);
            ViewData["SifraScenarija"] = new SelectList(_context.Scenarij, "SifraScenarija", "NazivScenarija", alarm.SifraScenarija);
            ViewData["SifraUredaja"] = new SelectList(_context.Uredaj, "SifraUredaja", "SifraUredaja", alarm.SifraUredaja);
            return View(alarm);
        }

        // POST: Alarms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SifraUredaja,SifraDogadaja,SifraScenarija,SifraOperatera")] Alarm alarm)
        {
            if (id != alarm.SifraUredaja)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(alarm);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AlarmExists(alarm.SifraUredaja))
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
            ViewData["SifraDogadaja"] = new SelectList(_context.Dogadaj, "SifraDogadaj", "SifraDogadaj", alarm.SifraDogadaja);
            ViewData["SifraScenarija"] = new SelectList(_context.Scenarij, "SifraScenarija", "NazivScenarija", alarm.SifraScenarija);
            ViewData["SifraUredaja"] = new SelectList(_context.Uredaj, "SifraUredaja", "SifraUredaja", alarm.SifraUredaja);
            return View(alarm);
        }

        // GET: Alarms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alarm = await _context.Alarm
                .Include(a => a.SifraDogadajaNavigation)
                .Include(a => a.SifraScenarijaNavigation)
                .Include(a => a.SifraUredajaNavigation)
                .FirstOrDefaultAsync(m => m.SifraUredaja == id);
            if (alarm == null)
            {
                return NotFound();
            }

            return View(alarm);
        }

        // POST: Alarms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var alarm = await _context.Alarm.FindAsync(id);
            _context.Alarm.Remove(alarm);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AlarmExists(int id)
        {
            return _context.Alarm.Any(e => e.SifraUredaja == id);
        }
    }
}
