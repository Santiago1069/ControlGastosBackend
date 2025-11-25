using ControlGastosBackend.Data;
using ControlGastosBackend.DTOs.Presupuesto;
using ControlGastosBackend.Models.Presupuesto;
using ControlGastosBackend.Repositories.Presupuesto;
using Microsoft.EntityFrameworkCore;

namespace ControlGastosBackend.Services.Presupuesto
{
    public class PresupuestoGastoService
    {
        private readonly PresupuestoGastoRepository _presupuestoGastoRepository;
        private readonly AppDbContext _context;

        public PresupuestoGastoService(PresupuestoGastoRepository presupuestoGastoRepository, AppDbContext context)
        {
            _presupuestoGastoRepository = presupuestoGastoRepository;
            _context = context;
        }


        public async Task<PresupuestoGastoResponseDto> CreateAsync(PresupuestoGastoCreateDto presupuestoGastoCreateDto)
        {
            var presupuestoGasto = new PresupuestoGasto
            {
                TipoGastoId = presupuestoGastoCreateDto.TipoGastoId,
                Monto = presupuestoGastoCreateDto.Monto,
                MontoEjecutado = presupuestoGastoCreateDto.MontoEjecutado,
                AnioMes = presupuestoGastoCreateDto.AnioMes
            };

            await _presupuestoGastoRepository.CreateAsync(presupuestoGasto);
            await _context.SaveChangesAsync();

            var tipoGasto = await _context.TiposGasto.FindAsync(presupuestoGasto.TipoGastoId);

            return new PresupuestoGastoResponseDto
            {
                Id = presupuestoGasto.Id,
                TipoGastoId = presupuestoGasto.TipoGastoId,
                TipoGastoNombre = tipoGasto?.Nombre ?? "N/A",
                Monto = presupuestoGasto.Monto,
                MontoEjecutado = presupuestoGasto.MontoEjecutado,
                AnioMes = presupuestoGasto.AnioMes
            };
        }

        public async Task<PresupuestoGastoResponseDto?> GetByIdAsync(Guid id, string anioMes)
        {
            var presupuesto = await _presupuestoGastoRepository.GetByIdAnioMesAsync(id, anioMes);

            if (presupuesto == null)
                return null;

            return new PresupuestoGastoResponseDto
            {
                Id = presupuesto.Id,
                TipoGastoId = presupuesto.TipoGastoId,
                TipoGastoNombre = presupuesto.TipoGasto?.Nombre ?? "N/A",
                Monto = presupuesto.Monto,
                MontoEjecutado = presupuesto.MontoEjecutado,
                AnioMes = presupuesto.AnioMes
            };
        }

        public async Task<List<PresupuestoGastoResponseDto>> GetAllAsync()
        {
            var presupuestos = await _presupuestoGastoRepository.GetAllAsync();
            return presupuestos.Select(p => new PresupuestoGastoResponseDto
            {
                Id = p.Id,
                TipoGastoId = p.TipoGastoId,
                TipoGastoNombre = p.TipoGasto?.Nombre ?? "N/A",
                Monto = p.Monto,
                MontoEjecutado = p.MontoEjecutado,
                AnioMes = p.AnioMes
            }).ToList();
        }

        public async Task<PresupuestoGastoResponseDto?> GetByIdAsync(Guid id)
        {
            var presupuesto = await _presupuestoGastoRepository.GetByIdAsync(id);
            if (presupuesto == null)
                return null;
            return new PresupuestoGastoResponseDto
            {
                Id = presupuesto.Id,
                TipoGastoId = presupuesto.TipoGastoId,
                TipoGastoNombre = presupuesto.TipoGasto?.Nombre ?? "N/A",
                Monto = presupuesto.Monto,
                MontoEjecutado = presupuesto.MontoEjecutado,
                AnioMes = presupuesto.AnioMes
            };
        }

    }
}
