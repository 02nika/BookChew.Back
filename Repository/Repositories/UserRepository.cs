using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Context;
using Repository.Contracts;
using Shared.Dtos.User;
using Shared.Extensions;

namespace Repository.Repositories;

public class UserRepository(AppDbContext db) : RepositoryBase<User>(db), IUserRepository
{
    public async Task AddUserAsync(User user) => await Create(user);
    public async Task AddUsersAsync(List<User> users) => await BulkCreate(users);

    public async Task<bool> UserExistsAsync(LoginUserDto userDto)
    {
        var userExists = await db.Users
            .AnyAsync(u => u.UserName == userDto.UserName &&
                           u.Passwords.Any(p => p.IsActive && 
                                                p.PasswordHash == userDto.Password.ComputeSha256Hash()));

        return userExists;
    }
}