using Backend_Taller.Models;

namespace Backend_Taller.Repository.Interfaces
{
    public interface IVehiculoRepository
    {
        Task<List<Vehiculos>> ObtenerVehiculosAsync();
        Task<List<Vehiculos>> BuscarVehiculosAsync(string? numeroSerie, string? placas, string? tipo, int? marcaId, string? modelo, string? numeroMotor, string? color);
    }
}
