using ControlGastosBackend.Data;
using ControlGastosBackend.Repositories.FondoMonetario;
using ControlGastosBackend.Repositories.Presupuesto;
using ControlGastosBackend.Repositories.TiposGasto;
using ControlGastosBackend.Services.FondoMonetario;
using ControlGastosBackend.Services.Presupuesto;
using ControlGastosBackend.Services.TiposGasto;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

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

var app = builder.Build();

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
