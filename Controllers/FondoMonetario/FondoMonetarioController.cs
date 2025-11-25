using ControlGastosBackend.DTOs.FondoMonetario;
using ControlGastosBackend.Services.FondoMonetario;
using Microsoft.AspNetCore.Mvc;

namespace ControlGastosBackend.Controllers.FondoMonetario
{
    [ApiController]
    [Route("api/[controller]")]
    public class FondoMonetarioController : ControllerBase
    {
        private readonly FondoMonetarioService _fondoMonetarioService;

        public FondoMonetarioController(FondoMonetarioService fondoMonetarioService)
        {
            _fondoMonetarioService = fondoMonetarioService;
        }

        [HttpPost]
        public async Task<IActionResult> CrearFondo([FromBody] CrearFondoMonetarioDTO crearFondoMonetarioDTO)
        {
            try
            {
                var response = await _fondoMonetarioService.CrearAsync(crearFondoMonetarioDTO);

                return CreatedAtAction(nameof(CrearFondo), response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerFondos()
        {
            var fondos = await _fondoMonetarioService.ObtenerFondos();
            return Ok(fondos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerFondoPorId(Guid id)
        {
            var fondo = await _fondoMonetarioService.FondoMonetarioById(id);
            if (fondo == null)
                return NotFound(new { message = "Fondo monetario no encontrado" });
            return Ok(fondo);
        }
    }
}
