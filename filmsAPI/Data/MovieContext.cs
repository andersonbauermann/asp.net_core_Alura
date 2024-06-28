using filmsAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace filmsAPI.Data;

public class MovieContext : IdentityDbContext<User> // sem identity herda somente de DbContext
{
    public MovieContext(DbContextOptions<MovieContext> opts) : base(opts) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // define chave composta
        modelBuilder.Entity<Section>().HasKey(section => new { section.MovieId, section.MovieTheaterId });

        //define quais setão as chaves compostas
        modelBuilder.Entity<Section>()
            .HasOne(section => section.MovieTheater)
            .WithMany(movieTheater => movieTheater.Sections)
            .HasForeignKey(section => section.MovieTheaterId);

        modelBuilder.Entity<Section>()
           .HasOne(section => section.Movie)
           .WithMany(movie => movie.Sections)
           .HasForeignKey(section => section.MovieId);

        modelBuilder.Entity<Address>()
            .HasOne(address => address.MovieTheater)
            .WithOne(movieTheater => movieTheater.Address)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<IdentityUserLogin<string>>()
            .HasKey(login => new { login.LoginProvider, login.ProviderKey });

        modelBuilder.Entity<IdentityUserRole<string>>()
            .HasKey(role => new { role.UserId, role.RoleId });

        modelBuilder.Entity<IdentityUserToken<string>>()
            .HasKey(token => new { token.UserId, token.LoginProvider, token.Name });
    }

    public DbSet<Movie> Movies { get; set; }
    public DbSet<MovieTheater> MovieTheaters { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<Section> Sections { get; set; }
}
