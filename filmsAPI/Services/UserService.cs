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
    private readonly TokenService _tokenService;

    public UserService(IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager, TokenService tokenService)
    {
        _mapper = mapper;
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenService = tokenService;
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

    public async Task<string> Login(LoginUserDto dto)
    {
        var result = await _signInManager.PasswordSignInAsync(dto.UserName, dto.Password, false, false);

        if (!result.Succeeded)
        {
            throw new ApplicationException("unauthenticated user");
        }

        var user = _signInManager.UserManager.Users.FirstOrDefault(user => user.NormalizedUserName == dto.UserName.ToUpper());

        var token = _tokenService.GenerateToken(user);

        return token;
    }
}
