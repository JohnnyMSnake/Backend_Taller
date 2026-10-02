using Backend_Taller.Models;

namespace Backend_Taller.Services.Interfaces
{
    public interface IClienteService
    {
        public Task<List<Clientes>> ObtenerClientes();

        public Task<List<Clientes>> BuscarClientes(string? rfcFisico,
                                                  string? nombre,
                                                  string? direccion,
                                                  string? cp,
                                                  string? telefono);

    }
}
