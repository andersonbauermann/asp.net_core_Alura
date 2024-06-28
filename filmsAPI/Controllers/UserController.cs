using AutoMapper;
using filmsAPI.Data.Dtos;
using filmsAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace filmsAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;

    public UserController(IMapper mapper, UserManager<User> userManager)
    {
        _mapper = mapper;
        _userManager = userManager;
    }

    [HttpPost]
    public async Task<IActionResult> RegisterUser(CreateUserDto userDto)
    {
        User user = _mapper.Map<User>(userDto);
        var result = await _userManager.CreateAsync(user, userDto.Password);

        if (result.Succeeded)
        {
            return Ok();
        }

        throw new ApplicationException();
    }
}
