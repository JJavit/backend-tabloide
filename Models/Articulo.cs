using System.ComponentModel.DataAnnotations;

namespace tabloidetek_Backend.Models
{
    public class Articulo
    {
        [Key]
        public int IdArticulo { get; set; }
        [Required]
        public int IdCategoria { get; set; }
        [Required]
        public string TituloArticulo { get; set; }
        [Required]
        public string Contenido { get; set; }
        [Required]
        public string Autor { get; set; }
        [Required]
        public DateTime FechaPublicacion { get; set; }
        public string? URL { get; set; }
    }
}
