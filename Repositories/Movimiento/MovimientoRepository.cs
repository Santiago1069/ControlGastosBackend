using ControlGastosBackend.Data;
using ControlGastosBackend.Models.Movimiento;
using Microsoft.EntityFrameworkCore;

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

        public async Task<List<Movimiento>> ObtenerTodosAsync()
        {
            return await _context.Movimiento.ToListAsync();
        }
    }
}
