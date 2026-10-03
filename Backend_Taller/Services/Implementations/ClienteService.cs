using Backend_Taller.Models;
using Backend_Taller.Repository.Interfaces;
using Backend_Taller.Services.Interfaces;

namespace Backend_Taller.Services.Implementations
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _clienteRepository;
        public ClienteService(IClienteRepository clienteRepository)
        { 
            _clienteRepository = clienteRepository;
        }
        public async Task<List<Clientes>> BuscarClientes(string? rfcFisico, string? nombre, string? direccion, string? cp, string? telefono)
        {

            var clientes = await _clienteRepository.BuscarClientesAsync(rfcFisico, nombre, direccion, cp, telefono);

            return clientes;

        }

        public async Task<List<Clientes>> ObtenerClientes()
        {
            var clientes = await _clienteRepository.ObtenerClientesAsync();
            return clientes;
        }

    }
}
