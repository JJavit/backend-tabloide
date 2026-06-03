using Microsoft.EntityFrameworkCore;
using tabloidetek_Backend.Models;

//ORM - Object Relational Mapper

namespace tabloidetek_Backend.Data
{
    public class MiContexto : DbContext
    {
        public MiContexto(DbContextOptions<MiContexto> options) : base(options) { }

        //Mapeo de ORM --- Modelo < > Tabla de BD 
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Articulo> Articulos { get; set; }
        public DbSet<Comentario> Comentarios { get; set; }
        public DbSet<Reaccion> Reacciones { get; set; }
    }
}
