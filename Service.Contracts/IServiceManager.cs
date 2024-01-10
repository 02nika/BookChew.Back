namespace Service.Contracts;

public interface IServiceManager
{
    IAuthService AuthService { get; }
    IUserService UserService { get; }
    IRestaurantService RestaurantService { get; }
}