using ControlGastosBackend.Models.FondoMonetario;
using ControlGastosBackend.Models.Presupuesto;
using ControlGastosBackend.Models.TiposGasto;
using Microsoft.EntityFrameworkCore;

namespace ControlGastosBackend.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<TipoGasto> TiposGasto { get; set; }
        public DbSet<FondosMonetario> FondoMonetario { get; set; }
        public DbSet<PresupuestoGasto> PresupuestosGasto { get; set; }
    }
}
