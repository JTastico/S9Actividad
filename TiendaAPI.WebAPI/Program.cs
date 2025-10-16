using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using TiendaAPI.Application.Mappings;
using TiendaAPI.Application.Services;
using TiendaAPI.Domain.Interfaces;
using TiendaAPI.Infrastructure.Data;
using TiendaAPI.Infrastructure.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Configuración de Servicios en el Contenedor ---

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Agregamos el DbContext al contenedor de servicios, especificando que usaremos PostgreSQL.
builder.Services.AddDbContext<TiendaDbContext>(options =>
    options.UseNpgsql(connectionString));

// Agregamos los servicios para los controladores de la API.
builder.Services.AddControllers();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
// REGISTRO DEL SERVICIO DE CATEGORIA
builder.Services.AddScoped<ICategoriaService, CategoriaService>();
// Configuración de Swagger (Swashbuckle)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "MyApp API",
        Version = "v1",
        Description = "API base del proyecto con Arquitectura Hexagonal - .NET 9"
    });
});


// 'AddScoped' crea una instancia por cada petición HTTP.
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
// REGISTRO AUTOMAPPER
builder.Services.AddAutoMapper(typeof(MappingProfile));
// Agregamos los servicios de Swagger/OpenAPI para la documentación de la API.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// --- 2. Construcción de la Aplicación ---
var app = builder.Build();

// --- 3. Configuración del Pipeline de Peticiones HTTP ---

// Habilita Swagger solo en el entorno de desarrollo.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
// Mostrar Swagger solo en desarrollo (puedes quitar la condición si deseas verlo siempre)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "MyApp API v1");
        c.RoutePrefix = string.Empty; // Swagger UI en la raíz (http://localhost:5000/)
    });
}


// Redirige las peticiones HTTP a HTTPS.
app.UseHttpsRedirection();

// Agrega el middleware de autorización.
app.UseAuthorization();

// Mapea las rutas a los controladores.
app.MapControllers();

// Ejecuta la aplicación.
app.Run();