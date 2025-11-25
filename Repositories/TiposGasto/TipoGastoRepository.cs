using ControlGastosBackend.Data;
using ControlGastosBackend.Models.TiposGasto;
using Microsoft.EntityFrameworkCore;
using System;

namespace ControlGastosBackend.Repositories.TiposGasto
{
    public class TipoGastoRepository
    {
        private readonly AppDbContext _context;

        public TipoGastoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(TipoGasto tipoGasto)
        {
            _context.TiposGasto.Add(tipoGasto);
        }

        public async Task<bool> ExistsByNameAsync(string nombre)
        {
            return await _context.TiposGasto
                .AnyAsync(x => x.Nombre.ToLower() == nombre.ToLower());
        }

        public async Task<List<TipoGasto>> ObtenerTodos()
        {
            return await _context.TiposGasto.ToListAsync();
        }

        public async Task<TipoGasto?> ObtenerPorIdAsync(Guid tipoGastoId)
        {
            return await _context.TiposGasto
                .FirstOrDefaultAsync(t => t.Id == tipoGastoId);
        }
    }
}
