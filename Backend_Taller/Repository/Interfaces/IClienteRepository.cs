using Backend_Taller.Models;

namespace Backend_Taller.Repository.Interfaces
{
    public interface IClienteRepository
    {
        Task<List<Clientes>> ObtenerClientesAsync();
        Task<List<Clientes>> BuscarClientesAsync(string? rfcFisico, string? nombre, string? direccion, string? cp, string? telefono);
        Task<Clientes> ObtenerClientePorIdAsync(int clienteId);
        Task AgregarClienteAsync(Clientes cliente);
    }
}
