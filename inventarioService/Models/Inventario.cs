using System.ComponentModel.DataAnnotations;

namespace inventarioService.Models
{
    public class Inventario
    {
        [Key]
        public required long Id_producto { get; set; }
        public int _Cantidad;

        public required int Cantidad
        {
            get => _Cantidad;
            set
            {
                if (_Cantidad != value)
                {
                    _Cantidad = value;
                    Console.WriteLine($"[EVENTO] Inventario del producto {Id_producto} actualizado. Nueva cantidad: {_Cantidad}");
                }
            }
        }
    }
}
