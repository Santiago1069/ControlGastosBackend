using ControlGastosBackend.Data;
using ControlGastosBackend.Models.Presupuesto;
using Microsoft.EntityFrameworkCore;

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

        public async Task<PresupuestoGasto?> GetByIdAnioMesAsync(Guid id, string anioMes)
        {
            return await _context.PresupuestosGasto
                .Include(p => p.TipoGasto)
                .FirstOrDefaultAsync(p => p.TipoGastoId == id && p.AnioMes == anioMes);
        }

        public async Task<bool> ActualizarMontoEjecutadoAsync(Guid tipoGastoId, string anioMes, decimal monto)
        {
            var presupuesto = await _context.PresupuestosGasto
                .FirstOrDefaultAsync(p => p.TipoGastoId == tipoGastoId && p.AnioMes == anioMes);

            if (presupuesto == null)
                return false;

            presupuesto.MontoEjecutado += monto;

            _context.PresupuestosGasto.Update(presupuesto);
            return true;
        }

        public async Task<List<PresupuestoGasto>> GetAllAsync()
        {
            return await _context.PresupuestosGasto
                .Include(p => p.TipoGasto)
                .ToListAsync();
        }

        public async Task<PresupuestoGasto?> GetByIdAsync(Guid id)
        {
            return await _context.PresupuestosGasto
                .Include(p => p.TipoGasto)
                .FirstOrDefaultAsync(p => p.TipoGastoId == id);
        }



    }
}
