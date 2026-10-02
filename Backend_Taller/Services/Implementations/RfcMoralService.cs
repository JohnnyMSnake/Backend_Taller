using Backend_Taller.Models;
using Backend_Taller.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Backend_Taller.Services.Implementations
{
    public class RfcMoralService : IRfcMoralService
    {
        private readonly TallerDbContext _context;
        public RfcMoralService(TallerDbContext context) 
        {
            _context = context;
        }
        public async Task<List<RfcMoral>> BuscarRfcMoral(string? rfcMoralValue, string? institucion)
        {
            var query = _context.RfcMorales.AsQueryable();
            if(!string.IsNullOrEmpty(rfcMoralValue))
            {
                query = query.Where(r => r.RfcMoralValue.Contains(rfcMoralValue.ToLower()));
            }
            if(!string.IsNullOrEmpty(institucion))
            {
                query = query.Where(r => r.Institucion.StartsWith(institucion.ToLower()));
            }
            
            var rfcs= await query.ToListAsync();
            return rfcs;
        }

        public async Task<List<RfcMoral>> ObtenerRfcMoral()
        {
            var rfcs = await _context.RfcMorales.ToListAsync();
            return rfcs;
        }
    }
}
