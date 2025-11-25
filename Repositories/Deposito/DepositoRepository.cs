using ControlGastosBackend.Data;
using ControlGastosBackend.Models.Deposito;
using Microsoft.EntityFrameworkCore;

namespace ControlGastosBackend.Repositories.Depositos
{
    public class DepositoRepository
    {
        private readonly AppDbContext _context;

        public DepositoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task CrearAsync(Deposito deposito)
        {
            await _context.Deposito.AddAsync(deposito);
        }

        public async Task<List<Deposito>> ObtenerTodosAsync()
        {
            return await _context.Deposito.ToListAsync();
        }


    }
}
