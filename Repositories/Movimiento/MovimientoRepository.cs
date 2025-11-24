using ControlGastosBackend.Data;
using ControlGastosBackend.Models.Movimiento;

namespace ControlGastosBackend.Repositories.Movimientos
{
    public class MovimientoRepository
    {
        private readonly AppDbContext _context;

        public MovimientoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task CrearAsync(Movimiento movimiento)
        {
            await _context.Movimiento.AddAsync(movimiento);
        }
    }
}
