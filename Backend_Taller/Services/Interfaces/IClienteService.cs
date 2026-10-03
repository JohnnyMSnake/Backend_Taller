using Backend_Taller.DTOs;
using Backend_Taller.Models;

namespace Backend_Taller.Services.Interfaces
{
    public interface IClienteService
    {
        Task<List<Clientes>> ObtenerClientes();

        Task<List<Clientes>> BuscarClientes(string? rfcFisico,
                                                  string? nombre,
                                                  string? direccion,
                                                  string? cp,
                                                  string? telefono);
        Task<Clientes> ObtenerOCrearClienteAsync(ClientesDTO clienteDTO);

    }
}
