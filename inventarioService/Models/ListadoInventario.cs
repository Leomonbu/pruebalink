namespace inventarioService.Models
{
    public class ListadoInventario
    {
        public  long Id_producto { get; set; }
        public string nombre_producto { get; set; }
        public int precio_producto { get; set; }
        public  int Cantidad { get; set; }
    }
}
