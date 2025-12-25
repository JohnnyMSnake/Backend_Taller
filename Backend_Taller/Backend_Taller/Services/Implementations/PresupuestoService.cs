using Backend_Taller.DTOs;
using Backend_Taller.Models;
using Backend_Taller.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Backend_Taller.Services.Implementations
{
    public class PresupuestoService : IPresupuestoService
    {
        private readonly TallerDbContext _context;
        public PresupuestoService(TallerDbContext context)
        {
            _context = context;
        }
        public async Task<bool> VerificarPresupuesto(PresupuestosDTO presupuesto)
        {
            decimal subtotalCalculado = 0m;
            decimal ivaCalculado = 0m;
            decimal totalCalculado = 0m;
            decimal restaCalculada = 0m;

            subtotalCalculado = presupuesto.ManoObra.GetValueOrDefault() +
                   presupuesto.Refacciones.GetValueOrDefault() +
                   presupuesto.OtrosMateriales.GetValueOrDefault() +
                   presupuesto.CargosAdicionales.GetValueOrDefault() +
                   presupuesto.Seguro.GetValueOrDefault();

            if (presupuesto.Subtotal.GetValueOrDefault() != subtotalCalculado)
            {
                return false;
            }

            var iva = await _context.Iva.SingleOrDefaultAsync();

            if (iva != null)
            {
                ivaCalculado = 0m * iva.IvaValue;
            }

            if (presupuesto.IVA.GetValueOrDefault() != ivaCalculado)
            {
                return false;
            }

            totalCalculado = 0m + ivaCalculado;

            if (presupuesto.Total.GetValueOrDefault() != totalCalculado)
            {
                return false;
            }

            restaCalculada = totalCalculado - presupuesto.Anticipo.GetValueOrDefault();

            if (presupuesto.Resta.GetValueOrDefault() != restaCalculada)
            {
                return false;
            }

            return true;
        }
    }
}
