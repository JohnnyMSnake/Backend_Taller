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

        [HttpGet("Search")]
        public IActionResult SearchCliente(string? rfcFisico,
                                            string? nombre,
                                            string? direccion,
                                            string? cp,
                                            string? telefono)
        {

            var query = _context.Clientes.AsQueryable();

            if (!string.IsNullOrWhiteSpace(rfcFisico))
            {
                query = query.Where(c => c.RfcFisico.Contains(rfcFisico.ToLower()));
            }
            if (!string.IsNullOrWhiteSpace(nombre))
            {
                query = query.Where(c => c.Nombre.StartsWith(nombre.ToLower()));
            }
            if (!string.IsNullOrWhiteSpace(direccion))
            {
                query = query.Where(c => c.Direccion.Contains(direccion.ToLower()));
            }
            if (!string.IsNullOrWhiteSpace(cp))
            {
                query = query.Where(c => c.Cp.Contains(cp.ToLower()));
            }
            if (!string.IsNullOrWhiteSpace(telefono))
            {
                query = query.Where(c => c.Telefono.Contains(telefono.ToLower()));
            }

            var cliente = query.ToList();

            return Ok(cliente);
        }
    }
}
