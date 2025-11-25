using ControlGastosBackend.DTOs.Presupuesto;
using ControlGastosBackend.Services.Presupuesto;
using Microsoft.AspNetCore.Mvc;

namespace ControlGastosBackend.Controllers.Presupuesto
{
    [ApiController]
    [Route("api/[controller]")]
    public class PresupuestoGastoController : ControllerBase
    {
        private readonly PresupuestoGastoService _presupuestoGastoService;

        public PresupuestoGastoController(PresupuestoGastoService presupuestoGastoService)
        {
            _presupuestoGastoService = presupuestoGastoService;
        }

        [HttpPost]
        public async Task<IActionResult> CrearPresupuesto([FromBody] PresupuestoGastoCreateDto presupuestoGastoCreateDto)
        {
            try
            {
                var response = await _presupuestoGastoService.CreateAsync(presupuestoGastoCreateDto);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet("{id}/anioMes/{anioMes}")]
        public async Task<IActionResult> GetPresupuesto(Guid id, string anioMes)
        {
            var result = await _presupuestoGastoService.GetByIdAsync(id, anioMes);

            if (result == null)
                return NotFound(new { message = "Presupuesto no encontrado" });

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPresupuestos()
        {
            var result = await _presupuestoGastoService.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPresupuestoById(Guid id)
        {
            var result = await _presupuestoGastoService.GetByIdAsync(id);
            if (result == null)
                return NotFound(new { message = "Presupuesto no encontrado" });
            return Ok(result);
        }
    }
}
