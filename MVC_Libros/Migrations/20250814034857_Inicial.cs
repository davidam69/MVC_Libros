using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MVC_Libros.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Autores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Autores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Libros",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AnioPublicacion = table.Column<int>(type: "int", nullable: false),
                    UrlImagen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AutorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Libros", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Libros_Autores_AutorId",
                        column: x => x.AutorId,
                        principalTable: "Autores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Autores",
                columns: new[] { "Id", "Nombre" },
                values: new object[,]
                {
                    { 1, "George Orwell" },
                    { 2, "Ray Bradbury" }
                });

            migrationBuilder.InsertData(
                table: "Libros",
                columns: new[] { "Id", "AnioPublicacion", "AutorId", "Titulo", "UrlImagen" },
                values: new object[,]
                {
                    { 1, 1949, 1, "1984", "https://proassetspdlcom.cdnstatics2.com/usuaris/libros/thumbs/b59f709a-ce74-4e50-a3a3-3527d68da121/d_360_620/portada_1984_george-orwell_202102151044.webp" },
                    { 2, 1953, 2, "Fahrenheit 451", "https://upload.wikimedia.org/wikipedia/en/d/db/Fahrenheit_451_1st_ed_cover.jpg" },
                    { 3, 1945, 1, "Rebelion en la granja", "https://proassetspdlcom.cdnstatics2.com/usuaris/libros/thumbs/a1ebb647-56f6-4c85-9c2e-a6f9ebb1ef27/d_360_620/portada_rebelion-en-la-granja_george-orwell_202104141022.webp" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Libros_AutorId",
                table: "Libros",
                column: "AutorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Libros");

            migrationBuilder.DropTable(
                name: "Autores");
        }
    }
}
