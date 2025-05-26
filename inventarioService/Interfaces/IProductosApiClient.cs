using inventarioService.Models;

namespace inventarioService.Interfaces
{
    public interface IProductosApiClient
    {
        Task<IEnumerable<ProductoDto>> ObtenerProductosAsync();
    }
}
