using AutoMapper;
using Entities.Models;
using Microsoft.Extensions.Options;
using Repository.Contracts;
using Service.Contracts;
using Service.Services;
using Shared.Config;

namespace Service;

public class ServiceManager: IServiceManager
{
    private readonly Lazy<IAuthService> _authService;
    private readonly Lazy<IUserService> _userService;
    private readonly Lazy<IRestaurantService> _restaurantService;
    
    public ServiceManager(IRepositoryManager repositoryManager, IOptions<JwtSettings> jwtSettings, IMapper mapper)
    {
        _authService = new Lazy<IAuthService>(() => new AuthService(jwtSettings));
        _userService = new Lazy<IUserService>(() => new UserService(repositoryManager, mapper));
        _restaurantService = new Lazy<IRestaurantService>(() => new RestaurantService(repositoryManager, mapper));
    }

    public IAuthService AuthService => _authService.Value;
    public IUserService UserService => _userService.Value;
    public IRestaurantService RestaurantService => _restaurantService.Value;
}