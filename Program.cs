using ControlGastosBackend.Data;
using ControlGastosBackend.Repositories.Depositos;
using ControlGastosBackend.Repositories.FondoMonetario;
using ControlGastosBackend.Repositories.Movimientos;
using ControlGastosBackend.Repositories.Presupuesto;
using ControlGastosBackend.Repositories.RegistroGastoDetalleRepository;
using ControlGastosBackend.Repositories.RegistrosGasto;
using ControlGastosBackend.Repositories.TiposGasto;
using ControlGastosBackend.Services.Depositos;
using ControlGastosBackend.Services.FondoMonetario;
using ControlGastosBackend.Services.Movimientos;
using ControlGastosBackend.Services.Presupuesto;
using ControlGastosBackend.Services.RegistroGasto;
using ControlGastosBackend.Services.TiposGasto;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular",
        policy =>
        {
            policy.WithOrigins("http://localhost:4200")
                  .AllowAnyHeader()
                  .AllowAnyMethod()
                  .AllowCredentials();
        });
});

builder.Services.AddControllers();


// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 👉 Conexión SQL Server
var connectionString = builder.Configuration.GetConnectionString("ControlGastosDB");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));


// 👉 Registrar Servicios y Repositorios
builder.Services.AddScoped<TipoGastoRepository>();
builder.Services.AddScoped<TipoGastoService>();
builder.Services.AddScoped<FondoMonetarioRepository>();
builder.Services.AddScoped<FondoMonetarioService>();
builder.Services.AddScoped<PresupuestoGastoRepository>();
builder.Services.AddScoped<PresupuestoGastoService>();
builder.Services.AddScoped<RegistroGastoRepository>();
builder.Services.AddScoped<RegistroGastoService>();
builder.Services.AddScoped<RegistroGastoDetalleRepository>();
builder.Services.AddScoped<MovimientoRepository>();
builder.Services.AddScoped<MovimientoService>();
builder.Services.AddScoped<DepositoRepository>();
builder.Services.AddScoped<DepositoService>();

var app = builder.Build();

app.UseCors("AllowAngular");

// Configure pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
//app.UseAuthorization();

// Map controllers
app.MapControllers();

// Check database connection
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    try
    {
        db.Database.CanConnect();
        Console.WriteLine("Conexión a SQL Server exitosa!");
    }
    catch (Exception ex)
    {
        Console.WriteLine("No se pudo conectar: " + ex.Message);
    }
}

app.Run();
