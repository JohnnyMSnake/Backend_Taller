using Backend_Taller.EFConfiguration;
using Backend_Taller.Models;
using Backend_Taller.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Backend_Taller.Repository.Implementations
{
    public class OrdenRepository : IOrdenRepository
    {
        private readonly TallerDbContext _context;

        public OrdenRepository(TallerDbContext context)
        {
            _context = context;
        }

        public async Task<List<OrdenServicio>> BuscarOrdenAsync(int? ordenServicioId, string? nombre, string? telefono, string? rfcFisico, string? placas, string? numeroSerie)
        {
            var query = _context.OrdenesServicio.AsQueryable();

            query = query.Include(v => v.Vehiculo)
                         .ThenInclude(m => m.Marca)
                         .Include(c => c.Cliente)
                         .Include(rfc => rfc.RfcMoral)
                         .Include(p => p.Presupuesto)
                         .Include(s => s.Servicios);

            if (ordenServicioId.HasValue && ordenServicioId > 0)
            {
                query = query.Where(id => id.OrdenServicioId == ordenServicioId);
            }
            if (!string.IsNullOrWhiteSpace(nombre))
            {
                query = query.Where(n => n.Cliente.Nombre.StartsWith(nombre));
            }
            if (!string.IsNullOrWhiteSpace(telefono))
            {
                query = query.Where(n => n.Cliente.Telefono.Contains(telefono));
            }
            if (!string.IsNullOrWhiteSpace(rfcFisico))
            {
                query = query.Where(rfc => rfc.Cliente.RfcFisico.Contains(rfcFisico));
            }
            if (!string.IsNullOrWhiteSpace(placas))
            {
                query = query.Where(p => p.Vehiculo.Placas.StartsWith(placas));
            }
            if (!string.IsNullOrWhiteSpace(numeroSerie))
            {
                query = query.Where(ns => ns.Vehiculo.NumeroSerie.Contains(numeroSerie));
            }

            var ordenesServicio = await query.ToListAsync();

            return ordenesServicio;
        }

        public async Task<Vehiculos?> ObtenerVehiculoPorIdAsync(int vehiculoId)
        {
            return await _context.Vehiculos.FindAsync(vehiculoId);
        }

        public async Task<RfcMoral?> ObtenerRfcMoralPorIdAsync(int rfcMoralId)
        {
            return await _context.RfcMorales.FindAsync(rfcMoralId);
        }

        public async Task AgregarVehiculoAsync(Vehiculos vehiculo)
        {
            await _context.Vehiculos.AddAsync(vehiculo);
        }

        public async Task AgregarRfcMoralAsync(RfcMoral rfcMoral)
        {
            await _context.RfcMorales.AddAsync(rfcMoral);
        }

        public async Task AgregarOrdenServicioAsync(OrdenServicio ordenServicio)
        {
            _context.OrdenesServicio.Add(ordenServicio);
        }

        public async Task AgregarPresupuestoAsync(Presupuestos presupuesto)
        {
            await _context.Presupuestos.AddAsync(presupuesto);
        }

        public async Task GuardarCambiosAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await _context.Database.BeginTransactionAsync();
        }
    }
}
