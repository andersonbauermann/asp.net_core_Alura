﻿using AutoMapper;
using filmsAPI.Data.Dtos;
using filmsAPI.Models;

namespace filmsAPI.Profiles;

public class MovieProfile : Profile
{
    public MovieProfile()
    {
        CreateMap<CreateMovieDto, Movie>();
        CreateMap<UpdateMovieDto, Movie>();
        CreateMap<Movie, UpdateMovieDto>();
        CreateMap<Movie, ReadMovieDto>();
    }
}