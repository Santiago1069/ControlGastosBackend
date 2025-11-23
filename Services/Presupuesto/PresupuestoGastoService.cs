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


        public async Task<PresupuestoGasto> CreateAsync(PresupuestoGastoCreateDto presupuestoGastoCreateDto)
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

            return presupuestoGasto;

        }
    }
}
