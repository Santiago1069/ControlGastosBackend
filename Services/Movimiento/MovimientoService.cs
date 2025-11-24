using ControlGastosBackend.Data;
using ControlGastosBackend.DTOs.Movimiento;
using ControlGastosBackend.Models.Movimiento;
using ControlGastosBackend.Repositories.Movimientos;

namespace ControlGastosBackend.Services.Movimientos
{
    public class MovimientoService
    {
        private readonly MovimientoRepository _repository;
        private readonly AppDbContext _context;

        public MovimientoService(
            MovimientoRepository repository,
            AppDbContext context)
        {
            _repository = repository;
            _context = context;
        }

        public async Task<Movimiento> CrearAsync(CrearMovimientoDTO dto)
        {
            if (dto.Monto <= 0)
                throw new Exception("El monto debe ser mayor a 0.");

            var movimiento = new Movimiento
            {
                Fecha = dto.Fecha,
                Descripcion = dto.Descripcion,
                Tipo = dto.Tipo,
                Monto = dto.Monto
            };

            await _repository.CrearAsync(movimiento);
            await _context.SaveChangesAsync();

            return movimiento;
        }



    }
}
