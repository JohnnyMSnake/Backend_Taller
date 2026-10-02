using Backend_Taller.Models;
using Backend_Taller.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend_Taller.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class VehiculosController : ControllerBase
    {
        private readonly IVehiculoService _vehiculoService;
        public VehiculosController(IVehiculoService vehiculoService)
        {
            _vehiculoService = vehiculoService;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerVehiculos()
        {
            var vehiculos = await _vehiculoService.ObtenerVehiculos();
            return Ok(vehiculos);
        }

        [HttpGet("Search")]
        public async Task<IActionResult> BuscarVehiculos(string? numeroSerie,  
                                        string? placas, 
                                        string? tipo,
                                        int? marcaId,
                                        string? modelo,
                                        string? numeroMotor,
                                        string? color)
        {
            
            var vehiculos = await _vehiculoService.BuscarVehiculos(numeroSerie, placas, tipo, marcaId, modelo, numeroMotor, color);

            return Ok(vehiculos);
        }

    }
}
