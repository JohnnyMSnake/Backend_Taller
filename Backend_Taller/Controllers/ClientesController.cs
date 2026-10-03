using Backend_Taller.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Backend_Taller.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteService _clienteService;
        public ClientesController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        [HttpGet]
        public async Task<IActionResult> GetClientes()
        {
            var clientes = await _clienteService.ObtenerClientes();

            return Ok(clientes);
        }

        [HttpGet("Search")]
        public async Task<IActionResult> SearchCliente(string? rfcFisico,
                                            string? nombre,
                                            string? direccion,
                                            string? cp,
                                            string? telefono)
        {

            
            var clientes = await _clienteService.BuscarClientes(rfcFisico,
                                                        nombre,
                                                        direccion,
                                                        cp,
                                                        telefono);
            return Ok(clientes);
        }
    }
}
