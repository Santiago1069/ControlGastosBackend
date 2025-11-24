using ControlGastosBackend.DTOs.RegistroGastoDetalle;

namespace ControlGastosBackend.DTOs.RegistrosGasto
{
    public class RegistroGastoCreateDto
    {
        public DateTime Fecha { get; set; }
        public Guid FondoMonetarioId { get; set; }
        public string? Observaciones { get; set; }
        public string NombreComercio { get; set; } = string.Empty;
        public int TipoDocumento { get; set; }

        public List<RegistroGastoDetalleCreateDto> Detalles { get; set; }


    }
}
