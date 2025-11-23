namespace ControlGastosBackend.DTOs.FondoMonetario
{
    public class CrearFondoMonetarioDTO
    {
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public int Tipo { get; set; }
        public decimal SaldoActual { get; set; }
    }
}
