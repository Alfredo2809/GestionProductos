
using Microsoft.EntityFrameworkCore;
using GestionProductosNet10.Models;

namespace GestionProductosNet10.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Producto> Productos => Set<Producto>();
    }
}
