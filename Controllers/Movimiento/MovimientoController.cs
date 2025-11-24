using ControlGastosBackend.DTOs.Movimiento;
using ControlGastosBackend.Services.Movimientos;
using Microsoft.AspNetCore.Mvc;

namespace ControlGastosBackend.Controllers.Movimientos
{
    [ApiController]
    [Route("api/[controller]")]
    public class MovimientoController : ControllerBase
    {
        private readonly MovimientoService _service;

        public MovimientoController(MovimientoService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] CrearMovimientoDTO movimiento)
        {
            try
            {
                var creado = await _service.CrearAsync(movimiento);
                return Ok(creado);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
