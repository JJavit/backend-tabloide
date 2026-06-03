namespace tabloidetek_Backend.Models
{
    public class ArticuloCliente
    {
        public Articulo Articulo { get; set; }
        public List<string> Imagenes { get; set; }
        public List<Comentario> Comentarios { get; set; }
        public List<Reaccion> Reacciones { get; set; }
    }
}
