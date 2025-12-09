using Backend_Taller.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend_Taller.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MarcasController : ControllerBase
    {
        private readonly TallerDbContext _context;
        public MarcasController(TallerDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetMarcas()
        {
            var marcas = _context.Marcas.ToList();
            return Ok(marcas);
        }
    }
}
