using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using productoService.Context;
using productoService.Models;

namespace productoService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly AppDBContext _context;

        public ProductosController(AppDBContext context)
        {
            _context = context;
        }

        // GET: api/Productos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Productos>>> GetProductos()
        {
            return await _context.Productos.ToListAsync();
        }

        // GET: api/Productos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Productos>> GetProductos(long id)
        {
            var productos = await _context.Productos.FindAsync(id);

            if (productos == null)
            {
                return NotFound();
            }

            return productos;
        }

        // GET: api/Productos/1/5
        [HttpGet("{page}/{pageSize}")]
        public async Task<ActionResult<ListProductos>> GetProductosLista(int page, int pageSize)
        {
            ListProductos Listado = new ListProductos();

            if (page <= 0 || pageSize <= 0)
                return BadRequest("page y pageSize deben ser mayores que cero.");

            var productos = await _context.Productos.ToListAsync();

            if (productos == null)
            {
                return NotFound();
            }

            var totalItems = productos.Count;
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            var items = productos
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

            Listado.page = page;
            Listado.pagesize = pageSize;
            Listado.totalItems = totalItems;
            Listado.totalPages = totalPages;
            Listado.items = items;

            return Listado;
        }

        // PUT: api/Productos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductos(long id, Productos productos)
        {
            if (id != productos.id_producto)
            {
                return BadRequest();
            }

            _context.Entry(productos).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductosExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Productos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Productos>> PostProductos(Productos productos)
        {
            _context.Productos.Add(productos);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProductos", new { id = productos.id_producto }, productos);
        }

        // DELETE: api/Productos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductos(long id)
        {
            var productos = await _context.Productos.FindAsync(id);
            if (productos == null)
            {
                return NotFound();
            }

            _context.Productos.Remove(productos);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductosExists(long id)
        {
            return _context.Productos.Any(e => e.id_producto == id);
        }
    }
}
