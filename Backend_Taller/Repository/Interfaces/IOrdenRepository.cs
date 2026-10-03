using Backend_Taller.Models;
using Microsoft.EntityFrameworkCore.Storage;

namespace Backend_Taller.Repository.Interfaces
{
    public interface IOrdenRepository
    {
        Task<List<OrdenServicio>> BuscarOrdenAsync(int? ordenServicioId, string? nombre, string? telefono, string? rfcFisico, string? placas, string? numeroSerie);
        Task<Clientes?> ObtenerClientePorIdAsync(int clienteId);
        Task<Vehiculos?> ObtenerVehiculoPorIdAsync(int vehiculoId);
        Task<RfcMoral?> ObtenerRfcMoralPorIdAsync(int rfcMoralId);
        Task AgregarClienteAsync(Clientes cliente);
        Task AgregarVehiculoAsync(Vehiculos vehiculo);
        Task AgregarRfcMoralAsync(RfcMoral rfcMoral);
        Task AgregarOrdenServicioAsync(OrdenServicio ordenServicio);
        Task AgregarPresupuestoAsync(Presupuestos presupuesto);
        Task GuardarCambiosAsync();
        Task<IDbContextTransaction> BeginTransactionAsync();
    }
}
