using ControlGastosBackend.DTOs;
using ControlGastosBackend.Services;
using Microsoft.AspNetCore.Mvc;

namespace ControlGastosBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TipoGastoController : ControllerBase
    {
        private readonly TipoGastoService _service;

        public TipoGastoController(TipoGastoService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> CrearTipoGasto([FromBody] CreateTipoGastoDto dto)
        {
            try
            {
                var result = await _service.CrearTipoGastoAsync(dto);
                return CreatedAtAction(nameof(CrearTipoGasto), result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
