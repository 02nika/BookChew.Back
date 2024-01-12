using AutoMapper;
using Entities.Models;
using Repository.Contracts;
using Service.Contracts;
using Shared.Dtos.User;

namespace Service.Services;

public class UserService(IRepositoryManager repositoryManager, IMapper mapper) : IUserService
{
    public async Task AddUserAsync(AddUserDto userDto)
    {
        var user = mapper.Map<User>(userDto);
        var password = mapper.Map<UserPassword>(userDto);
        
        user.Passwords.Add(password);
        
        await repositoryManager.UserRepository.AddUserAsync(user);
        await repositoryManager.SaveAsync();
    }

    public async Task<bool> UserExistsAsync(LoginUserDto userDto)
    {
        return await repositoryManager.UserRepository.UserExistsAsync(userDto);
    }
}