using Backend_Taller.DTOs;
using Backend_Taller.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend_Taller.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUserService _userService;
        public LoginController(IUserService userService) 
        { 
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] UsersRequestDTO userDTO)
        {

            var token = await _userService.IniciarSesion(userDTO);

            var cookie = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddMinutes(15)
            };

            Response.Cookies.Append("jwt", token, cookie);

            return Ok();
        }

        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUser([FromBody] UsersRequestDTO userDTO)
        {

            var userResponseDTO = await _userService.CrearUser(userDTO);

            return CreatedAtAction(nameof(CreateUser), new { id = userResponseDTO.Email }, userResponseDTO);
        }

    }
}
