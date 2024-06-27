using filmsAPI.Data.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace filmsAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    [HttpPost]
    public IActionResult RegisterUser(CreateUserDto user)
    {
        throw new NotImplementedException();
    }
}
