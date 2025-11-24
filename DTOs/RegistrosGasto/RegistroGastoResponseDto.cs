using ControlGastosBackend.DTOs.RegistroGastoDetalle;

namespace ControlGastosBackend.DTOs.RegistrosGasto
{
    public class RegistroGastoResponseDto
    {
        public Guid Id { get; set; }
        public DateTime Fecha { get; set; }
        public Guid FondoMonetarioId { get; set; }
        public string? Observaciones { get; set; }
        public string NombreComercio { get; set; }
        public string TipoDocumento { get; set; }
        public List<RegistroGastoDetalleCreateDto> Detalles { get; set; }

    }
}
