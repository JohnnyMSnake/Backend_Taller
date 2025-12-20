using Backend_Taller.DTOs;
using Backend_Taller.Models;

namespace Backend_Taller.Services.Interfaces
{
    public interface IMarcaService
    {
        Task<List<Marcas>> ObtenerMarcas();
        Task<Marcas> CrearMarca(MarcaDTO marcaNueva);
    }
}
