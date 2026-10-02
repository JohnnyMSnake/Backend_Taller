using Backend_Taller.DTOs;
using Backend_Taller.Models;

namespace Backend_Taller.Services.Interfaces
{
    public interface IOrdenService
    {
        Task<CrearOrdenServicioDTO> CrearOrden(CrearOrdenServicioDTO nuevaOrdenServicio);

        Task<List<OrdenServicio>> BuscarOrden(int? ordenServicioId, string? nombre, string? telefono, string? rfcFisico, string? placas, string? numeroSerie);
    }
}
