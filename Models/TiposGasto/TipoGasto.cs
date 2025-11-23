using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ControlGastosBackend.Models.TiposGasto
{
    [Table("TiposGasto")]
    public class TipoGasto
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; } = string.Empty;

        [MaxLength(250)]
        public string? Descripcion { get; set; }

        [Required]
        public EstadoTipoGasto Estado { get; set; } = EstadoTipoGasto.Activo;

        [MaxLength(20)]
        public string Color { get; set; } = "#000000";


    }

    public enum EstadoTipoGasto
    {
        Activo = 1,
        Inactivo = 2
    }

}
