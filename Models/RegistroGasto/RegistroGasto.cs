using ControlGastosBackend.Models.FondoMonetario;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ControlGastosBackend.Models.RegistrosGasto
{
    [Table("RegistrosGasto")]
    public class RegistroGasto
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public DateTime Fecha { get; set; }

        [Required]
        [ForeignKey("FondoMonetario")]
        public Guid FondoMonetarioId { get; set; }

        public FondosMonetario FondoMonetario { get; set; }

        [MaxLength(300)]
        public string? Observaciones { get; set; }

        [Required]
        [MaxLength(150)]
        public string NombreComercio { get; set; } = string.Empty;

        [Required]
        public TipoDocumentoGasto TipoDocumento { get; set; } = TipoDocumentoGasto.Comprobante;
    }

    public enum TipoDocumentoGasto
    {
        Comprobante = 1,
        Factura = 2,
        Otro = 3
    }
}
