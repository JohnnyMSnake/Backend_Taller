using Backend_Taller.DTOs;
using Backend_Taller.Repository.Interfaces;
using Backend_Taller.Services.Interfaces;

namespace Backend_Taller.Services.Implementations
{
    public class PresupuestoService : IPresupuestoService
    {
        private readonly IPresupuestoRepository _presupuestoRepository;
        public PresupuestoService(IPresupuestoRepository presupuestoRepository)
        {
            _presupuestoRepository = presupuestoRepository;
        }
        public async Task<bool> VerificarPresupuesto(PresupuestosDTO presupuesto)
        {
            decimal subtotalCalculado = 0m;
            decimal ivaCalculado = 0m;
            decimal totalCalculado = 0m;
            decimal restaCalculada = 0m;

            subtotalCalculado = presupuesto.ManoObra +
                   presupuesto.Refacciones +
                   presupuesto.OtrosMateriales +
                   presupuesto.CargosAdicionales +
                   presupuesto.Seguro;

            if (presupuesto.Subtotal != subtotalCalculado)
            {
                return false;
            }

            var iva = await _presupuestoRepository.ObtenerIvaAsync();

            if (iva != null)
            {
                ivaCalculado = subtotalCalculado * iva.IvaValue;
            }

            if (presupuesto.IVA != ivaCalculado)
            {
                return false;
            }

            totalCalculado = subtotalCalculado + ivaCalculado;

            if (presupuesto.Total != totalCalculado)
            {
                return false;
            }

            restaCalculada = totalCalculado - presupuesto.Anticipo;

            if (presupuesto.Resta != restaCalculada)
            {
                return false;
            }

            return true;
        }
    }
}
