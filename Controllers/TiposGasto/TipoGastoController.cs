using ControlGastosBackend.DTOs.TiposGasto;
using ControlGastosBackend.Services.TiposGasto;
using Microsoft.AspNetCore.Mvc;

namespace ControlGastosBackend.Controllers.TiposGasto
{
    [ApiController]
    [Route("api/[controller]")]
    public class TipoGastoController : ControllerBase
    {
        private readonly TipoGastoService _tipoGastoService;

        public TipoGastoController(TipoGastoService tipoGastoService)
        {
            _tipoGastoService = tipoGastoService;
        }

        [HttpPost]
        public async Task<IActionResult> CrearTipoGasto([FromBody] CreateTipoGastoDto createTipoGastoDto)
        {
            try
            {
                var response = await _tipoGastoService.CrearTipoGastoAsync(createTipoGastoDto);
                return CreatedAtAction(nameof(CrearTipoGasto), response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }


        [HttpGet]
        public async Task<IActionResult> ObtenerTiposGasto()
        {
            var response = await _tipoGastoService.ObtenerTodosAsync();
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerTipoGastoPorId(Guid id)
        {
            var response = await _tipoGastoService.ObtenerPorIdAsync(id);
            if (response == null)
                return NotFound(new { message = "Tipo de gasto no encontrado" });
            return Ok(response);
        }
    }
}
