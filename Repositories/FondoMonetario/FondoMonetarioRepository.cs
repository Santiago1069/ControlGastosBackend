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

        public async Task<bool> ActualizarSaldoAsync(Guid fondoId, decimal montoRestar)
        {
            var fondo = await _context.FondoMonetario
                .FirstOrDefaultAsync(f => f.Id == fondoId);

            if (fondo == null)
                return false;

            // Restamos el monto
            fondo.SaldoActual -= montoRestar;

            if (fondo.SaldoActual < 0)
                fondo.SaldoActual = 0;

            _context.FondoMonetario.Update(fondo);
            return true;
        }
    }
}
