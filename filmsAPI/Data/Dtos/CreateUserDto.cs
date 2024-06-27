using System.ComponentModel.DataAnnotations;

namespace filmsAPI.Data.Dtos;

public class CreateUserDto
{
    [Required]
    public string UserName { get; set; }
    [Required]
    public DateTime BirthdayDate { get; set; }
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
    [Required]
    [Compare("Password")]
    public string RePassword { get; set; }
}
