using Backend_Taller.DTOs;
using Backend_Taller.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Backend_Taller.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarcasController : ControllerBase
    {
        private readonly IMarcaService _marcaService;
        public MarcasController(IMarcaService marcaService)
        {
            _marcaService = marcaService;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerMarcas()
        {
            var marcas = await _marcaService.ObtenerMarcas();
            return Ok(marcas);
        }

        [HttpPost]
        public async Task<IActionResult> CrearMarca([FromBody] MarcaDTO marcaNueva)
        {
            var marca = await _marcaService.CrearMarca(marcaNueva);
            return Ok(marca);
        }
    }
}
