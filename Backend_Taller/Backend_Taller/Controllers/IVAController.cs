using Backend_Taller.DTOs;
using Backend_Taller.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend_Taller.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class IVAController : ControllerBase
    {
        private readonly TallerDbContext _context;
        public IVAController(TallerDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetIVA()
        {
            var iva = await _context.Iva.FirstOrDefaultAsync();
            if (iva == null)
            {
                return NotFound();
            }
            return Ok(iva);
        }

        [HttpPut]
        public async Task<IActionResult> ModifyIva([FromBody] IvaDTO nuevoIva)
        {
            var iva = await _context.Iva.SingleOrDefaultAsync();

            if(iva == null)
            {
                return NotFound();
            }

            iva.IvaValue = nuevoIva.IvaValue;
            await _context.SaveChangesAsync();
            return Ok(iva);
        }
    }
}
