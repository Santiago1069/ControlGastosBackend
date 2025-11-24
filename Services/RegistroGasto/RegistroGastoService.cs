using ControlGastosBackend.Data;
using ControlGastosBackend.DTOs.RegistrosGasto;
using ControlGastosBackend.DTOs.TiposGasto;
using ControlGastosBackend.Models.Presupuesto;
using ControlGastosBackend.Models.RegistroGastoDetalle;
using ControlGastosBackend.Models.RegistrosGasto;
using ControlGastosBackend.Repositories.FondoMonetario;
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
        private readonly AppDbContext _context;
        private readonly ILogger<TipoGastoService> _logger;

        public RegistroGastoService(
            RegistroGastoRepository registroRepository,
            RegistroGastoDetalleRepository registroGastoDetalleRepository,
            FondoMonetarioRepository fondoRepository,
            PresupuestoGastoRepository presupuestoGastoRepository,
            AppDbContext context,
            ILogger<TipoGastoService> logger)
        {
            _registroRepository = registroRepository;
            _registroGastoDetalleRepository = registroGastoDetalleRepository;
            _fondoRepository = fondoRepository;
            _presupuestoGastoRepository = presupuestoGastoRepository;
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
            var registro = await _context.RegistroGasto
                .Include(r => r.FondoMonetario)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (registro == null)
                return null;

            return new RegistroGastoResponseDto
            {
                Id = registro.Id,
                Fecha = registro.Fecha,
                FondoMonetarioId = registro.FondoMonetario.Id,
                Observaciones = registro.Observaciones,
                NombreComercio = registro.NombreComercio,
                TipoDocumento = registro.TipoDocumento.ToString()
            };
        }

        public async Task<List<RegistroGastoResponseDto>> ObtenerTodosAsync()
        {
            return await _context.RegistroGasto
                .Include(r => r.FondoMonetario)
                .OrderByDescending(r => r.Fecha)
                .Select(r => new RegistroGastoResponseDto
                {
                    Id = r.Id,
                    FondoMonetarioId = r.Id,
                    Fecha = r.Fecha,
                    Observaciones = r.Observaciones,
                    NombreComercio = r.NombreComercio,
                    TipoDocumento = r.TipoDocumento.ToString()
                })
                .ToListAsync();
        }

    }
}
