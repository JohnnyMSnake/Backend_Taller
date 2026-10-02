using Backend_Taller.DTOs;

namespace Backend_Taller.Services.Interfaces
{
    public interface IIvaService
    {
        Task<decimal> ObtenerIvaValue();
        Task<decimal> ModificarIva(IvaDTO nuevoIvaValue);

    }
}
