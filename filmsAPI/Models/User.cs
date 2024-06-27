using Microsoft.AspNetCore.Identity;

namespace filmsAPI.Models;

public class User : IdentityUser
{
    public DateTime BirthdayDate { get; set; }
    public User() : base() { }
}
