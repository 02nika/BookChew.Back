using AutoMapper;
using Entities.Models;
using Shared.Dto.Restaurant;
using Shared.Dto.User;
using Shared.Extensions;

namespace Service.Mapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<AddUserDto, User>();
        CreateMap<User, UserDto>();
        CreateMap<AddUserDto, UserPassword>()
            .ForMember(up => up.PasswordHash, m => m
                .MapFrom(u => u.Password.ComputeSha256Hash()))
            .ForMember(up => up.IsActive, m => m
                .MapFrom(u => true));
        
        CreateMap<Restaurant, RestaurantDto>();
        CreateMap<AddRestaurantDto, Restaurant>();
    }
}