using Backend_Taller.DTOs;
using Backend_Taller.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Backend_Taller.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IVAController : ControllerBase
    {
        private readonly IIvaService _ivaService;
        public IVAController(IIvaService ivaService)
        {
            _ivaService = ivaService;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerIVA()
        {
            var iva = await _ivaService.ObtenerIvaValue();

            return Ok(iva);
        }

        [HttpPut]
        public async Task<IActionResult> ModificarIva([FromBody] IvaDTO nuevoIva)
        {
            var iva = await _ivaService.ModificarIva(nuevoIva);
            return Ok(iva);
        }
    }
}
