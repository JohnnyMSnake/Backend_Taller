using Backend_Taller.DTOs;
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

        public async Task<Clientes> ObtenerOCrearClienteAsync(ClientesDTO clienteDTO)
        {
            if (clienteDTO == null)
                throw new ArgumentNullException(nameof(clienteDTO), "El DTO del cliente no puede ser nulo");

            Clientes? cliente = null;

            // Buscar cliente existente
            if (clienteDTO.ClientesId > 0)
            {
                cliente = await _clienteRepository.ObtenerClientePorIdAsync(clienteDTO.ClientesId);
            }

            // Crear nuevo cliente si no existe
            if (cliente == null)
            {
                cliente = new Clientes
                {
                    RfcFisico = clienteDTO.RfcFisico,
                    Nombre = clienteDTO.Nombre,
                    Direccion = clienteDTO.Direccion,
                    Cp = clienteDTO.Cp,
                    Telefono = clienteDTO.Telefono
                };
                await _clienteRepository.AgregarClienteAsync(cliente);
            }
            else
            {
                // Actualizar cliente existente
                ActualizarCliente(cliente, clienteDTO);
            }

            return cliente;
        }

        private static void ActualizarCliente(Clientes cliente, ClientesDTO clienteDTO)
        {
            cliente.RfcFisico = clienteDTO.RfcFisico;
            cliente.Nombre = clienteDTO.Nombre;
            cliente.Direccion = clienteDTO.Direccion;
            cliente.Cp = clienteDTO.Cp;
            cliente.Telefono = clienteDTO.Telefono;
        }

    }
}
