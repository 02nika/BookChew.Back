using Entities.Models;

namespace Repository.Contracts;

public interface IUserRepository
{
    Task AddUserAsync(User user);
    Task AddUsersAsync(List<User> users);
}