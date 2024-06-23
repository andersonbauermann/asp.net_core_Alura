using System.ComponentModel.DataAnnotations;

namespace filmsAPI.Data.Dtos
{
    public class UpdateMovieTheaterDto
    {
        [Required(ErrorMessage = "The Name of the Movie Theather is mandatory")]
        public string Name { get; set; }
    }
}
