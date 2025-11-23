using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ControlGastosBackend.Models.FondoMonetario
{
    [Table("FondosMonetarios")]
    public class FondosMonetario
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string Nombre { get; set; } = string.Empty;

        [MaxLength(250)]
        public string? Descripcion { get; set; }

        [Required]
        public TipoFondo Tipo { get; set; } = TipoFondo.Cuenta;

        [Required]
        public decimal SaldoActual { get; set; } = 0;
    }

    public enum TipoFondo
    {
        Cuenta = 1,
        CajaMenuda = 2
    }
}
