using AutoMapper;
using filmsAPI.Data.Dtos;
using filmsAPI.Models;

namespace filmsAPI.Profiles;

public class AddressProfile : Profile
{
    public AddressProfile()
    {
        CreateMap<CreateAddressDto, Address>();
        CreateMap<UpdateAddressDto, Address>();
        CreateMap<Address, ReadAddressDto>();
    }
}
