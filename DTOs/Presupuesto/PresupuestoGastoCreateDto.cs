namespace ControlGastosBackend.DTOs.Presupuesto
{
    public class PresupuestoGastoCreateDto
    {
        public Guid TipoGastoId { get; set; }
        public decimal Monto { get; set; }
        public decimal MontoEjecutado { get; set; }
        public string AnioMes { get; set; } = string.Empty;
    }
}
