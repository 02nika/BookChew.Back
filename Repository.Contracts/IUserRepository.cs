using Entities.Models;
using Shared.Dto.User;

namespace Repository.Contracts;

public interface IUserRepository
{
    Task AddUserAsync(User user);
    Task AddUsersAsync(List<User> users);
    Task<User> GetUserAsync(int userId);
    Task<User> GetUserAsync(LoginUserDto userDto);
}