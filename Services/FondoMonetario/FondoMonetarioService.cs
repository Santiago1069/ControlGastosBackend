using ControlGastosBackend.Data;
using ControlGastosBackend.DTOs.FondoMonetario;
using ControlGastosBackend.Models.FondoMonetario;
using ControlGastosBackend.Repositories.FondoMonetario;
using Microsoft.EntityFrameworkCore;

namespace ControlGastosBackend.Services.FondoMonetario
{
    public class FondoMonetarioService
    {
        private readonly FondoMonetarioRepository _fondoMonetarioRepository;
        private readonly AppDbContext _context;

        public FondoMonetarioService(FondoMonetarioRepository fondoMonetarioRepository, AppDbContext context)
        {
            _fondoMonetarioRepository = fondoMonetarioRepository;
            _context = context;
        }

        public async Task<FondosMonetario> CrearAsync(CrearFondoMonetarioDTO crearFondoMonetarioDTO)
        {
            if (await _fondoMonetarioRepository.ExisteNombreAsync(crearFondoMonetarioDTO.Nombre))
            {
                throw new Exception("Ya existe un fondo monetario con ese nombre.");
            }

            var nuevoFondo = new FondosMonetario
            {
                Nombre = crearFondoMonetarioDTO.Nombre,
                Descripcion = crearFondoMonetarioDTO.Descripcion,
                Tipo = (TipoFondo)crearFondoMonetarioDTO.Tipo,
                SaldoActual = crearFondoMonetarioDTO.SaldoActual
            };

            await _fondoMonetarioRepository.CrearAsync(nuevoFondo);
            await _context.SaveChangesAsync();

            return nuevoFondo;

        }

        public async Task<List<FondosMonetario>> ObtenerFondos()
        {
            return await _fondoMonetarioRepository.GetAllFondos();
        }
    }
}
