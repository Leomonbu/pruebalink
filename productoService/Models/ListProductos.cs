namespace productoService.Models
{
    public class ListProductos
    {
        public int page { get; set; }
        public int pagesize { get; set; }
        public int totalItems { get; set; }
        public int totalPages { get; set; }
        public IEnumerable<Productos> items { get; set; }
    }
}
