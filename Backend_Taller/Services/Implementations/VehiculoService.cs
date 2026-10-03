using Backend_Taller.Models;
using Backend_Taller.Repository.Interfaces;
using Backend_Taller.Services.Interfaces;

namespace Backend_Taller.Services.Implementations
{
    public class VehiculoService : IVehiculoService
    {
        private readonly IVehiculoRepository _vehiculoRepository;
        public VehiculoService(IVehiculoRepository vehiculoRepository)
        {
            _vehiculoRepository = vehiculoRepository;
        }
        public async Task<List<Vehiculos>> BuscarVehiculos(string? numeroSerie, string? placas, string? tipo, int? marcaId, string? modelo, string? numeroMotor, string? color)
        {
            var vehiculos = await _vehiculoRepository.BuscarVehiculosAsync(numeroSerie, placas, tipo, marcaId, modelo, numeroMotor, color);
            return vehiculos;
        }

        public async Task<List<Vehiculos>> ObtenerVehiculos()
        {
            var vehiculos = await _vehiculoRepository.ObtenerVehiculosAsync();
            return vehiculos;
        }
    }
}
