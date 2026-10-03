using Backend_Taller.EFConfiguration;
using Backend_Taller.Models;
using Backend_Taller.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Backend_Taller.Repository.Implementations
{
    public class MarcaRepository : IMarcaRepository
    {
        private readonly TallerDbContext _context;

        public MarcaRepository(TallerDbContext context)
        {
            _context = context;
        }

        public async Task<List<Marcas>> ObtenerMarcasAsync()
        {
            var marcas = await _context.Marcas.ToListAsync();
            return marcas;
        }

        public async Task<Marcas?> ObtenerMarcaPorNombreAsync(string nombreMarca)
        {
            var marca = await _context.Marcas.FirstOrDefaultAsync(m => m.NombreMarca.ToLower() == nombreMarca.ToLower());
            return marca;
        }

        public async Task AgregarMarcaAsync(Marcas marca)
        {
            await _context.Marcas.AddAsync(marca);
        }

        public async Task GuardarCambiosAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
