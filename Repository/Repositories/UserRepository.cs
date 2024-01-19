using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Context;
using Repository.Contracts;
using Shared.Dto.User;
using Shared.Extensions;

namespace Repository.Repositories;

public class UserRepository(AppDbContext db) : RepositoryBase<User>(db), IUserRepository
{
    public async Task AddUserAsync(User user) => await Create(user);
    public async Task AddUsersAsync(List<User> users) => await BulkCreate(users);
    public async Task<User> GetUserAsync(LoginUserDto userDto)
    {
        var userPasswords = db.Passwords
            .Where(p => p.IsActive && p.PasswordHash == userDto.Password.ComputeSha256Hash());

        return await db.Users.Where(u => u.UserName == userDto.UserName &&
                                   userPasswords.Any(p => p.UserId == u.Id)).FirstAsync();
    }

    public async Task<User> GetUserAsync(int userId) => await FindByCondition(u => u.Id == userId).FirstAsync();
}