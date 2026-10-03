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
    }
}
