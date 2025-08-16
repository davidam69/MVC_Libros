namespace MVC_Libros.Models
{
    public class Autor
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El nombre del autor es obligatorio.")]
        public string? Nombre { get; set; } = string.Empty;

        public ICollection<Libro> Libros { get; set; } = new List<Libro>();
    }
}
