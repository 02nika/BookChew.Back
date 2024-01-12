using Entities.Models;
using Shared.Dtos.User;

namespace Repository.Contracts;

public interface IUserRepository
{
    Task AddUserAsync(User user);
    Task AddUsersAsync(List<User> users);
    Task<bool> UserExistsAsync(LoginUserDto userDto);
}