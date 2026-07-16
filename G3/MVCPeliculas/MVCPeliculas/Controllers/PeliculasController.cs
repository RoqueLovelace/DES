
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCPeliculas.Models;
using MVCPeliculas.Data;

public class PeliculasController : Controller
{
    private readonly PeliculasDbContext _context;

    public PeliculasController(PeliculasDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()    
    {
        return View(await _context.Peliculas.ToListAsync());
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var pelicula = await _context.Peliculas
            .FirstOrDefaultAsync(m => m.Id == id);
        if (pelicula == null)
        {
            return NotFound();
        }

        return View(pelicula);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Titulo,FechaLanzamiento,GeneroId,Genero,Precio,Director")] Pelicula pelicula)
    {
        if (ModelState.IsValid)
        {
            _context.Add(pelicula);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(pelicula);
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var pelicula = await _context.Peliculas.FindAsync(id);
        if (pelicula == null)
        {
            return NotFound();
        }
        return View(pelicula);
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,Titulo,FechaLanzamiento,GeneroId,Genero,Precio,Director")] Pelicula pelicula)
    {
        if (id != pelicula.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(pelicula);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PeliculaExists(pelicula.Id))
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
        return View(pelicula);
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var pelicula = await _context.Peliculas
            .FirstOrDefaultAsync(m => m.Id == id);
        if (pelicula == null)
        {
            return NotFound();
        }

        return View(pelicula);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var pelicula = await _context.Peliculas.FindAsync(id);
        if (pelicula != null)
        {
            _context.Peliculas.Remove(pelicula);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool PeliculaExists(int? id)
    {
        return _context.Peliculas.Any(e => e.Id == id);
    }
}
