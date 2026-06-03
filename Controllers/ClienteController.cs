using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using tabloidetek_Backend.Data;
using tabloidetek_Backend.Models;

namespace tabloidetek_Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClienteController : Controller
    {

        private readonly MiContexto _miContexto;
        public ClienteController(MiContexto miContexto)
        {
            _miContexto = miContexto;
        }

        [HttpGet("ListadoGeneral")]
        public async Task<IActionResult> Listado([FromQuery]int idCategoria = 1)
        {
            var listaArticulos = await _miContexto.Articulos.Where(a => a.IdCategoria.Equals(idCategoria)).OrderByDescending(a => a.FechaPublicacion).Take(20).ToListAsync();

            List<ArticuloCliente> articulosCliente = new List<ArticuloCliente>();
            if (listaArticulos != null)
            {
                foreach (var item in listaArticulos)
                {
                    List<string> imagenes = new List<string>();
                    item.Contenido = ExtraerImagenesDeContenido(item.Contenido, out imagenes);
                    articulosCliente.Add(new ArticuloCliente
                    {
                        Articulo = item,
                        Comentarios = await _miContexto.Comentarios.Where(a => a.IdArticulo.Equals(item.IdArticulo)).ToListAsync(),
                        Reacciones = await _miContexto.Reacciones.Where(a => a.IdArticulo.Equals(item.IdArticulo)).ToListAsync(),
                        Imagenes = imagenes
                    });
                }
                return Ok(articulosCliente);
            }
            else
            {
                return BadRequest("No se pudo obtener la lista de articulos");
            }
        }

        [HttpPost("ToggleReaccion")]
        public async Task<IActionResult> ToggleReaccion(int idArticulo, string tipo, string nombreUsuario)
        {
            var tipoLimpio = tipo.ToLower().Trim();
            var nombreUsuarioLimpio = nombreUsuario.ToLower().Replace("script", "scr").Replace("iframe", "ifrm").Trim();
            var reaccion = await _miContexto.Reacciones.Where(
                r => r.Tipo.ToLower().Trim().Equals(tipoLimpio)
                && r.Usuario.ToLower().Trim().Equals(nombreUsuarioLimpio)
                && r.IdArticulo.Equals(idArticulo)).FirstOrDefaultAsync();

            if (reaccion != null)
            {
                _miContexto.Remove(reaccion);
                _miContexto.SaveChanges();
                return Ok("Reaccion Eliminada");
            }
            else
            {
                try
                {
                    Reaccion nuevaReaccion = new Reaccion()
                    {
                        IdArticulo = idArticulo,
                        Tipo = tipoLimpio,
                        Usuario = nombreUsuarioLimpio,
                        FechaCreacion = System.DateTime.Now,

                    };
                    _miContexto.Reacciones.Add(nuevaReaccion);
                    await _miContexto.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }

                return Ok(new
                {
                    Message = "Reaccion Guardada"
                });
            }
        }

        [HttpPost("AgregaComentario")]
        public async Task<IActionResult> AgregaComentario(int idArticulo, string contenido, string autor)
        {
            var contenidoLimpio = contenido.Trim().Replace("script", "scr").Replace("iframe", "ifrm");
            var autorLimpio = autor.ToLower().Replace("script", "scr").Replace("iframe", "ifrm").Trim();

            try
            {
                Comentario nuevoComentario = new Comentario()
                {
                    IdArticulo = idArticulo,
                    Contenido = contenido,
                    Autor = autor,
                    FechaCreacion = System.DateTime.Now,
                };
                _miContexto.Comentarios.Add(nuevoComentario);
                await _miContexto.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(new
            {
                Message = "Comentario Guardado"
            });
        }

        private string ExtraerImagenesDeContenido(string contenido, out List<string> imagenes)
        {
            imagenes = new List<string>();

            // Patron para buscar imagenes en el string contenido
            string base64Pattern = @"<img[^>]*src\s*=\s*""(data:image\/[^;]+;base64,[^""]+)""[^>]*>";

            // Lista de imagenes
            List<string> base64Images = new List<string>();

            // Reemplaza imagenes base64 con strings vacios.
            string contenidoSinImagenes = Regex.Replace(contenido, base64Pattern, match =>
            {
                string base64Data = match.Groups[1].Value;
                base64Images.Add(base64Data);
                return ""; // quita la imagen del texto
            }, RegexOptions.IgnoreCase);

            imagenes = base64Images;
            return contenidoSinImagenes;
        }
    }
}
