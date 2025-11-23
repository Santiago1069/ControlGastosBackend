using ControlGastosBackend.Data;
using ControlGastosBackend.DTOs.TiposGasto;
using ControlGastosBackend.Models.TiposGasto;
using ControlGastosBackend.Repositories.TiposGasto;

namespace ControlGastosBackend.Services.TiposGasto
{
    public class TipoGastoService
    {
        private readonly TipoGastoRepository _repository;
        private readonly ILogger<TipoGastoService> _logger;
        private readonly AppDbContext _context;

        public TipoGastoService(TipoGastoRepository repository, ILogger<TipoGastoService> logger, AppDbContext context)
        {
            _repository = repository;
            _logger = logger;
            _context = context;
        }

        public async Task<TipoGastoResponseDto> CrearTipoGastoAsync(CreateTipoGastoDto createTipoGastoDto)
        {
            _logger.LogInformation("Creating Tipo Gasto {createTipoGastoDto}", createTipoGastoDto);
            

            // validar nombre único
            if (await _repository.ExistsByNameAsync(createTipoGastoDto.Nombre))
                throw new Exception("El nombre del tipo de gasto ya existe.");

            var tipoGasto = new TipoGasto
            {
                Nombre = createTipoGastoDto.Nombre,
                Descripcion = createTipoGastoDto.Descripcion,
                Estado = EstadoTipoGasto.Activo,
                Color = createTipoGastoDto.Color
            };

            await _repository.AddAsync(tipoGasto);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Tipo Gasto {id} created successfully", tipoGasto.Id);

            return new TipoGastoResponseDto
            {
                Id = tipoGasto.Id,
                Nombre = tipoGasto.Nombre,
                Descripcion = tipoGasto.Descripcion,
                Estado = tipoGasto.Estado.ToString(),
                Color = tipoGasto.Color
            };
        }

        public async Task<List<TipoGasto>> ObtenerTodosAsync()
        {
            return await _repository.ObtenerTodos();
        }


    }
}

