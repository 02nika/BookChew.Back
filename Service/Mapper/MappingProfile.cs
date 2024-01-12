using AutoMapper;
using Entities.Models;
using Shared.Dtos.User;
using Shared.Extensions;

namespace Service.Mapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<AddUserDto, User>();
        CreateMap<AddUserDto, UserPassword>()
            .ForMember(up => up.PasswordHash, m => m
                .MapFrom(u => u.Password.ComputeSha256Hash()))
            .ForMember(up => up.IsActive, m => m
                .MapFrom(u => true));
    }
}