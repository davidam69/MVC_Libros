namespace MVC_Libros.Models
{
    public class Libro
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El título es obligatorio.")]
        public string? Titulo { get; set; }

        [Required(ErrorMessage = "El campo Año de publicación es requerido.")]
        [Range(1800, 2025, ErrorMessage = "El año debe estar entre 1800 y 2025.")]
        public int AnioPublicacion { get; set; }

        [Required(ErrorMessage = "Debes seleccionar un autor.")]
        public int AutorId { get; set; }
        public Autor? Autor { get; set; }
        [Required(ErrorMessage = "La Url de la imágen es requerida.")]
        public string? UrlImagen { get; set; }

       
        // public string? Editorial { get; set; }
        // public string? Genero { get; set; }
        // public string? Sinopsis { get; set; }
        // public bool Disponible { get; set; } = true;
    }
}
