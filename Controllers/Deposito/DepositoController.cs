using ControlGastosBackend.DTOs.Deposito;
using ControlGastosBackend.Models.Deposito;
using ControlGastosBackend.Services.Depositos;
using Microsoft.AspNetCore.Mvc;

namespace ControlGastosBackend.Controllers.Depositos
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepositoController : ControllerBase
    {
        private readonly DepositoService _service;

        public DepositoController(DepositoService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> CrearDeposito([FromBody] CrearDepositoDTO deposito)
        {
            try
            {
                var result = await _service.CrearAsync(deposito);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerDepositos()
        {
            var result = await _service.ObtenerAllDepositos();
            return Ok(result);
        }
    }
}
