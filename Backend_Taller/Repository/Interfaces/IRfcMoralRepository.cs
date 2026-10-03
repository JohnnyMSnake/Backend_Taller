using Backend_Taller.Models;

namespace Backend_Taller.Repository.Interfaces
{
    public interface IRfcMoralRepository
    {
        Task<List<RfcMoral>> ObtenerRfcMoralAsync();
        Task<List<RfcMoral>> BuscarRfcMoralAsync(string? rfcMoralValue, string? institucion);
        Task<RfcMoral> BuscarRfcMoralByIdAsync(int id);
        Task AgregarRfcMoralAsync(RfcMoral rfcMoral);
    }
}
