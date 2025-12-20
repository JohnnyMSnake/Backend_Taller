using Backend_Taller.DTOs;
using Backend_Taller.Models;
using Backend_Taller.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Backend_Taller.Services.Implementations
{
    public class MarcaService : IMarcaService
    {
        private readonly TallerDbContext _context;
        public MarcaService(TallerDbContext context)
        {
            _context = context;
        }
        public async Task<Marcas> CrearMarca(MarcaDTO marcaNueva)
        {
            var marca = new Marcas();
            marca.NombreMarca = marcaNueva.NombreMarca;

            await _context.Marcas.AddAsync(marca);
            await _context.SaveChangesAsync();

            return(marca);
        }

        public async Task<List<Marcas>> ObtenerMarcas()
        {
            var marcas = await _context.Marcas.ToListAsync();
            return marcas;
        }
    }
}
