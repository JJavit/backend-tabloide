using System.ComponentModel.DataAnnotations;

namespace tabloidetek_Backend.Models
{
    public class Usuario
    {
        [Key]
        [Required]
        public int IdUsuario { get; set; }
        [Required]
        public string NombreUsuario { get; set; }
        [Required]
        public string CorreoUsuario { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string TipoUsuario { get; set; }
        [Required]
        public bool Activo { get; set; }
    }
}
