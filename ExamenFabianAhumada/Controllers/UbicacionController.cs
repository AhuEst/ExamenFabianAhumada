using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ExamenFabianAhumada.Data;

namespace ExamenFabianAhumada.Controllers
{
    public class UbicacionController : Controller
    {
        private readonly EjemploDbContext _context;

        public UbicacionController(EjemploDbContext context)
        {
            _context = context;
        }

        // GET: Ubicacion
        public async Task<IActionResult> Index()
        {
              return _context.Ubicacion != null ? 
                          View(await _context.Ubicacion.ToListAsync()) :
                          Problem("Entity set 'EjemploDbContext.Ubicacion'  is null.");
        }

        // GET: Ubicacion/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Ubicacion == null)
            {
                return NotFound();
            }

            var ubicacion = await _context.Ubicacion
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ubicacion == null)
            {
                return NotFound();
            }

            return View(ubicacion);
        }

        // GET: Ubicacion/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Ubicacion/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre")] Ubicacion ubicacion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ubicacion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ubicacion);
        }

        // GET: Ubicacion/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Ubicacion == null)
            {
                return NotFound();
            }

            var ubicacion = await _context.Ubicacion.FindAsync(id);
            if (ubicacion == null)
            {
                return NotFound();
            }
            return View(ubicacion);
        }

        // POST: Ubicacion/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre")] Ubicacion ubicacion)
        {
            if (id != ubicacion.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ubicacion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UbicacionExists(ubicacion.Id))
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
            return View(ubicacion);
        }

        // GET: Ubicacion/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Ubicacion == null)
            {
                return NotFound();
            }

            var ubicacion = await _context.Ubicacion
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ubicacion == null)
            {
                return NotFound();
            }

            return View(ubicacion);
        }

        // POST: Ubicacion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Ubicacion == null)
            {
                return Problem("Entity set 'EjemploDbContext.Ubicacion'  is null.");
            }
            var ubicacion = await _context.Ubicacion.FindAsync(id);
            if (ubicacion != null)
            {
                _context.Ubicacion.Remove(ubicacion);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UbicacionExists(int id)
        {
          return (_context.Ubicacion?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
