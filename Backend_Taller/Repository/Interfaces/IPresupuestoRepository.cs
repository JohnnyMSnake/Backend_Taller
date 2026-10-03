using Backend_Taller.Models;

namespace Backend_Taller.Repository.Interfaces
{
    public interface IPresupuestoRepository
    {
        Task<Iva?> ObtenerIvaAsync();
    }
}
