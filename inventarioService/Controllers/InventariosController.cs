using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using inventarioService.Context;
using inventarioService.Models;
using inventarioService.Interfaces;

namespace inventarioService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventariosController : ControllerBase
    {
        private readonly AppDBContext _context;
        private readonly IProductosApiClient _productosApi;

        public InventariosController(AppDBContext context, IProductosApiClient productosApi)
        {
            _context = context;
            _productosApi = productosApi;
        }

        // GET: api/Inventarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Inventario>>> GetInventario()
        {
            return await _context.Inventario.ToListAsync();
        }

        // GET: api/Inventarios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Inventario>> GetInventario(long id)
        {
            var inventario = await _context.Inventario.FindAsync(id);

            if (inventario == null)
            {
                return NotFound();
            }

            return inventario;
        }

        // GET: api/Inventarios/producto/2
        [HttpGet("producto/{id_producto}")]
        public async Task<IActionResult> GetExternos(long id_producto)
        {
            var inventario = await _context.Inventario.FindAsync(id_producto);
            ListadoInventario resultado = new ListadoInventario();

            if (inventario == null)
            {
                return NotFound();
            }
            else
            {
                var productos = await _productosApi.ObtenerProductosAsync();
                var productoServicio = productos.Where(p => p.id_producto == inventario.Id_producto).First();

                resultado.Id_producto = inventario.Id_producto;
                resultado.nombre_producto = productoServicio.nombre_producto;
                resultado.precio_producto = productoServicio.precio_producto;
                resultado.Cantidad = inventario.Cantidad;
            }

            return Ok(resultado);
        }

        // PUT: api/Inventarios/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInventario(long id, Inventario inventario)
        {
            if (id != inventario.Id_producto)
            {
                return BadRequest();
            }

            _context.Entry(inventario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InventarioExists(id))
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

        // POST: api/Inventarios
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Inventario>> PostInventario(Inventario inventario)
        {
            _context.Inventario.Add(inventario);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInventario", new { id = inventario.Id_producto }, inventario);
        }

        // DELETE: api/Inventarios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInventario(long id)
        {
            var inventario = await _context.Inventario.FindAsync(id);
            if (inventario == null)
            {
                return NotFound();
            }

            _context.Inventario.Remove(inventario);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool InventarioExists(long id)
        {
            return _context.Inventario.Any(e => e.Id_producto == id);
        }
    }
}
