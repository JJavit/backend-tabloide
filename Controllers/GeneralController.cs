using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using tabloidetek_Backend.Data;
using tabloidetek_Backend.Models;

namespace tabloidetek_Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GeneralController : Controller
    {
        private readonly MiContexto _miContexto;
        public GeneralController(MiContexto miContexto)
        {
            _miContexto = miContexto;
        }

        [HttpGet("ListadoGeneral")]
        public async Task<IActionResult> ListadoGeneral()
        {
            var listaGeneral = await _miContexto.Articulos.Where(a => a.IdCategoria.Equals(1)).ToListAsync();
            if (listaGeneral != null)
            {
                return Ok(listaGeneral);
            }
            else
            {
                return BadRequest("No se pudo obtener la lista general");
            }
        }

        [HttpPost("AgregaArticulo")]
        public async Task<IActionResult> AgregaArticulo([FromBody] Articulo articulo)
        {
            //guardar en bd
            Articulo nuevoArticulo = new Articulo();
            nuevoArticulo.IdCategoria = articulo.IdCategoria;
            nuevoArticulo.TituloArticulo = articulo.TituloArticulo;
            nuevoArticulo.Contenido = articulo.Contenido;
            nuevoArticulo.Autor = articulo.Autor;
            nuevoArticulo.FechaPublicacion = articulo.FechaPublicacion;
            nuevoArticulo.URL = articulo.URL;

            try
            {
                _miContexto.Articulos.Add(nuevoArticulo);
                await _miContexto.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(new
            {
                Message = "Articulo Guardado"
            });
        }

        [HttpGet("GetEditarArticulo/{id}")]
        public async Task<IActionResult> GetEditarArticulo(int id)
        {
            var articulo = await _miContexto.Articulos.FindAsync(id);
            if (articulo == null)
                return NotFound();

            return Ok(articulo);
        }

        [HttpPut("ActualizarArticulo/{id}")]
        public async Task<IActionResult> ActualizarArticuloArticulo(int id, [FromBody] Articulo articuloActualizado)
        {
            var existente = await _miContexto.Articulos.FindAsync(id);
            if (existente == null)
                return NotFound();

            // Update fields
            existente.IdCategoria = articuloActualizado.IdCategoria;
            existente.TituloArticulo = articuloActualizado.TituloArticulo;
            existente.Contenido = articuloActualizado.Contenido;
            existente.Autor = articuloActualizado.Autor;
            existente.FechaPublicacion = articuloActualizado.FechaPublicacion;
            existente.URL = articuloActualizado.URL;

            await _miContexto.SaveChangesAsync();
            return Ok(new { message = "Artículo actualizado" });
        }

        [HttpDelete("EliminaArticulo")]
        public async Task<IActionResult> EliminaArticulo(int idArticulo)
        {
            Articulo? articuloEliminar = await _miContexto.Articulos
                .Where(c => c.IdArticulo == idArticulo).FirstOrDefaultAsync();
            if (articuloEliminar != null)
            {
                _miContexto.Remove(articuloEliminar);
                _miContexto.SaveChanges();
                return Ok("Categoria Eliminada");
            }
            else
            {
                return BadRequest("No se encontro la categoria");
            }

        }
    }
}
