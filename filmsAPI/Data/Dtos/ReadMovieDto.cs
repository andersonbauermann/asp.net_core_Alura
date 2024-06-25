namespace filmsAPI.Data.Dtos;

public class ReadMovieDto
{
    public string Title { get; set; }
    public string Gender { get; set; }
    public int Duration { get; set; }
    public DateTime Time { get; set; } = DateTime.Now;
    public ICollection<ReadSectionDto> Sections { get; set; }
}
