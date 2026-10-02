using Backend_Taller.DTOs;
using Backend_Taller.Models;
using Backend_Taller.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend_Taller.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OrdenServicioController : ControllerBase
    {
        private readonly TallerDbContext _context;
        private readonly IOrdenService _ordenService;
        public OrdenServicioController(TallerDbContext context, IOrdenService OrdenService)
        {
            _context = context;
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
