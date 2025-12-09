using ControlGastosBackend.Data;
using ControlGastosBackend.DTOs.RegistroGastoDetalle;
using ControlGastosBackend.DTOs.RegistrosGasto;
using ControlGastosBackend.DTOs.TiposGasto;
using ControlGastosBackend.Models.Movimiento;
using ControlGastosBackend.Models.Presupuesto;
using ControlGastosBackend.Models.RegistroGastoDetalle;
using ControlGastosBackend.Models.RegistrosGasto;
using ControlGastosBackend.Repositories.FondoMonetario;
using ControlGastosBackend.Repositories.Movimientos;
using ControlGastosBackend.Repositories.Presupuesto;
using ControlGastosBackend.Repositories.RegistroGastoDetalleRepository;
using ControlGastosBackend.Repositories.RegistrosGasto;
using ControlGastosBackend.Services.TiposGasto;
using Microsoft.EntityFrameworkCore;


namespace ControlGastosBackend.Services.RegistroGasto
{
    public class RegistroGastoService
    {
        private readonly RegistroGastoRepository _registroRepository;
        private readonly RegistroGastoDetalleRepository _registroGastoDetalleRepository;
        private readonly FondoMonetarioRepository _fondoRepository;
        private readonly PresupuestoGastoRepository _presupuestoGastoRepository;
        private readonly MovimientoRepository _movimientoRepository;
        private readonly AppDbContext _context;
        private readonly ILogger<TipoGastoService> _logger;

        public RegistroGastoService(
            RegistroGastoRepository registroRepository,
            RegistroGastoDetalleRepository registroGastoDetalleRepository,
            FondoMonetarioRepository fondoRepository,
            PresupuestoGastoRepository presupuestoGastoRepository,
            MovimientoRepository movimientoRepository,
            AppDbContext context,
            ILogger<TipoGastoService> logger)
        {
            _registroRepository = registroRepository;
            _registroGastoDetalleRepository = registroGastoDetalleRepository;
            _fondoRepository = fondoRepository;
            _presupuestoGastoRepository = presupuestoGastoRepository;
            _movimientoRepository = movimientoRepository;
            _context = context;
            _logger = logger;
        }

        public async Task<RegistroGastoResponseDto> CrearAsync(RegistroGastoCreateDto dto)
        {
            // Validar fondo monetario
            var fondo = await _fondoRepository.ObtenerPorIdAsync(dto.FondoMonetarioId);
            if (fondo == null)
                throw new Exception("El fondo monetario no existe.");

            // Validar detalles
            if (dto.Detalles == null || dto.Detalles.Count == 0)
                throw new Exception("Debe enviar al menos un detalle.");

            var registro = new ControlGastosBackend.Models.RegistrosGasto.RegistroGasto
            {
                Fecha = dto.Fecha,
                FondoMonetarioId = dto.FondoMonetarioId,
                Observaciones = dto.Observaciones,
                NombreComercio = dto.NombreComercio,
                TipoDocumento = (TipoDocumentoGasto)dto.TipoDocumento
            };

            // Crear registro en BD
            await _registroRepository.CrearAsync(registro);

            // Procesar detalles
            foreach (var item in dto.Detalles)
            {
                var detalle = new RegistroGastoDetalle
                {
                    TipoGastoId = item.TipoGastoId,
                    Monto = item.Monto,
                    Descripcion = item.Descripcion,
                    RegistroGastoId = registro.Id
                };

                await _registroGastoDetalleRepository.CreateAsync(detalle);

                await _presupuestoGastoRepository.ActualizarMontoEjecutadoAsync(
                       detalle.TipoGastoId,
                       dto.Fecha.ToString("yyyy-MM"),
                       detalle.Monto
                );

                await _fondoRepository.ActualizarSaldoAsync(fondo.Id, detalle.Monto);

                await _movimientoRepository.CrearAsync(new Models.Movimiento.Movimiento
                {
                    Fecha = dto.Fecha,
                    Descripcion = detalle.Descripcion,
                    Tipo = TipoMovimiento.Gasto,
                    Monto = detalle.Monto
                });


                var presupuesto = await _presupuestoGastoRepository
                    .GetByIdAnioMesAsync(item.TipoGastoId, dto.Fecha.ToString("yyyy-MM"));


                if (presupuesto == null)
                {
                    _logger.LogWarning(
                        $"No existe presupuesto para TipoGastoId {item.TipoGastoId} en {dto.Fecha:yyyy-MM}");
                }
                else
                {
                    if (presupuesto.MontoEjecutado > presupuesto.Monto)
                    {
                        _logger.LogWarning(
                            "El monto ejecutado del presupuesto ya ha alcanzado o superado el monto asignado.");
                    }
                }
            }

            await _context.SaveChangesAsync();

            return new RegistroGastoResponseDto
            {
                Id = registro.Id,
                Fecha = registro.Fecha,
                FondoMonetarioId = fondo.Id,
                Observaciones = registro.Observaciones,
                NombreComercio = registro.NombreComercio,
                TipoDocumento = registro.TipoDocumento.ToString(),
                Detalles = dto.Detalles
            };
        }


        public async Task<RegistroGastoResponseDto?> ObtenerPorIdAsync(Guid id)
        {
            // 1. Obtener el registro principal
            var registro = await _context.RegistroGasto
                .Include(r => r.FondoMonetario)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (registro == null)
                return null;

            // 2. Obtener los detalles asociados al registro
            var detalles = await _registroGastoDetalleRepository.GetDetallesByIdAsync(registro.Id);

            // 3. Mapear la respuesta con la misma estructura de ObtenerTodosAsync
            var respuesta = new RegistroGastoResponseDto
            {
                Id = registro.Id,
                FondoMonetarioId = registro.FondoMonetarioId,
                Fecha = registro.Fecha,
                Observaciones = registro.Observaciones,
                NombreComercio = registro.NombreComercio,
                TipoDocumento = registro.TipoDocumento.ToString(),

                Detalles = detalles
                    .Select(d => new RegistroGastoDetalleCreateDto
                    {
                        TipoGastoId = d.TipoGastoId,
                        Monto = d.Monto,
                        Descripcion = d.Descripcion
                    })
                    .ToList()
            };

            return respuesta;
        }


        public async Task<List<RegistroGastoResponseDto>> ObtenerTodosAsync()
        {
            var gastos = await _context.RegistroGasto
                .Include(r => r.FondoMonetario)
                .OrderByDescending(r => r.Fecha)
                .ToListAsync();

            var respuesta = new List<RegistroGastoResponseDto>();
            foreach (var r in gastos)
            {
                var detalles = await _registroGastoDetalleRepository.GetDetallesByIdAsync(r.Id);

                respuesta.Add(new RegistroGastoResponseDto
                {
                    Id = r.Id,
                    FondoMonetarioId = r.FondoMonetarioId,
                    Fecha = r.Fecha,
                    Observaciones = r.Observaciones,
                    NombreComercio = r.NombreComercio,
                    TipoDocumento = r.TipoDocumento.ToString(),
                    Detalles = detalles
                        .Select(d => new RegistroGastoDetalleCreateDto
                        {
                            TipoGastoId = d.TipoGastoId,
                            Monto = d.Monto,
                            Descripcion = d.Descripcion
                        })
                        .ToList()
                });
            }

            return respuesta;
        }


    }
}
