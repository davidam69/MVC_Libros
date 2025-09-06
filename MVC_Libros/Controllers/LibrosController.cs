namespace MVC_Libros.Controllers
{
    public class LibrosController : Controller
    {
        private readonly LibrosContext _context;

        public LibrosController(LibrosContext context)
        {
            _context = context;
        }

        // GET: Libros
        public async Task<IActionResult> Index()
        {
            var librosContext = _context.Libros
                .Include(l => l.Autor)
                .OrderBy(l => l.Autor.Nombre)      // ordena por Autor
                .ThenBy(l => l.Titulo);
            return View(await librosContext.ToListAsync());
        }

        // GET: Libros/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                ViewBag.Mensaje = "Libro no encontrado";
                return View("Error");
            }

            var libro = await _context.Libros
                .Include(l => l.Autor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (libro == null)
            {
                ViewBag.Mensaje = "Libro no encontrado";
                return View("Error");
            }

            return View(libro);
        }

        // GET: Libros/Create
        public IActionResult Create()
        {
            ViewData["AutorId"] = new SelectList(_context.Autores
                .AsNoTracking()
                .OrderBy(a => a.Nombre),
                "Id", "Nombre");
            return View();
        }

        // POST: Libros/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Titulo,AnioPublicacion,UrlImagen,AutorId")] Libro libro)
        {
            // Normalizamos entrada
            var titulo = (libro.Titulo ?? "").Trim();

            // ¿Ya existe un libro con el mismo Título y el mismo Autor?
            bool duplicado = await _context.Libros
                .AnyAsync(l => l.AutorId == libro.AutorId &&
                               l.Titulo.ToLower() == titulo.ToLower());

            if (duplicado)
            {
                ModelState.AddModelError(string.Empty, "Ya existe un libro con ese título para ese autor.");
            }
            else
            {
                libro.Titulo = titulo; // guardo normalizado
            }

            if (ModelState.IsValid)
            {
                _context.Add(libro);
                await _context.SaveChangesAsync();

                TempData["Mensaje"] = "Libro agregado correctamente";

                return RedirectToAction(nameof(Details), new { id = libro.Id });
            }
            ViewData["AutorId"] = new SelectList(_context.Autores
                .AsNoTracking()
                .OrderBy(a => a.Nombre),
                "Id", "Nombre", libro.AutorId);
            return View(libro);
        }

        // GET: Libros/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var libro = await _context.Libros.FindAsync(id);
            if (libro == null)
            {
                return NotFound();
            }
            ViewData["AutorId"] = new SelectList(_context.Autores
                .AsNoTracking().OrderBy(a => a.Nombre),
                "Id", "Nombre", libro.AutorId);
            return View(libro);
        }

        // POST: Libros/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Titulo,AnioPublicacion,UrlImagen,AutorId")] Libro libro)
        {
            if (id != libro.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(libro);
                    await _context.SaveChangesAsync();

                    TempData["Mensaje"] = "Libro modificado correctamente";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LibroExists(libro.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AutorId"] = new SelectList(_context.Autores, "Id", "Nombre", libro.AutorId);
            return View(libro);
        }

        // GET: Libros/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var libro = await _context.Libros
                .Include(l => l.Autor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (libro == null)
            {
                return NotFound();
            }

            return View(libro);
        }

        // POST: Libros/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var libro = await _context.Libros.FindAsync(id);
            if (libro != null)
            {
                _context.Libros.Remove(libro);
                await _context.SaveChangesAsync();

                TempData["Mensaje"] = "Libro eliminado correctamente";
            }

            return RedirectToAction(nameof(Index));
        }

        private bool LibroExists(int id)
        {
            return _context.Libros.Any(e => e.Id == id);
        }

        [HttpGet]
        public IActionResult Tema(string color = "light")
        {
            // Mapa a clases Bootstrap nativas (sin depender de site.css)
            var map = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
    {
                { "light", "bg-light" },
                { "dark",  "bg-dark text-white" },
                { "orange", "bg-warning text-dark" },
                { "green", "bg-success text-white" }
    };

            if (!map.ContainsKey(color)) color = "light";

            HttpContext.Session.SetString("BodyClass", map[color]);

            // Volver a la página previa si existe; sino al Index del controlador actual
            var back = Request.Headers["Referer"].ToString();
            return !string.IsNullOrEmpty(back) ? Redirect(back) : RedirectToAction(nameof(Index));
        }


    }
}
