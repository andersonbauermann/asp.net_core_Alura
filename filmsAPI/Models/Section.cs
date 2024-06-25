using System.ComponentModel.DataAnnotations;

namespace filmsAPI.Models;

public class Section
{
    [Key]
    [Required]
    public int Id { get; set; }
    [Required]
    public int MovieId { get; set; }
    public virtual Movie Movie { get; set; }
}
