using ControlGastosBackend.DTOs.RegistrosGasto;
using ControlGastosBackend.Services.RegistroGasto;
using Microsoft.AspNetCore.Mvc;

namespace ControlGastosBackend.Controllers.RegistroGasto
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegistroGastoController : ControllerBase
    {
        private readonly RegistroGastoService _registroGastoService;

        public RegistroGastoController(RegistroGastoService registroGastoService)
        {
            _registroGastoService = registroGastoService;
        }

        // POST: api/RegistroGasto
        [HttpPost]
        public async Task<IActionResult> CrearRegistroGasto([FromBody] RegistroGastoCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var creado = await _registroGastoService.CrearAsync(dto);

            return CreatedAtAction(nameof(ObtenerPorId), new { id = creado.Id }, creado);
        }

        // GET: api/RegistroGasto/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerPorId(Guid id)
        {
            var registro = await _registroGastoService.ObtenerPorIdAsync(id);

            if (registro == null)
                return NotFound();

            return Ok(registro);
        }

        // GET: api/RegistroGasto
        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            var registros = await _registroGastoService.ObtenerTodosAsync();
            return Ok(registros);
        }

    }
}
