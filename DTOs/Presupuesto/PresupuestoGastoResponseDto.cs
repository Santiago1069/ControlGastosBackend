namespace ControlGastosBackend.DTOs.Presupuesto
{
    public class PresupuestoGastoResponseDto
    {
        public Guid Id { get; set; }
        public Guid TipoGastoId { get; set; }
        public string TipoGastoNombre { get; set; }
        public decimal Monto { get; set; }
        public decimal MontoEjecutado { get; set; }
        public string AnioMes { get; set; }
    }
}
