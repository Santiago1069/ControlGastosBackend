namespace ControlGastosBackend.DTOs
{
    public class TipoGastoResponseDto
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public string? Descripcion { get; set; }
        public string Estado { get; set; }
        public string? Color { get; set; }
    }
}
