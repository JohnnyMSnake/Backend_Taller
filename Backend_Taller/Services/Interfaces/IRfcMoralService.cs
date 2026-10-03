using Backend_Taller.DTOs;
using Backend_Taller.Models;

namespace Backend_Taller.Services.Interfaces
{
    public interface IRfcMoralService
    {
        Task<List<RfcMoral>> ObtenerRfcMoral();

        Task<List<RfcMoral>> BuscarRfcMoral(string? rfcMoralValue, string? institucion);

        Task<RfcMoral> ObtenerOCrearRfcMoral(RfcMoralDTO rfcMoralDTO);

    }
}
