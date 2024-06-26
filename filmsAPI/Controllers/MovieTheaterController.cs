using AutoMapper;
using filmsAPI.Data;
using filmsAPI.Data.Dtos;
using filmsAPI.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
    /// <param name="movieTheaterDto">DTO with the properties to create a movie theater</param>
    /// <returns>IActionResult</returns>
    /// <response code="201">If insertion is successful</response>
    [HttpPost]
    public IActionResult AddMovieTheather([FromBody] CreateMovieTheaterDto movieTheaterDto)
    {
        MovieTheater movieTheater = _mapper.Map<MovieTheater>(movieTheaterDto);
        _context.MovieTheaters.Add(movieTheater);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetMovieTheaterById), new { id = movieTheater.Id }, movieTheater);
    }

    /// <summary>
    /// Retrieves a list of movies theaters with pagination
    /// </summary>
    /// <param name="skip">The number of movies theaters to skip</param>
    /// <param name="take">The number of movies theaters to take</param>
    /// <param name="nameMovieTheater"></param>
    /// <returns>IEnumerable&lt;ReadMovieTheaterDto&gt;</returns>
    /// <response code="200">Returns the list of movie theathers</response>
    [HttpGet]
    public IEnumerable<ReadMovieTheaterDto> GetMovieTheaterList([FromQuery] int skip = 0, [FromQuery] int take = 10, [FromQuery] string? nameMovieTheater = null)
    {
        if (string.IsNullOrEmpty(nameMovieTheater))
            return _mapper.Map<List<ReadMovieTheaterDto>>(_context.MovieTheaters.Skip(skip).Take(take).ToList());

        return _mapper.Map<List<ReadMovieTheaterDto>>(_context.MovieTheaters
                                                            .Skip(skip)
                                                            .Take(take)
                                                            .Where(movie => movie.Sections.Any(section => section.MovieTheater.Name == nameMovieTheater))
                                                            .ToList());
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

        var movieTheaterDto = _mapper.Map<ReadMovieTheaterDto>(movieTheater);
        return Ok(movieTheaterDto);
    }

    /// <summary>
    /// Retrieves a list of movie theaters. If an address ID is provided, it retrieves movie theaters at that address.
    /// </summary>
    /// <param name="addressId">The optional address ID to filter the movie theaters</param>
    /// <returns>An enumerable list of ReadMovieTheaterDto</returns>
    /// <response code="200">If the movie theaters are successfully retrieved</response>
    /// <response code="404">If no movie theaters are found</response>
    [HttpGet("{id}")]
    public IEnumerable<ReadMovieTheaterDto> GetMovieTheaterList([FromQuery] int? addressId = null)
    {
        if (addressId is null)
            return _mapper.Map<List<ReadMovieTheaterDto>>(_context.MovieTheaters.ToList());

        return _mapper.Map<List<ReadMovieTheaterDto>>(_context.MovieTheaters.FromSqlRaw($"SELECT Id, Name, AddressId FROM MovieTheaters WHERE AddressId = {addressId}").ToList());
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
