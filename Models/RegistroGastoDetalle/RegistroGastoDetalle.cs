using ControlGastosBackend.Models.RegistrosGasto;
using ControlGastosBackend.Models.TiposGasto;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ControlGastosBackend.Models.RegistroGastoDetalle
{
    [Table("RegistroGastoDetalle")]
    public class RegistroGastoDetalle
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid TipoGastoId { get; set; }

        [ForeignKey("TipoGastoId")]
        public TipoGasto TipoGasto { get; set; }

        [Required]
        public decimal Monto { get; set; }

        [MaxLength(500)]
        public string? Descripcion { get; set; }

        [Required]
        public Guid RegistroGastoId { get; set; }

        [ForeignKey("RegistroGastoId")]
        public RegistroGasto RegistroGasto { get; set; }
    }
}
