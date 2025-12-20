using Backend_Taller.Models;
using Backend_Taller.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Backend_Taller.Services.Implementations
{
    public class ClienteService : IClienteService
    {
        private readonly TallerDbContext _context;
        public ClienteService(TallerDbContext context)
        { 
            _context = context;
        }
        public async Task<List<Clientes>> BuscarClientes(string? rfcFisico, string? nombre, string? direccion, string? cp, string? telefono)
        {

            var query = _context.Clientes.AsQueryable();

            if (!string.IsNullOrWhiteSpace(rfcFisico))
            {
                query = query.Where(c => c.RfcFisico.Contains(rfcFisico.ToLower()));
            }
            if (!string.IsNullOrWhiteSpace(nombre))
            {
                query = query.Where(c => c.Nombre.StartsWith(nombre.ToLower()));
            }
            if (!string.IsNullOrWhiteSpace(direccion))
            {
                query = query.Where(c => c.Direccion.Contains(direccion.ToLower()));
            }
            if (!string.IsNullOrWhiteSpace(cp))
            {
                query = query.Where(c => c.Cp.Contains(cp.ToLower()));
            }
            if (!string.IsNullOrWhiteSpace(telefono))
            {
                query = query.Where(c => c.Telefono.Contains(telefono.ToLower()));
            }

            var clientes = await query.ToListAsync();
            return clientes;

        }

        public async Task<List<Clientes>> ObtenerClientes()
        {
            var clientes = await _context.Clientes.ToListAsync();
            return clientes;
        }

    }
}
