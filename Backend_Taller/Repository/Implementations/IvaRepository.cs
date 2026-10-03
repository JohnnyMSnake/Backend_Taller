using Backend_Taller.Models;
using Backend_Taller.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Backend_Taller.Repository.Implementations
{
    public class IvaRepository : IIvaRepository
    {
        private readonly TallerDbContext _context;

        public IvaRepository(TallerDbContext context)
        {
            _context = context;
        }

        public async Task<Iva?> ObtenerIvaAsync()
        {
            var iva = await _context.Iva.SingleOrDefaultAsync();
            return iva;
        }

        public async Task ActualizarIvaAsync(Iva iva)
        {
            _context.Iva.Update(iva);
        }

        public async Task GuardarCambiosAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
