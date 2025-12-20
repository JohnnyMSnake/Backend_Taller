using Backend_Taller.Models;
using Backend_Taller.Services.Implementations;
using Backend_Taller.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Registro de DbContext
builder.Services.AddDbContext<TallerDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("TallerConnection"));
});

//Registro de servicios
builder.Services.AddScoped<IClienteService, ClienteService>();
builder.Services.AddScoped<IVehiculoService, VehiculoService>();
builder.Services.AddScoped<IRfcMoralService, RfcMoralService>();
builder.Services.AddScoped<IIvaService, IvaService>();
builder.Services.AddScoped<IMarcaService, MarcaService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
