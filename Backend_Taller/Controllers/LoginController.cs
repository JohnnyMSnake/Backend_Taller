using Microsoft.AspNetCore.Mvc;

namespace Backend_Taller.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {

        public LoginController() 
        { 
        
        }

        [HttpPost]
        public async Task<IActionResult> Login()
        {
            return Ok();
        }

    }
}
