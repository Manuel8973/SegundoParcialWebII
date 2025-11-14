using Microsoft.EntityFrameworkCore;
using WebAPI1.Data;
using WebAPI1.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(
    options => options.UseInMemoryDatabase("GestionProyectosDB"));

// Add services to the container.

// Servicios del sistema de gestión de proyectos
builder.Services.AddScoped<TareaService>();
builder.Services.AddScoped<MiembroService>();
builder.Services.AddScoped<PrioridadService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
