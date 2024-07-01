using filmsAPI.Data.Dtos;
using filmsAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace filmsAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly UserService _userService;

    public UserController(UserService userService)
    {
        _userService = userService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterUser(CreateUserDto userDto)
    {
        await _userService.Register(userDto);
        return Ok();
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginUserDto dto)
    {
        await _userService.Login(dto);
        return Ok("authenticated user");
    }
}
