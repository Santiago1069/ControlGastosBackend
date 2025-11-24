namespace ControlGastosBackend.DTOs.FondoMonetario
{
    public class FondoMonetarioResponseDTO
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public string Tipo { get; set; } = string.Empty;
        public decimal SaldoActual { get; set; }
    }
}
