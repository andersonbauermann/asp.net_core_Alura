using AutoMapper;
using filmsAPI.Data;
using filmsAPI.Data.Dtos;
using filmsAPI.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace filmsAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class MovieTheaterController : ControllerBase
{
    private readonly MovieContext _context;
    private readonly IMapper _mapper;

    public MovieTheaterController(MovieContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    /// <summary>
    /// Add a movie theater to the database
    /// </summary>
    /// <param name="movieTheatherDto">DTO with the properties to create a movie theater</param>
    /// <returns>IActionResult</returns>
    /// <response code="201">If insertion is successful</response>
    [HttpPost]
    public IActionResult AddMovieTheather([FromBody] CreateMovieTheaterDto movieTheatherDto)
    {
        MovieTheater movieTheather = _mapper.Map<MovieTheater>(movieTheatherDto);
        _context.MovieTheaters.Add(movieTheather);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetMovieTheaterById), new { id = movieTheather.Id }, movieTheather);
    }

    /// <summary>
    /// Retrieves a list of movies theaters with pagination
    /// </summary>
    /// <param name="skip">The number of movies theaters to skip</param>
    /// <param name="take">The number of movies theaters to take</param>
    /// <returns>IEnumerable&lt;ReadMovieTheaterDto&gt;</returns>
    /// <response code="200">Returns the list of movie theathers</response>
    [HttpGet]
    public IEnumerable<ReadMovieTheaterDto> GetMovieTheaterList([FromQuery] int skip = 0, [FromQuery] int take = 10)
    {

        return _mapper.Map<List<ReadMovieTheaterDto>>(_context.MovieTheaters.Skip(skip).Take(take));
    }

    /// <summary>
    /// Retrieves a movie theater by its ID
    /// </summary>
    /// <param name="id">The ID of the movie theater to retrieve</param>
    /// <returns>IActionResult</returns>
    /// <response code="200">If the movie theater is found</response>
    /// <response code="404">If the movie theater is not found</response>
    [HttpGet("{id}")]
    public IActionResult GetMovieTheaterById(int id)
    {
        var movieTheater = _context.MovieTheaters.FirstOrDefault(x => x.Id == id);

        if (movieTheater is null) return NotFound();

        var movieTheatherDto = _mapper.Map<ReadMovieTheaterDto>(movieTheater);
        return Ok(movieTheatherDto);
    }

    /// <summary>
    /// Updates an existing movie theater in the database
    /// </summary>
    /// <param name="id">The ID of the movie theater to update</param>
    /// <param name="movieTheaterDto">DTO with the properties to update the movie theater</param>
    /// <returns>IActionResult</returns>
    /// <response code="204">If the update is successful</response>
    /// <response code="404">If the movie theater is not found</response>
    /// <response code="400">If the input data is invalid</response>
    [HttpPut("{id}")]
    public IActionResult UpdateMovieTheater(int id, [FromBody] UpdateMovieTheaterDto movieTheaterDto)
    {
        var movieTheater = _context.MovieTheaters.FirstOrDefault(movieTheater => movieTheater.Id == id);

        if (movieTheater is null) return NotFound();

        _mapper.Map(movieTheaterDto, movieTheater);
        _context.SaveChanges();
        return NoContent();
    }

    /// <summary>
    /// Partially updates a movie theater in the database
    /// </summary>
    /// <param name="id">The ID of the movie theater to update</param>
    /// <param name="patch">JSON Patch document with the properties to update</param>
    /// <returns>IActionResult</returns>
    /// <response code="204">If the update is successful</response>
    /// <response code="404">If the movie theater is not found</response>
    /// <response code="400">If the patch document is invalid</response>    
    [HttpPatch("{id}")]
    public IActionResult UpdatePartialMovieTheater(int id, JsonPatchDocument<UpdateMovieTheaterDto> patch)
    {
        var movieTheater = _context.MovieTheaters.FirstOrDefault(movieTheater => movieTheater.Id == id);

        if (movieTheater is null) return NotFound();

        var movieTheaterToUpdate = _mapper.Map<UpdateMovieTheaterDto>(movieTheater);
        patch.ApplyTo(movieTheaterToUpdate, ModelState);

        if (!TryValidateModel(movieTheaterToUpdate)) return ValidationProblem(ModelState);

        _mapper.Map(movieTheaterToUpdate, movieTheater);
        _context.SaveChanges();
        return NoContent();
    }

    /// <summary>
    /// Deletes a movie theater from the database
    /// </summary>
    /// <param name="id">The ID of the movie theater to be deleted</param>
    /// <returns>IActionResult</returns>
    /// <response code="204">If the deletion is successful</response>
    /// <response code="404">If the movie theater is not found</response>
    [HttpDelete("{id}")]
    public IActionResult DeleteMovieTheater(int id)
    {
        var movieTheater = _context.MovieTheaters.FirstOrDefault(movieTheater => movieTheater.Id == id);

        if (movieTheater is null) return NotFound();

        _context.Remove(movieTheater);
        _context.SaveChanges();
        return NoContent();
    }
}
