using ControlGastosBackend.Data;
using ControlGastosBackend.Models.FondoMonetario;
using Microsoft.EntityFrameworkCore;

namespace ControlGastosBackend.Repositories.FondoMonetario
{
    public class FondoMonetarioRepository
    {
        private readonly AppDbContext _context;

        public FondoMonetarioRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> ExisteNombreAsync(string nombre)
        {
            return await _context.FondoMonetario
                .AnyAsync(f => f.Nombre.ToLower() == nombre.ToLower());
        }

        public async Task CrearAsync(FondosMonetario fondosMonetario)
        {
            _context.FondoMonetario.Add(fondosMonetario);
        }

        public async Task<List<FondosMonetario>> GetAllFondos()
        {
            return await _context.FondoMonetario.ToListAsync();
        }

        public async Task<FondosMonetario> ObtenerPorIdAsync(Guid fondoMonetarioId)
        {
            return await _context.FondoMonetario
                .FirstOrDefaultAsync(f => f.Id == fondoMonetarioId);
        }
    }
}
