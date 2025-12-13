using Backend_Taller.DTOs;
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

        [HttpPost]
        public IActionResult CreateMarca([FromBody] MarcaDTO marcaNueva)
        {
            Marcas marca = new Marcas();
            marca.NombreMarca = marcaNueva.NombreMarca;
            _context.Marcas.Add(marca);
            _context.SaveChanges();
            return Ok(marca);
        }
    }
}
