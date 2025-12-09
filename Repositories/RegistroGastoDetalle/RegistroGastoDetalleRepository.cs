using ControlGastosBackend.Data;
using ControlGastosBackend.Models.Presupuesto;
using ControlGastosBackend.Models.RegistroGastoDetalle;
using Microsoft.EntityFrameworkCore;

namespace ControlGastosBackend.Repositories.RegistroGastoDetalleRepository
{
    public class RegistroGastoDetalleRepository
    {
        private readonly AppDbContext _context;

        public RegistroGastoDetalleRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(RegistroGastoDetalle detalle)
        {
            await _context.RegistroGastoDetalle.AddAsync(detalle);
        }

        public async Task<PresupuestoGasto?> GetByIdAndAnioMesAsync(Guid id, string anioMes)
        {
            return await _context.PresupuestosGasto
                .Include(p => p.TipoGasto)
                .FirstOrDefaultAsync(p => p.Id == id && p.AnioMes == anioMes);
        }

        public async Task<List<RegistroGastoDetalle>> GetDetallesByIdAsync(Guid registroGastoId)
        {
            return await _context.RegistroGastoDetalle
                .Where(d => d.RegistroGastoId == registroGastoId)
                .ToListAsync();
        }
    }
}
