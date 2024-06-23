using AutoMapper;
using filmsAPI.Data.Dtos;
using filmsAPI.Models;

namespace filmsAPI.Profiles;

public class MovieTheaterProfile : Profile
{
    public MovieTheaterProfile()
    {
        CreateMap<CreateMovieTheaterDto, MovieTheater>();
        CreateMap<MovieTheater, ReadMovieTheaterDto>();
        CreateMap<UpdateMovieTheaterDto, MovieTheater>();
    }
}
