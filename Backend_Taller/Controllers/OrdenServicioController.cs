using Backend_Taller.DTOs;
using Backend_Taller.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace Backend_Taller.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OrdenServicioController : ControllerBase
    {
        private readonly IOrdenService _ordenService;
        public OrdenServicioController(IOrdenService OrdenService)
        { 
            _ordenService = OrdenService;
        }

        [HttpGet]
        public async Task<IActionResult> GetOrdenesServicio(int? ordenServicioId, string? nombre, string? telefono, string? rfcFisico, string? placas, string? numeroSerie)
        {
            
            var ordenesServicio = await _ordenService.BuscarOrden(ordenServicioId, nombre, telefono, rfcFisico, placas, numeroSerie);

            return Ok(ordenesServicio);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrdenServicio([FromBody] CrearOrdenServicioDTO nuevaOrdenServicio)
        {
            
                var ordenServicioCreada = await _ordenService.CrearOrden(nuevaOrdenServicio);
                return Ok(ordenServicioCreada);
            
            
        }
    }
}
