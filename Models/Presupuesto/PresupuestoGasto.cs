using ControlGastosBackend.Models.TiposGasto;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ControlGastosBackend.Models.Presupuesto
{
    [Table("PresupuestosGasto")]
    public class PresupuestoGasto
    {
        [Key]
        public Guid Id { get; set; }

        public Guid TipoGastoId { get; set; }
        public TipoGasto TipoGasto { get; set; }

        [Required]
        public decimal Monto { get; set; }

        [Required]
        public decimal MontoEjecutado { get; set; }

        [MaxLength(7)]
        public string AnioMes { get; set; } = string.Empty;
    }
}
