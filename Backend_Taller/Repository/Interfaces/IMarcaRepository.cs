using Backend_Taller.Models;

namespace Backend_Taller.Repository.Interfaces
{
    public interface IMarcaRepository
    {
        Task<List<Marcas>> ObtenerMarcasAsync();
        Task<Marcas?> ObtenerMarcaPorNombreAsync(string nombreMarca);
        Task AgregarMarcaAsync(Marcas marca);
        Task GuardarCambiosAsync();
    }
}
