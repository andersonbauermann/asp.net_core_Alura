using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using filmsAPI.Data;
using filmsAPI.Models;
using filmsAPI.Data.Dtos;

namespace filmsAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class SectionController : ControllerBase
{
    private readonly MovieContext _context;
    private readonly IMapper _mapper;

    public SectionController(MovieContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpPost]
    public IActionResult AddSection(CreateSectionDto sectionDto)
    {
        var movie = _context.Movies.FirstOrDefault(movie => movie.Id == sectionDto.MovieId);

        if (movie is null) 
        {
            return NotFound();
        }

        Section section = _mapper.Map<Section>(sectionDto);
        _context.Sections.Add(section);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetSectionById),
                               new { movieId = section.MovieId, movieTheaterId = section.MovieTheaterId },
                               section);
    }

    [HttpGet]
    public IEnumerable<ReadSectionDto> GetSection()
    {
        return _mapper.Map<List<ReadSectionDto>>(_context.Sections.ToList());
    }

    [HttpGet("{movieId}/{movieTheaterId}")]
    public IActionResult GetSectionById(int movieId, int movieTheaterId)
    {
        Section section = _context.Sections.FirstOrDefault(section => section.MovieId == movieId && section.MovieTheaterId == movieTheaterId);
        if (section != null)
        {
            ReadSectionDto sectionDto = _mapper.Map<ReadSectionDto>(section);

            return Ok(sectionDto);
        }
        return NotFound();
    }
}
