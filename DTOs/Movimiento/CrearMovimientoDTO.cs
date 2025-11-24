using ControlGastosBackend.Models.Movimiento;
using System.ComponentModel.DataAnnotations;

namespace ControlGastosBackend.DTOs.Movimiento
{
    public class CrearMovimientoDTO
    {

        public DateTime Fecha { get; set; }

        public string? Descripcion { get; set; }

        public TipoMovimiento Tipo { get; set; }

        public decimal Monto { get; set; }
    }
}
