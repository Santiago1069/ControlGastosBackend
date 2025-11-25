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

        public async Task<FondoMonetarioResponseDTO> CrearAsync(CrearFondoMonetarioDTO crearFondoMonetarioDTO)
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

            return new FondoMonetarioResponseDTO
            {
                Id = nuevoFondo.Id,
                Nombre = nuevoFondo.Nombre,
                Descripcion = nuevoFondo.Descripcion,
                Tipo = nuevoFondo.Tipo.ToString(),
                SaldoActual = nuevoFondo.SaldoActual
            };
        }


        public async Task<List<FondoMonetarioResponseDTO>> ObtenerFondos()
        {
            var fondos = await _fondoMonetarioRepository.GetAllFondos();

            return fondos.Select(f => new FondoMonetarioResponseDTO
            {
                Id = f.Id,
                Nombre = f.Nombre,
                Descripcion = f.Descripcion,
                Tipo = f.Tipo.ToString(),
                SaldoActual = f.SaldoActual
            }).ToList();
        }

        public async Task<FondoMonetarioResponseDTO> FondoMonetarioById(Guid fondoId)
        {
            var fondo = await _fondoMonetarioRepository.ObtenerPorIdAsync(fondoId);
            if (fondo == null)
                throw new Exception("Fondo monetario no encontrado.");
            return new FondoMonetarioResponseDTO
            {
                Id = fondo.Id,
                Nombre = fondo.Nombre,
                Descripcion = fondo.Descripcion,
                Tipo = fondo.Tipo.ToString(),
                SaldoActual = fondo.SaldoActual
            };
        }

    }
}
