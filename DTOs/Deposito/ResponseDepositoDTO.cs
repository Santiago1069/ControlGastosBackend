namespace ControlGastosBackend.DTOs.Deposito
{
    public class ResponseDepositoDTO
    {
        public Guid Id { get; set; }
        public DateTime Fecha { get; set; }
        public Guid FondoMonetarioId { get; set; }
        public string? Descripcion { get; set; }
        public decimal Monto { get; set; }
    }
}
