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
    }
}
