using Backend_Taller.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend_Taller.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RfcMoralController : ControllerBase
    {
        private readonly TallerDbContext _context;
        public RfcMoralController(TallerDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetRfcs()
        {
            var rfcs = _context.RfcMorales.ToList();
            return Ok(rfcs);
        }
    }
}
