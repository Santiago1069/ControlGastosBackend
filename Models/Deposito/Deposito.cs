using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ControlGastosBackend.Models.FondoMonetario;

namespace ControlGastosBackend.Models.Deposito
{
    [Table("Depositos")]
    public class Deposito
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public DateTime Fecha { get; set; }

        [Required]
        [ForeignKey("FondoMonetarioId")]
        public Guid FondoMonetarioId { get; set; }

        public FondosMonetario FondoMonetario { get; set; }

        public string? Descripcion { get; set; }

        [Required]
        public decimal Monto { get; set; }
    }
}
