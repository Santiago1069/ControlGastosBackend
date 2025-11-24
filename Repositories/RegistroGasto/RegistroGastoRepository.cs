using ControlGastosBackend.Data;
using ControlGastosBackend.Models.RegistrosGasto;
using Microsoft.EntityFrameworkCore;

namespace ControlGastosBackend.Repositories.RegistrosGasto
{
    public class RegistroGastoRepository
    {

        private readonly AppDbContext _context;

        public RegistroGastoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task CrearAsync(RegistroGasto registro)
        {
            await _context.RegistroGasto.AddAsync(registro);
        }

        public async Task<RegistroGasto?> ObtenerPorIdAsync(Guid id)
        {
            return await _context.RegistroGasto
                .Include(r => r.FondoMonetario)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<List<RegistroGasto>> ObtenerTodosAsync()
        {
            return await _context.RegistroGasto
                .Include(r => r.FondoMonetario)
                .OrderByDescending(r => r.Fecha)
                .ToListAsync();
        }

    }
}
