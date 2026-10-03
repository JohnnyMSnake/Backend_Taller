using Backend_Taller.EFConfiguration;
using Backend_Taller.Models;
using Backend_Taller.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Backend_Taller.Repository.Implementations
{
    public class PresupuestoRepository : IPresupuestoRepository
    {
        private readonly TallerDbContext _context;

        public PresupuestoRepository(TallerDbContext context)
        {
            _context = context;
        }

        public async Task<Iva?> ObtenerIvaAsync()
        {
            var iva = await _context.Iva.SingleOrDefaultAsync();
            return iva;
        }
    }
}
