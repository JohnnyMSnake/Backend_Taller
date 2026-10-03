using Backend_Taller.DTOs;
using Backend_Taller.Models;

namespace Backend_Taller.Services.Interfaces
{
    public interface IVehiculoService
    {
        Task<List<Vehiculos>> ObtenerVehiculos();

        Task<List<Vehiculos>> BuscarVehiculos(
            string? numeroSerie,
            string? placas,
            string? tipo,
            int? marcaId,
            string? modelo,
            string? numeroMotor,
            string? color);

        Task<Vehiculos> ObtenerOCrearVehiculoAsync(VehiculosDTO vehiculo);
    }
}
