using System.ComponentModel.DataAnnotations;

namespace tabloidetek_Backend.Models
{
    public class Comentario
    {
        [Key]
        public int IdComentario { get; set; }
        [Required]
        public int IdArticulo { get; set; }
        [Required]
        public string Contenido { get; set; }
        [Required]
        public string Autor { get; set; }
        [Required]
        public DateTime FechaCreacion { get; set; }
    }
}
