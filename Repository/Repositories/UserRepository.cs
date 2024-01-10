using Entities.Models;
using Repository.Context;
using Repository.Contracts;

namespace Repository.Repositories;

public class UserRepository(AppDbContext appDbContext) : RepositoryBase<User>(appDbContext), IUserRepository
{
    public async Task AddUserAsync(User user) => await Create(user);
}