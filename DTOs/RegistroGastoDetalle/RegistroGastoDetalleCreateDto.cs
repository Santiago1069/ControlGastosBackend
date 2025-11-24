namespace ControlGastosBackend.DTOs.RegistroGastoDetalle
{
    public class RegistroGastoDetalleCreateDto
    {
        public Guid TipoGastoId { get; set; }
        public decimal Monto { get; set; }
        public string? Descripcion { get; set; }
    }
}
