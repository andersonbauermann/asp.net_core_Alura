using AutoMapper;
using filmsAPI.Data.Dtos;
using filmsAPI.Models;

namespace filmsAPI.Profiles;

public class MovieTheaterProfile : Profile
{
    public MovieTheaterProfile()
    {
        CreateMap<CreateMovieTheaterDto, MovieTheater>();
        CreateMap<MovieTheater, ReadMovieTheaterDto>()
            .ForMember(movieTheaterDto => movieTheaterDto.Address, 
                opt => opt.MapFrom(movieTheather => movieTheather.Address))
            .ForMember(movieTheaterDto => movieTheaterDto.Sections,
                opt => opt.MapFrom(movieTheather => movieTheather.Sections));
        CreateMap<UpdateMovieTheaterDto, MovieTheater>();
    }
}
