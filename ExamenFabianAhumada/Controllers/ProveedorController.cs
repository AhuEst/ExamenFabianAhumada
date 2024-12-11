using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ExamenFabianAhumada.Models;
using Microsoft.EntityFrameworkCore;
using ExamenFabianAhumada.Data;

namespace ExamenFabianAhumada.Controllers
{
    public class ProveedorController : Controller
    {
        private readonly EjemploDbContext _context;

        public ProveedorController(EjemploDbContext context)
        {
            _context = context;
        }

        // GET: Proveedor
        public async Task<IActionResult> Index()
        {
            // Lista de proveedor
            var proveedores = await _context.Proveedor.ToListAsync(); 
            // Lista de ubicacion
            var ubicaciones = await _context.Ubicacion.ToListAsync(); 

            // Pasar ubicaciones al viewbag
            ViewBag.Ubicaciones = ubicaciones.ToDictionary(u => u.Id, u => u.Nombre);

            return _context.Proveedor != null ? 
                          View(await _context.Proveedor.ToListAsync()) :
                          Problem("Entity set 'EjemploDbContext.Proveedor'  is null.");
        }

        // GET: Proveedor/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Proveedor == null)
            {
                return NotFound();
            }

            var proveedor = await _context.Proveedor
                .FirstOrDefaultAsync(m => m.Id == id);
            if (proveedor == null)
            {
                return NotFound();
            }

            return View(proveedor);
        }

        // GET: Proveedor/Create
        public IActionResult Create()
        {
            ViewBag.Ubicaciones = new SelectList(_context.Ubicacion, "Id", "Nombre");
            return View();
        }

        // POST: Proveedor/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Rut,Nombre,UbicacionId")] Proveedor proveedor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(proveedor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Ubicaciones = new SelectList(_context.Ubicacion, "Id", "Nombre", proveedor.UbicacionId);
            return View(proveedor);
        }

        public async Task<IActionResult> ProveedoresPorUbicacion()
        {
            var agrupacion = await _context.Proveedor
                .Join(_context.Ubicacion,
                      proveedor => proveedor.UbicacionId,
                      ubicacion => ubicacion.Id,
                      (proveedor, ubicacion) => new { proveedor, ubicacion })  // Aqui se hace el inner join
                .GroupBy(p => p.ubicacion.Nombre)  // Agrupacion por nombre de ubicacion
                .Select(g => new ProveedorUbicacion
                {
                    UbicacionNombre = g.Key,
                    CantidadProveedores = g.Count()
                })
                .ToListAsync();

            return View(agrupacion);
        }


        public async Task<IActionResult> DetallesPorUbicacion(string ubicacionNombre)
        {
            if (string.IsNullOrEmpty(ubicacionNombre))
            {
                return NotFound();
            }

            // Obtener los proveedores de la ubicación específica
            var proveedores = await _context.Proveedor
                .Join(_context.Ubicacion,
                      proveedor => proveedor.UbicacionId,
                      ubicacion => ubicacion.Id,
                      // Inner Join
                      (proveedor, ubicacion) => new { proveedor, ubicacion }) 
                // Nombre de la ubicacion
                .Where(p => p.ubicacion.Nombre == ubicacionNombre) 
                // Select solo al proveedor
                .Select(p => p.proveedor) 
                .ToListAsync();

            if (proveedores == null || !proveedores.Any())
            {
                return NotFound();
            }
            
            // Pasar el nombre de la ubicación a la vista
            ViewData["UbicacionNombre"] = ubicacionNombre;  
            return View(proveedores);
        }


        // GET: Proveedor/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Proveedor == null)
            {
                return NotFound();
            }

            var proveedor = await _context.Proveedor.FindAsync(id);
            if (proveedor == null)
            {
                return NotFound();
            }
            ViewBag.Ubicaciones = new SelectList(_context.Ubicacion, "Id", "Nombre", proveedor.UbicacionId);

            return View(proveedor);
        }

        // POST: Proveedor/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Rut,Nombre,UbicacionId")] Proveedor proveedor)
        {
            if (id != proveedor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(proveedor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProveedorExists(proveedor.Id))
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

            ViewBag.Ubicaciones = new SelectList(_context.Ubicacion, "Id", "Nombre", proveedor.UbicacionId);

            return View(proveedor);
        }

        // GET: Proveedor/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Proveedor == null)
            {
                return NotFound();
            }

            var proveedor = await _context.Proveedor
                .FirstOrDefaultAsync(m => m.Id == id);
            if (proveedor == null)
            {
                return NotFound();
            }

            return View(proveedor);
        }

        // POST: Proveedor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Proveedor == null)
            {
                return Problem("Entity set 'EjemploDbContext.Proveedor'  is null.");
            }
            var proveedor = await _context.Proveedor.FindAsync(id);
            if (proveedor != null)
            {
                _context.Proveedor.Remove(proveedor);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProveedorExists(int id)
        {
          return (_context.Proveedor?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
