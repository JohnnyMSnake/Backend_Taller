using Backend_Taller.DTOs;
using Backend_Taller.Models;
using Backend_Taller.Repository.Interfaces;
using Backend_Taller.Services.Interfaces;

namespace Backend_Taller.Services.Implementations
{
    public class MarcaService : IMarcaService
    {
        private readonly IMarcaRepository _marcaRepository;
        public MarcaService(IMarcaRepository marcaRepository)
        {
            _marcaRepository = marcaRepository;
        }
        public async Task<Marcas> CrearMarca(MarcaDTO marcaNueva)
        {
            var verificarMarca = await _marcaRepository.ObtenerMarcaPorNombreAsync(marcaNueva.NombreMarca);
            if (verificarMarca == null)
            {
                var marca = new Marcas();
                marca.NombreMarca = marcaNueva.NombreMarca;

                await _marcaRepository.AgregarMarcaAsync(marca);
                await _marcaRepository.GuardarCambiosAsync();
                return marca;
            }
            return verificarMarca;
        }

        public async Task<List<Marcas>> ObtenerMarcas()
        {
            var marcas = await _marcaRepository.ObtenerMarcasAsync();
            return marcas;
        }
    }
}
