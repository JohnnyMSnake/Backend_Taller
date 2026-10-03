using Backend_Taller.Models;
using Backend_Taller.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Backend_Taller.Repository.Implementations
{
    public class VehiculoRepository : IVehiculoRepository
    {
        private readonly TallerDbContext _context;

        public VehiculoRepository(TallerDbContext context)
        {
            _context = context;
        }

        public async Task<List<Vehiculos>> ObtenerVehiculosAsync()
        {
            var vehiculos = await _context.Vehiculos.Include(M => M.Marca).ToListAsync();
            return vehiculos;
        }

        public async Task<List<Vehiculos>> BuscarVehiculosAsync(string? numeroSerie, string? placas, string? tipo, int? marcaId, string? modelo, string? numeroMotor, string? color)
        {
            var query = _context.Vehiculos.Include(M => M.Marca).AsQueryable();

            if (!string.IsNullOrWhiteSpace(numeroSerie))
            {
                query = query.Where(v => v.NumeroSerie.Contains(numeroSerie.ToLower()));
            }
            if (!string.IsNullOrWhiteSpace(placas))
            {
                query = query.Where(v => v.Placas.StartsWith(placas.ToLower()));
            }
            if (!string.IsNullOrWhiteSpace(tipo))
            {
                query = query.Where(v => v.Tipo.Contains(tipo.ToLower()));
            }
            if (marcaId.HasValue)
            {
                query = query.Where(v => v.MarcasId == marcaId);
            }
            if (!string.IsNullOrWhiteSpace(modelo))
            {
                query = query.Where(v => v.Modelo.Contains(modelo.ToLower()));
            }
            if (!string.IsNullOrWhiteSpace(numeroMotor))
            {
                query = query.Where(v => v.NumeroMotor.Contains(numeroMotor.ToLower()));
            }
            if (!string.IsNullOrWhiteSpace(color))
            {
                query = query.Where(v => v.Color.Contains(color));
            }

            var vehiculos = await query.ToListAsync();

            return vehiculos;
        }
    }
}
