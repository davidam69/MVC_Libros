Se eligio el enfoque Code First por la libertad de programar sin depender de la base de datos lo que es una ventaja
en cambio la desventaja esta en que si la base de datos tiene datos, y quiero agregar algo a la tabla puede ser complicado
hacer que el modelo de datos coincida con la base de datos existente

creo mis modelos Autor y Libro
creo en la raiz del proyecto la clase Usings.cs donde va a ir todos los using que voy a usar en el proyecto
instalo EntityFrameworkCore, EntityFrameworkCore.SqlServer y EntityFrameworkCore.Tools
en  Usings.cs Agrego:global using Microsoft.EntityFrameworkCore; global using Microsoft.EntityFrameworkCore.Design; global using Microsoft.EntityFrameworkCore.SqlServer; global using System.ComponentModel.DataAnnotations; global using System.ComponentModel.DataAnnotations.Schema; global using System.Collections.Generic;
creo la clase DbContext en models llamada LibrosContext.cs
configuro la cadena de conexion en appsettings.json
registro dbcontext en Program.cs
creo los controladores y las vistas de Autores y Libros con scaffolding
hago las migraciones y actualizo la base de datos
