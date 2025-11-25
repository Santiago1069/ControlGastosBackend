using ControlGastosBackend.Models.Movimiento;
using System.ComponentModel.DataAnnotations;

namespace ControlGastosBackend.DTOs.Deposito
{
    public class CrearDepositoDTO
    {
        public DateTime Fecha { get; set; }
        public string? Descripcion { get; set; }
        public Guid FondoMonetarioId { get; set; }
        public decimal Monto { get; set; }
    }
}
