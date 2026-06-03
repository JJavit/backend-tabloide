using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using tabloidetek_Backend.Data;

namespace tabloidetek_Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuariosController : Controller
    {
        private readonly MiContexto _miContexto;
        public UsuariosController(MiContexto miContexto)
        {
            _miContexto = miContexto;
        }

        [HttpGet("DameUsuarios")]
        public async Task<IActionResult> DameUsuarios()
        {
            var listaUsuarios = await _miContexto.Usuarios.ToListAsync();
            if (listaUsuarios != null)
            {
                return Ok(listaUsuarios);
            }
            else
            {
                return BadRequest("No se pudo obtener la lista de usuarios");
            }
        }
    }
}
