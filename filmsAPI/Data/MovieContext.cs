using filmsAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace filmsAPI.Data;

public class MovieContext : DbContext
{
    public MovieContext(DbContextOptions<MovieContext> opts) : base(opts)
    {
    }

    public DbSet<Movie> Movies { get; set; }
    public DbSet<MovieTheater> MovieTheaters { get; set; }
    public DbSet<Address> Addresses { get; set; }
}
