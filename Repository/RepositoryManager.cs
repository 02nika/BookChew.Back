using Repository.Context;
using Repository.Contracts;
using Repository.Repositories;

namespace Repository;

public class RepositoryManager : IRepositoryManager
{
    private readonly AppDbContext _appDbContext;
    private readonly Lazy<IUserRepository> _userRepository;
    private readonly Lazy<IRestaurantRepository> _restaurantRepository;

    public RepositoryManager(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
        _userRepository = new Lazy<IUserRepository>(() => new UserRepository(_appDbContext));
        _restaurantRepository = new Lazy<IRestaurantRepository>(() => new RestaurantRepository(_appDbContext));
    }

    public IUserRepository UserRepository => _userRepository.Value;
    public IRestaurantRepository RestaurantRepository => _restaurantRepository.Value;

    public async Task SaveAsync() => await _appDbContext.SaveChangesAsync();
}