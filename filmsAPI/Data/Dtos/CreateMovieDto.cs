using System.ComponentModel.DataAnnotations;

namespace filmsAPI.Data.Dtos;

public class CreateMovieDto
{
    [Required(ErrorMessage = "The title of the film is mandatory")]
    public string Title { get; set; }
    [Required(ErrorMessage = "The gender of the film is mandatory")]
    [StringLength(50, ErrorMessage = "Genre length cannot exceed 50 characters")]
    public string Gender { get; set; }
    [Required]
    [Range(70, 600, ErrorMessage = "The length of the film must be between 70 and 600 minutes")]
    public int Duration { get; set; }
}
