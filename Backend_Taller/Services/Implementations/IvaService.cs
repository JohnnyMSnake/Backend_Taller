using Backend_Taller.DTOs;
using Backend_Taller.Models;
using Backend_Taller.Repository.Interfaces;
using Backend_Taller.Services.Interfaces;

namespace Backend_Taller.Services.Implementations
{
    public class IvaService : IIvaService
    {
        private readonly IIvaRepository _ivaRepository;
        public IvaService(IIvaRepository ivaRepository) 
        { 
            _ivaRepository = ivaRepository;
        }
        public async Task<decimal> ModificarIva(IvaDTO nuevoIvaValue)
        {
            var iva = await _ivaRepository.ObtenerIvaAsync();
            if (iva != null)
            {
                iva.IvaValue = nuevoIvaValue.IvaValue;
                await _ivaRepository.ActualizarIvaAsync(iva);
                await _ivaRepository.GuardarCambiosAsync();
                return iva.IvaValue;
            }
            return 0;
        }

        public async Task<decimal> ObtenerIvaValue()
        {
            var iva = await _ivaRepository.ObtenerIvaAsync();
            if (iva == null)
            {
                // Tecnicamente nunca deberia de llegar aqui, pero por si al caso lo dejo en que devuelva 0, para que no rompa la aplicacion
                return 0;
            }
            return iva.IvaValue;
        }
    }
}
