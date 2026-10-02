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
            //verificar si la maraca a agregar ya existe
            var verificarMarca = await _context.Marcas.FirstOrDefaultAsync(m => m.NombreMarca.ToLower() == marcaNueva.NombreMarca.ToLower());
            if (verificarMarca == null)
            {
                var marca = new Marcas();
                marca.NombreMarca = marcaNueva.NombreMarca;

                await _context.Marcas.AddAsync(marca);
                await _context.SaveChangesAsync();
                return (marca);
            }
            // Si la marca ya existe, devolver la marca existente
            return (verificarMarca);
        }

        public async Task<List<Marcas>> ObtenerMarcas()
        {
            var marcas = await _context.Marcas.ToListAsync();
            return marcas;
        }
    }
}
