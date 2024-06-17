using filmsAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace filmsAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class FilmController : ControllerBase
{
    private static List<Movie> movies = new();
    private static int id = 0;

    [HttpPost]
    public IActionResult AddFilm([FromBody] Movie movie)
    {
        movie.Id = id++;
        movies.Add(movie);
        return CreatedAtAction(nameof(GetMovieById), new { id = movie.Id }, movie);
        // seguindo o padrão rest, quando criamos algo, retornamos o método que mostra a entidade que foi criada, e a própria entidade
    }

    [HttpGet]
    public IEnumerable<Movie> GetMovies([FromQuery] int skip = 0, [FromQuery] int take = 10)
    {

        return movies.Skip(skip).Take(take);
    }

    [HttpGet("{id}")]
    public IActionResult GetMovieById(int id)
    {
        var movie = movies.FirstOrDefault(x => x.Id == id);
        if (movie is null) return NotFound();
        return Ok(movie);
    }
}
