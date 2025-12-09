using Backend_Taller.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend_Taller.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class VehiculosController : ControllerBase
    {
        private readonly TallerDbContext _context;
        public VehiculosController(TallerDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetVehiculos()
        {
            var vehiculos = _context.Vehiculos.ToList();
            return Ok(vehiculos);
        }
    }
}
