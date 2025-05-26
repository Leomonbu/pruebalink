using inventarioService.Interfaces;
using inventarioService.Models;

namespace inventarioService.Services
{
    public class ProductosApiClient : IProductosApiClient
    {
        private readonly HttpClient _httpClient;

        public ProductosApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<ProductoDto>> ObtenerProductosAsync()
        {
            var productos = await _httpClient.GetFromJsonAsync<List<ProductoDto>>("/api/Productos");
            return productos ?? new List<ProductoDto>();
        }
    }
}
