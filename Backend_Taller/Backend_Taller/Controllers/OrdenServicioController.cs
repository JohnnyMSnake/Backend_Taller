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
        private readonly ICrearOrdenService _crearOrdenService;
        public OrdenServicioController(TallerDbContext context, ICrearOrdenService crearOrdenService)
        {
            _context = context;
            _crearOrdenService = crearOrdenService;
        }

        [HttpGet("{Id}")]
        public IActionResult GetOrdenesServicio(int Id)
        {
            var ordenServicio = _context.OrdenesServicio.Where(i => i.OrdenServicioId == Id)
                                                                .Include(v => v.Vehiculo)
                                                                .ThenInclude(m => m.Marca)
                                                                .Include(c => c.Cliente)
                                                                .Include(rfc => rfc.RfcMoral)
                                                                .Include(p => p.Presupuesto)
                                                                .Include(s => s.Servicios)
                                                                .FirstOrDefault();

            if (ordenServicio == null)
            {
                return NotFound();
            }

            return Ok(ordenServicio);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrdenServicio([FromBody] CrearOrdenServicioDTO nuevaOrdenServicio)
        {
            try
            {
                var ordenServicioCreada = await _crearOrdenService.CrearOrden(nuevaOrdenServicio);
                return Ok(ordenServicioCreada);
            }
            catch (ArgumentException argEx)
            {
                return BadRequest("Verificar presupuesto, no coincide");
            }
            catch (DbUpdateException dbEx)
            {
                return Conflict("Error al guardar en la base de datos");
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error del servidor");
            }
            
        }
    }
}
