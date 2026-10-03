using Backend_Taller.EFConfiguration;
using Backend_Taller.Models;
using Backend_Taller.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Backend_Taller.Repository.Implementations
{
    public class RfcMoralRepository : IRfcMoralRepository
    {
        private readonly TallerDbContext _context;

        public RfcMoralRepository(TallerDbContext context)
        {
            _context = context;
        }

        public async Task<List<RfcMoral>> ObtenerRfcMoralAsync()
        {
            var rfcs = await _context.RfcMorales.ToListAsync();
            return rfcs;
        }

        public async Task<List<RfcMoral>> BuscarRfcMoralAsync(string? rfcMoralValue, string? institucion)
        {
            var query = _context.RfcMorales.AsQueryable();
            if (!string.IsNullOrEmpty(rfcMoralValue))
            {
                query = query.Where(r => r.RfcMoralValue.Contains(rfcMoralValue.ToLower()));
            }
            if (!string.IsNullOrEmpty(institucion))
            {
                query = query.Where(r => r.Institucion.StartsWith(institucion.ToLower()));
            }

            var rfcs = await query.ToListAsync();
            return rfcs;
        }

        public async Task<RfcMoral> BuscarRfcMoralByIdAsync(int id)
        {
            return await _context.RfcMorales.FirstOrDefaultAsync(r => r.RfcMoralId == id);
        }

        public async Task AgregarRfcMoralAsync(RfcMoral rfcMoral)
        {
            await _context.RfcMorales.AddAsync(rfcMoral);
        }
    }
}
