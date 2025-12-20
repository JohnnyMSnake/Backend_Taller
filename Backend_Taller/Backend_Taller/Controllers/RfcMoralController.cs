using Backend_Taller.Models;
using Backend_Taller.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend_Taller.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RfcMoralController : ControllerBase
    {
        private readonly IRfcMoralService _rfcMoralService;
        public RfcMoralController(IRfcMoralService rfcMoralService)
        {
            _rfcMoralService = rfcMoralService;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerRfcs()
        {
            var rfcs = await _rfcMoralService.ObtenerRfcMoral();
            return Ok(rfcs);
        }

        [HttpGet("Search")]
        public async Task<IActionResult> BusacrRfcMoral(string? rfcMoralValue, string? institucion)
        {
            var rfcs = await _rfcMoralService.BuscarRfcMoral(rfcMoralValue, institucion);
            return Ok(rfcs);
        }
    }
}
