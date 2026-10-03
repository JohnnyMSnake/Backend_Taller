using Backend_Taller.DTOs;
using Backend_Taller.Models;
using Backend_Taller.Repository.Interfaces;
using Backend_Taller.Services.Interfaces;

namespace Backend_Taller.Services.Implementations
{
    public class RfcMoralService : IRfcMoralService
    {
        private readonly IRfcMoralRepository _rfcMoralRepository;
        public RfcMoralService(IRfcMoralRepository rfcMoralRepository) 
        {
            _rfcMoralRepository = rfcMoralRepository;
        }
        public async Task<List<RfcMoral>> BuscarRfcMoral(string? rfcMoralValue, string? institucion)
        {
            var rfcs = await _rfcMoralRepository.BuscarRfcMoralAsync(rfcMoralValue, institucion);
            return rfcs;
        }

        public async Task<List<RfcMoral>> ObtenerRfcMoral()
        {
            var rfcs = await _rfcMoralRepository.ObtenerRfcMoralAsync();
            return rfcs;
        }

        public async Task<RfcMoral> ObtenerOCrearRfcMoral(RfcMoralDTO rfcMoralDTO)
        {
            if (rfcMoralDTO == null)
                throw new ArgumentNullException(nameof(rfcMoralDTO));

            var rfcMoral = _rfcMoralRepository.BuscarRfcMoralByIdAsync(rfcMoralDTO.RfcMoralId).Result;

            
            if (rfcMoral == null)
            {
                // Create a new RfcMoral
                rfcMoral = new RfcMoral
                {
                    RfcMoralId = rfcMoralDTO.RfcMoralId,
                    RfcMoralValue = rfcMoralDTO.RfcMoralValue,
                    Institucion = rfcMoralDTO.Institucion
                };
                await _rfcMoralRepository.AgregarRfcMoralAsync(rfcMoral);
            } 
            else
            {
                ActualizarRfcMoral(rfcMoral, rfcMoralDTO);
            }

            return rfcMoral;
        }

        private static void ActualizarRfcMoral(RfcMoral rfcMoral, RfcMoralDTO rfcMoralDTO)
        {
            rfcMoral.RfcMoralValue = rfcMoralDTO.RfcMoralValue;
            rfcMoral.Institucion = rfcMoralDTO.Institucion;
        }
    }
}
