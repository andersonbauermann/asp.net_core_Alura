using AutoMapper;
using filmsAPI.Data;
using filmsAPI.Data.Dtos;
using filmsAPI.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace filmsAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class AddressController : ControllerBase
{
    private readonly MovieContext _context;
    private readonly IMapper _mapper;

    public AddressController(MovieContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    /// <summary>
    /// Add a movie theater to the database
    /// </summary>
    /// <param name="addressDto">DTO with the properties to create a movie theater</param>
    /// <returns>IActionResult</returns>
    /// <response code="201">If insertion is successful</response>
    [HttpPost]
    public IActionResult AddAddress([FromBody] CreateAddressDto addressDto)
    {
        Address address = _mapper.Map<Address>(addressDto);
        _context.Addresses.Add(address);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetAddressById), new { id = address.Id }, address);
    }

    /// <summary>
    /// Retrieves a list of addresses with pagination
    /// </summary>
    /// <param name="skip">The number of addresses to skip</param>
    /// <param name="take">The number of addresses to take</param>
    /// <returns>IEnumerable&lt;ReadAddressDto&gt;</returns>
    /// <response code="200">Returns the list addresses</response>
    [HttpGet]
    public IEnumerable<ReadAddressDto> GetAddressList([FromQuery] int skip = 0, [FromQuery] int take = 10)
    {

        return _mapper.Map<List<ReadAddressDto>>(_context.Addresses.Skip(skip).Take(take));
    }

    /// <summary>
    /// Retrieves a address by its ID
    /// </summary>
    /// <param name="id">The ID of the address to retrieve</param>
    /// <returns>IActionResult</returns>
    /// <response code="200">If the address is found</response>
    /// <response code="404">If the address is not found</response>
    [HttpGet("{id}")]
    public IActionResult GetAddressById(int id)
    {
        var address = _context.Addresses.FirstOrDefault(x => x.Id == id);

        if (address is null) return NotFound();

        var addressDto = _mapper.Map<ReadAddressDto>(address);
        return Ok(addressDto);
    }

    /// <summary>
    /// Updates an existing address in the database
    /// </summary>
    /// <param name="id">The ID of the address to update</param>
    /// <param name="addressDto">DTO with the properties to update the address</param>
    /// <returns>IActionResult</returns>
    /// <response code="204">If the update is successful</response>
    /// <response code="404">If the address is not found</response>
    /// <response code="400">If the input data is invalid</response>
    [HttpPut("{id}")]
    public IActionResult UpdateAddress(int id, [FromBody] UpdateAddressDto addressDto)
    {
        var address = _context.MovieTheaters.FirstOrDefault(address => address.Id == id);

        if (address is null) return NotFound();

        _mapper.Map(addressDto, address);
        _context.SaveChanges();
        return NoContent();
    }

    /// <summary>
    /// Partially updates a address in the database
    /// </summary>
    /// <param name="id">The ID of the address to update</param>
    /// <param name="patch">JSON Patch document with the properties to update</param>
    /// <returns>IActionResult</returns>
    /// <response code="204">If the update is successful</response>
    /// <response code="404">If the address is not found</response>
    /// <response code="400">If the patch document is invalid</response>    
    [HttpPatch("{id}")]
    public IActionResult UpdatePartialAddress(int id, JsonPatchDocument<UpdateAddressDto> patch)
    {
        var address = _context.MovieTheaters.FirstOrDefault(movieTheater => movieTheater.Id == id);

        if (address is null) return NotFound();

        var addressToUpdate = _mapper.Map<UpdateAddressDto>(address);
        patch.ApplyTo(addressToUpdate, ModelState);

        if (!TryValidateModel(addressToUpdate)) return ValidationProblem(ModelState);

        _mapper.Map(addressToUpdate, address);
        _context.SaveChanges();
        return NoContent();
    }

    /// <summary>
    /// Deletes a address from the database
    /// </summary>
    /// <param name="id">The ID of the address to be deleted</param>
    /// <returns>IActionResult</returns>
    /// <response code="204">If the deletion is successful</response>
    /// <response code="404">If the address is not found</response>
    [HttpDelete("{id}")]
    public IActionResult DeleteAddress(int id)
    {
        var address = _context.Addresses.FirstOrDefault(address => address.Id == id);

        if (address is null) return NotFound();

        _context.Remove(address);
        _context.SaveChanges();
        return NoContent();
    }
}
