using Backend_Taller.DTOs;
using Backend_Taller.Models;
using Backend_Taller.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Backend_Taller.Services.Implementations
{
    public class IvaService : IIvaService
    {
        private readonly TallerDbContext _context;
        public IvaService(TallerDbContext context) 
        { 
            _context = context;
        }
        public async Task<decimal> ModificarIva(IvaDTO nuevoIvaValue)
        {
            var iva = await _context.Iva.SingleOrDefaultAsync();
            iva.IvaValue = nuevoIvaValue.IvaValue;
            await _context.SaveChangesAsync();
            return iva.IvaValue;
        }

        public async Task<decimal> ObtenerIvaValue()
        {
            var iva = await _context.Iva.SingleOrDefaultAsync();
            if(iva == null)
            {
                //Tecnicamente nunca deberia de llegar aqui, pero por si al caso y evitar que haga cosas raras
                return 0;
            }
            return iva.IvaValue;
        }
    }
}
