using AutoMapper;
using filmsAPI.Data.Dtos;
using filmsAPI.Models;

namespace filmsAPI.Profiles;

public class SectionProfile : Profile
{
    public SectionProfile()
    {
        CreateMap<CreateSectionDto, Section>();
        CreateMap<Section, ReadSectionDto>();
    }
}
