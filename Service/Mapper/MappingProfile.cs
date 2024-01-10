using AutoMapper;
using Entities.Models;
using Shared.Dtos.User;

namespace Service.Mapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<AddUserDto, User>();
    }
}