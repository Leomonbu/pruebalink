using productoService.Models;

namespace productoService.Interfaces
{
    public interface IProductoRepositorio
    {
        Task<int> InsertarAsync(Productos producto);
        Task<Productos?> ObtenerPorIdAsync(long id_producto);
        Task<Productos?> ActualizarAsync(Productos producto);
    }

    public class ProductoService
    {
        private readonly IProductoRepositorio _repo;

        public ProductoService(IProductoRepositorio repo)
        {
            _repo = repo;
        }

        public async Task<int> CrearProductoAsync(Productos producto)
        {
            return await _repo.InsertarAsync(producto);
        }

        public async Task<bool> ActualizarProductoAsync(Productos producto)
        {
            var existente = await _repo.ObtenerPorIdAsync(producto.id_producto);
            if (existente == null)
                return false;

            //existente.nombre_producto = producto.nombre_producto;
            //existente.precio_producto = producto.precio_producto;

            await _repo.ActualizarAsync(existente);
            return true;
        }
    }
}
