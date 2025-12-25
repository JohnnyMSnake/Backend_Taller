using Backend_Taller.DTOs;

namespace Backend_Taller.Services.Interfaces
{
    public interface ICrearOrdenService
    {
        Task<CrearOrdenServicioDTO> CrearOrden(CrearOrdenServicioDTO nuevaOrdenServicio);
    }
}
