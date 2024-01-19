using AutoMapper;
using Entities.Exceptions.Custom;
using Entities.Models;
using Repository.Contracts;
using Service.Contracts;
using Shared.Dto.User;

namespace Service.Services;

public class UserService(IRepositoryManager repositoryManager, IMapper mapper) : IUserService
{
    public async Task<UserDto> AddUserAsync(AddUserDto userDto)
    {
        var user = mapper.Map<User>(userDto);
        var password = mapper.Map<UserPassword>(userDto);
        
        user.Passwords.Add(password);
        
        await repositoryManager.UserRepository.AddUserAsync(user);
        await repositoryManager.SaveAsync();

        return mapper.Map<UserDto>(user);
    }

    public async Task<UserDto> GetUserAsync(LoginUserDto userDto)
    {
        var user = await repositoryManager.UserRepository.GetUserAsync(userDto);

        if (user is null) throw new UserNotFoundException();
        
        return mapper.Map<UserDto>(user);
    }

    public async Task<UserDto> GetUserAsync(int userId)
    {
        var user = await repositoryManager.UserRepository.GetUserAsync(userId);

        if (user is null) throw new UserNotFoundException();
        
        return mapper.Map<UserDto>(user);
    }
}