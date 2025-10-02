using Microsoft.AspNetCore.Mvc;
using ProjetoPokegochi.Data.Dtos;
using ProjetoPokegochi.Services;

namespace ProjetoPokegochi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApiUsuarioController : ControllerBase
    {
        private UsuarioService _usuarioService;

        public ApiUsuarioController(UsuarioService cadastroService)
        {
            _usuarioService = cadastroService;
        }

        [HttpPost("cadastro")]
        public async Task<IActionResult> CadastraUsuario(CreateUsuarioDto dto)
        {
            await _usuarioService.Cadastra(dto);
            return Ok("usuario cadastrado");

        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync(LoginUsuarioDto dto)
        {
            var token = await _usuarioService.Login(dto);
            return Ok(token);
        }

    }
}
