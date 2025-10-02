using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ProjetoPokegochi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AcessoController : ControllerBase
    {
        [HttpGet]
        [Authorize(Policy = "IdadeMinima")]
        public IActionResult Get()
        {
            return Ok("Acesso autorizado");
        }
    }
}
