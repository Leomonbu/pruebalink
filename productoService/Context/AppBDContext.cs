using Microsoft.EntityFrameworkCore;
using productoService.Models;

namespace productoService.Context
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {

        }

        public DbSet<Productos> Productos { get; set; }
    }
}
