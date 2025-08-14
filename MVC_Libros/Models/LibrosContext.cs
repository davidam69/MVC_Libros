namespace MVC_Libros.Models
{
    public class LibrosContext : DbContext
    {
        public DbSet<Libro> Libros { get; set; }
        public DbSet<Autor> Autores { get; set; }

        public LibrosContext(DbContextOptions<LibrosContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Autor>()
                .HasData(
                    new Autor { Id = 1, Nombre = "George Orwell" },
                    new Autor { Id = 2, Nombre = "Ray Bradbury" }
                );

            modelBuilder.Entity<Libro>()
                .HasData(
                    new Libro { Id = 1, Titulo = "1984", AnioPublicacion = 1949, AutorId = 1, UrlImagen = "https://proassetspdlcom.cdnstatics2.com/usuaris/libros/thumbs/b59f709a-ce74-4e50-a3a3-3527d68da121/d_360_620/portada_1984_george-orwell_202102151044.webp" },
                    new Libro { Id = 2, Titulo = "Fahrenheit 451", AnioPublicacion = 1953, AutorId = 2, UrlImagen = "https://upload.wikimedia.org/wikipedia/en/d/db/Fahrenheit_451_1st_ed_cover.jpg" },
                    new Libro { Id = 3, Titulo = "Rebelion en la granja", AnioPublicacion = 1945, AutorId = 1, UrlImagen = "https://proassetspdlcom.cdnstatics2.com/usuaris/libros/thumbs/a1ebb647-56f6-4c85-9c2e-a6f9ebb1ef27/d_360_620/portada_rebelion-en-la-granja_george-orwell_202104141022.webp" }
                );

            modelBuilder.Entity<Libro>()
                .HasOne(l => l.Autor)
                .WithMany()
                .HasForeignKey(l => l.AutorId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
