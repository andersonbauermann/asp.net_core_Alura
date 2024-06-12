using filmsAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace filmsAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class FilmController : ControllerBase
{
    private static List<Movie> movies = new();

    [HttpPost]
    public void AddFilm([FromBody] Movie movie)
    {
        movies.Add(movie);
    }
}
