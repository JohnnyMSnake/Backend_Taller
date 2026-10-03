using Backend_Taller.DTOs;
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

        public async Task<Vehiculos> ObtenerOCrearVehiculoAsync(VehiculosDTO vehiculoDTO)
        {
            if (vehiculoDTO == null)
                throw new ArgumentNullException(nameof(vehiculoDTO));

            Vehiculos? vehiculo = null;

            if (vehiculoDTO.VehiculosId > 0)
            {
                vehiculo = await _vehiculoRepository.BuscarVehiculoByIdAsync(vehiculoDTO.VehiculosId);
            }

            if (vehiculo == null)
            {
                vehiculo = new Vehiculos
                {
                    NumeroSerie = vehiculoDTO.NumeroSerie,
                    Placas = vehiculoDTO.Placas,
                    Tipo = vehiculoDTO.Tipo,
                    MarcasId = vehiculoDTO.MarcasId,
                    Modelo = vehiculoDTO.Modelo,
                    NumeroMotor = vehiculoDTO.NumeroMotor,
                    Color = vehiculoDTO.Color
                };
                await _vehiculoRepository.AgregarVehiculoAsync(vehiculo);
            }
            else
            {
                //Actualizar vehiculo exiwstente
                ActualizarVehiculo(vehiculo, vehiculoDTO);
            }
            return vehiculo;


        }

        private static void ActualizarVehiculo(Vehiculos vehiculo, VehiculosDTO vehiculoDTO)
        {
            vehiculo.NumeroSerie = vehiculoDTO.NumeroSerie;
            vehiculo.Placas = vehiculoDTO.Placas;
            vehiculo.Tipo = vehiculoDTO.Tipo;
            vehiculo.MarcasId = vehiculoDTO.MarcasId;
            vehiculo.Modelo = vehiculoDTO.Modelo;
            vehiculo.NumeroMotor = vehiculoDTO.NumeroMotor;
            vehiculo.Color = vehiculoDTO.Color;
        }
    }
}
