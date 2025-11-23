namespace ControlGastosBackend.DTOs.TiposGasto
{
    public class CreateTipoGastoDto
    {
        public string Nombre { get; set; }
        public string? Descripcion { get; set; }
        public string? Color { get; set; }
    }
}
