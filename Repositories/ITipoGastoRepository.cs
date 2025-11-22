using ControlGastosBackend.Data;
using ControlGastosBackend.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace ControlGastosBackend.Repositories
{
    public class ITipoGastoRepository
    {
        private readonly AppDbContext _context;

        public ITipoGastoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(TipoGasto entity)
        {
            _context.TiposGasto.Add(entity);
        }

        public async Task<bool> ExistsByNameAsync(string nombre)
        {
            return await _context.TiposGasto
                .AnyAsync(x => x.Nombre.ToLower() == nombre.ToLower());
        }
    }
}
