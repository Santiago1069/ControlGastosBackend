using ControlGastosBackend.Data;
using ControlGastosBackend.DTOs.Deposito;
using ControlGastosBackend.Models.Deposito;
using ControlGastosBackend.Repositories.Depositos;
using ControlGastosBackend.Repositories.FondoMonetario;
using ControlGastosBackend.Repositories.Movimientos;

namespace ControlGastosBackend.Services.Depositos
{
    public class DepositoService
    {
        private readonly DepositoRepository _depositoRepository;
        private readonly FondoMonetarioRepository _fondoRepository;
        private readonly MovimientoRepository _movimientoRepository;
        private readonly AppDbContext _context;

        public DepositoService(
            DepositoRepository depositoRepository,
            FondoMonetarioRepository fondoRepository,
            MovimientoRepository movimientoRepository,
            AppDbContext context)
        {
            _depositoRepository = depositoRepository;
            _fondoRepository = fondoRepository;
            _movimientoRepository = movimientoRepository;
            _context = context;
        }

        public async Task<ResponseDepositoDTO> CrearAsync(CrearDepositoDTO dto)
        {
            if (dto.Monto <= 0)
                throw new Exception("El monto debe ser mayor a 0.");

            var fondo = await _fondoRepository.ObtenerPorIdAsync(dto.FondoMonetarioId);
            if (fondo == null)
                throw new Exception("El fondo monetario no existe.");

            dto.Fecha = dto.Fecha == default ? DateTime.Now : dto.Fecha;

            var deposito = new Deposito
            {
                FondoMonetarioId = dto.FondoMonetarioId,
                Fecha = dto.Fecha,
                Descripcion = dto.Descripcion,
                Monto = dto.Monto
            };

            var movimiento = new Models.Movimiento.Movimiento
            {
                Fecha = dto.Fecha,
                Tipo = Models.Movimiento.TipoMovimiento.Deposito,
                Monto = dto.Monto,
                Descripcion = dto.Descripcion
            };

            await _fondoRepository.SumarSaldoActual(dto.FondoMonetarioId, dto.Monto);
            await _depositoRepository.CrearAsync(deposito);
            await _movimientoRepository.CrearAsync(movimiento);
            await _context.SaveChangesAsync();

            // Retornar DTO
            var depositoDTO = new ResponseDepositoDTO
            {
                Id = deposito.Id,
                Fecha = deposito.Fecha,
                FondoMonetarioId = deposito.FondoMonetarioId,
                Descripcion = deposito.Descripcion,
                Monto = deposito.Monto
            };

            return depositoDTO;
        }


        public async Task<List<Deposito>> ObtenerAllDepositos()
        {
            return await _depositoRepository.ObtenerTodosAsync();
        }


    }
}
