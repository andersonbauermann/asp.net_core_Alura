using AutoMapper;
using filmsAPI.Data.Dtos;
using filmsAPI.Models;
using Microsoft.AspNetCore.Identity;

namespace filmsAPI.Services;

public class UserService
{
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;

    public UserService(IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager)
    {
        _mapper = mapper;
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task Register(CreateUserDto userDto)
    {
        User user = _mapper.Map<User>(userDto);
        var result = await _userManager.CreateAsync(user, userDto.Password);

        if (!result.Succeeded)
        {
            throw new ApplicationException();
        }
    }

    public async Task Login(LoginUserDto dto)
    {
        var result = await _signInManager.PasswordSignInAsync(dto.UserName, dto.Password, false, false);

        if (!result.Succeeded)
        {
            throw new ApplicationException("unauthenticated user");
        }
    }
}
