using ControlGastosBackend.Data;
using ControlGastosBackend.Models.Presupuesto;

namespace ControlGastosBackend.Repositories.Presupuesto
{
    public class PresupuestoGastoRepository
    {
        private readonly AppDbContext _context;

        public PresupuestoGastoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(PresupuestoGasto presupuesto)
        {
            await _context.PresupuestosGasto.AddAsync(presupuesto);
        }
    }
}
