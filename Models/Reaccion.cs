using System.ComponentModel.DataAnnotations;

namespace tabloidetek_Backend.Models
{
    public class Reaccion
    {
        [Key]
        public int IdReaccion { get; set; }
        [Required]
        public int IdArticulo { get; set; }
        [Required]
        public string Tipo { get; set; }
        [Required]
        public string Usuario { get; set; }
        [Required]
        public DateTime FechaCreacion { get; set; }
    }
}
