using Backend_Taller.Models;

namespace Backend_Taller.Repository.Interfaces
{
    public interface IIvaRepository
    {
        Task<Iva?> ObtenerIvaAsync();
        Task ActualizarIvaAsync(Iva iva);
        Task GuardarCambiosAsync();
    }
}
