
using GestionProductosNet10.Data;
using GestionProductosNet10.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestionProductosNet10.Controllers
{
    public class ProductosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // CONSULTAR: Mostrar todos los productos
        public async Task<IActionResult> Index()
        {
            var productos = await _context.Productos
                .AsNoTracking()
                .OrderBy(p => p.IdProducto)
                .ToListAsync();

            return View(productos);
        }

        // CREAR: Mostrar formulario
        [HttpGet]
        public IActionResult Crear()
        {
            return View();
        }

        // CREAR: Guardar producto
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(
            [Bind("Nombre,Descripcion,Precio,Stock")] Producto producto)
        {
            if (!ModelState.IsValid)
            {
                return View(producto);
            }

            _context.Productos.Add(producto);
            await _context.SaveChangesAsync();

            TempData["Mensaje"] = "Producto registrado correctamente.";

            return RedirectToAction(nameof(Index));
        }

        // EDITAR: Mostrar formulario con los datos actuales
        [HttpGet]
        public async Task<IActionResult> Editar(int id)
        {
            var producto = await _context.Productos.FindAsync(id);

            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        // EDITAR: Guardar los cambios realizados
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(
            int id,
            [Bind("IdProducto,Nombre,Descripcion,Precio,Stock")] Producto datos)
        {
            if (id != datos.IdProducto)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(datos);
            }

            var producto = await _context.Productos.FindAsync(id);

            if (producto == null)
            {
                return NotFound();
            }

            producto.Nombre = datos.Nombre;
            producto.Descripcion = datos.Descripcion;
            producto.Precio = datos.Precio;
            producto.Stock = datos.Stock;

            await _context.SaveChangesAsync();

            TempData["Mensaje"] = "Producto actualizado correctamente.";

            return RedirectToAction(nameof(Index));
        }
    }
}
