using Backend_Taller.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend_Taller.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly TallerDbContext _context;
        public ClientesController(TallerDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetClientes()
        {
            var clientes = _context.Clientes.ToList();

            return Ok(clientes);
        }
    }
}
