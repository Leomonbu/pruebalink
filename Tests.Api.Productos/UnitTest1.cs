using Moq;
using productoService.Interfaces;
using productoService.Models;

namespace Tests.Api.Productos
{
    public class UnitTest1
    {
        [Fact]
        public async Task CrearProductoAsync_CreaProductoCorrectamente()
        {
            var productoDemo = new productoService.Models.Productos
            {
                nombre_producto = "Nuevo",
                precio_producto = 5600
            };

            var mockRepo = new Mock<IProductoRepositorio>();
            mockRepo.Setup(r => r.InsertarAsync(It.IsAny<productoService.Models.Productos>()))
                    .ReturnsAsync(1);

            var servicio = new ProductoService(mockRepo.Object);
            var id = await servicio.CrearProductoAsync(productoDemo);

            Assert.Equal(1, id);
            mockRepo.Verify(r => r.InsertarAsync(It.Is<productoService.Models.Productos>(p => p.nombre_producto == "Nuevo")), Times.Once);
        }

        [Fact]
        public async Task ActualizarProductoAsync_ProductoExistente_RetornaTrue()
        {
            var productoAnterior = new productoService.Models.Productos
            {
                id_producto = 5,
                nombre_producto = "Antiguo",
                precio_producto = 5600
            };

            var productoNuevo = new productoService.Models.Productos
            {
                id_producto = 5,
                nombre_producto = "Nuevo",
                precio_producto = 11600
            };

            var mockRepo = new Mock<IProductoRepositorio>();

            // Simula que el producto ya existe
            mockRepo.Setup(r => r.ObtenerPorIdAsync(It.IsAny<int>()))
                   .ReturnsAsync(new productoService.Models.Productos
                   {
                         id_producto = 1,
                         nombre_producto = "Producto de prueba",
                         precio_producto = 5600
                     });

            // Simula que se actualiza correctamente (sin excepción)
            mockRepo.Setup(r => r.ActualizarAsync(It.IsAny<productoService.Models.Productos>()))
                    .Returns(() =>
                    {
                        return Task.CompletedTask;
                    });

            var servicio = new ProductoService(mockRepo.Object);

            // Act
            var resultado = await servicio.ActualizarProductoAsync(productoNuevo);

            // Assert
            Assert.True(resultado); // Se espera que retorne true
            mockRepo.Verify(r => r.ActualizarAsync(It.Is<productoService.Models.Productos>(
                p => p.id_producto == 1 && p.nombre_producto == "Nuevo" && p.precio_producto == 11600
            )), Times.Once);
        }
    }
}