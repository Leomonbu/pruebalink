using inventarioService.Models;
using Microsoft.EntityFrameworkCore;

namespace inventarioService.Context
{
    public class AppDBContext: DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {

        }

        public DbSet<Inventario> Inventario { get; set; }
    }
}
