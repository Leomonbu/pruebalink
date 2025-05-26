using System.ComponentModel.DataAnnotations;

namespace productoService.Models
{
    public class Productos
    {
        [Key]
        public long id_producto { get; set; }
        public required string nombre_producto { get; set; }
        public required int precio_producto { get; set; }
    }
}
