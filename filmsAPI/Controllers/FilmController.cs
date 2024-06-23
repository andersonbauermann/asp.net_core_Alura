using AutoMapper;
using filmsAPI.Data;
using filmsAPI.Data.Dtos;
using filmsAPI.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace filmsAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class FilmController : ControllerBase
{
    private readonly MovieContext _context;
    private readonly IMapper _mapper;

    public FilmController(MovieContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    /// <summary>
    /// Add a movie to the database
    /// </summary>
    /// <param name="movieDto">DTO with the properties to create a movie</param>
    /// <returns>IActionResult</returns>
    /// <response code="201">If insertion is successful</response>
    [HttpPost]
    public IActionResult AddFilm([FromBody] CreateMovieTheaterDto movieDto)
    {
        Movie movie = _mapper.Map<Movie>(movieDto);
        _context.Movies.Add(movie);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetMovieById), new { id = movie.Id }, movie);
        // seguindo o padrão rest, quando criamos algo, retornamos o método que mostra a entidade que foi criada, e a própria entidade
    }

    /// <summary>
    /// Retrieves a list of movies with pagination
    /// </summary>
    /// <param name="skip">The number of movies to skip</param>
    /// <param name="take">The number of movies to take</param>
    /// <returns>IEnumerable&lt;ReadMovieDto&gt;</returns>
    /// <response code="200">Returns the list of movies</response>
    [HttpGet]
    public IEnumerable<ReadMovieDto> GetMovies([FromQuery] int skip = 0, [FromQuery] int take = 10)
    {

        return _mapper.Map<List<ReadMovieDto>>(_context.Movies.Skip(skip).Take(take));
    }

    /// <summary>
    /// Retrieves a movie by its ID
    /// </summary>
    /// <param name="id">The ID of the movie to retrieve</param>
    /// <returns>IActionResult</returns>
    /// <response code="200">If the movie is found</response>
    /// <response code="404">If the movie is not found</response>
    [HttpGet("{id}")]
    public IActionResult GetMovieById(int id)
    {
        var movie = _context.Movies.FirstOrDefault(x => x.Id == id);

        if (movie is null) return NotFound();

        var movieDto = _mapper.Map<ReadMovieDto>(movie);
        return Ok(movie);
    }

    /// <summary>
    /// Updates an existing movie in the database
    /// </summary>
    /// <param name="id">The ID of the movie to update</param>
    /// <param name="movieDto">DTO with the properties to update the movie</param>
    /// <returns>IActionResult</returns>
    /// <response code="204">If the update is successful</response>
    /// <response code="404">If the movie is not found</response>
    /// <response code="400">If the input data is invalid</response>
    [HttpPut("{id}")]
    public IActionResult UpdateMovie(int id, [FromBody]UpdateMovieDto movieDto)
    {
        var movie = _context.Movies.FirstOrDefault(movie => movie.Id == id);

        if (movie is null) return NotFound();

        _mapper.Map(movieDto, movie);
        _context.SaveChanges();
        return NoContent();
    }

    /// <summary>
    /// Partially updates a movie in the database
    /// </summary>
    /// <param name="id">The ID of the movie to update</param>
    /// <param name="patch">JSON Patch document with the properties to update</param>
    /// <returns>IActionResult</returns>
    /// <response code="204">If the update is successful</response>
    /// <response code="404">If the movie is not found</response>
    /// <response code="400">If the patch document is invalid</response>
    [HttpPatch("{id}")]
    public IActionResult UpdatePartialMovie(int id, JsonPatchDocument<UpdateMovieDto> patch)
    {
        var movie = _context.Movies.FirstOrDefault(movie => movie.Id == id);

        if (movie is null) return NotFound();

        var movieToUpdate = _mapper.Map<UpdateMovieDto>(movie);
        patch.ApplyTo(movieToUpdate, ModelState);

        if (!TryValidateModel(movieToUpdate)) return ValidationProblem(ModelState);

        _mapper.Map(movieToUpdate, movie);
        _context.SaveChanges();
        return NoContent();
    }

    /// <summary>
    /// Deletes a movie from the database
    /// </summary>
    /// <param name="id">The ID of the movie to be deleted</param>
    /// <returns>IActionResult</returns>
    /// <response code="204">If the deletion is successful</response>
    /// <response code="404">If the movie is not found</response>
    [HttpDelete("{id}")]
    public IActionResult DeleteMovie(int id) 
    {
        var movie = _context.Movies.FirstOrDefault(movie => movie.Id == id);

        if (movie is null) return NotFound();

        _context.Remove(movie);
        _context.SaveChanges();
        return NoContent();
    }
}
