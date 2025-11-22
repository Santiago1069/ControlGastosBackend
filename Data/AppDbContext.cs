
using ControlGastosBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace ControlGastosBackend.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<TipoGasto> TiposGasto { get; set; }
    }
}
