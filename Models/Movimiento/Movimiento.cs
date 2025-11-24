using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ControlGastosBackend.Models.Movimiento
{
    [Table("Movimientos")]
    public class Movimiento
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public DateTime Fecha { get; set; }

        [MaxLength(500)]
        public string? Descripcion { get; set; }

        [Required]
        public TipoMovimiento Tipo { get; set; }

        [Required]
        public decimal Monto { get; set; }
    }

    public enum TipoMovimiento
    {
        Deposito = 1,
        Gasto = 2
    }
}
