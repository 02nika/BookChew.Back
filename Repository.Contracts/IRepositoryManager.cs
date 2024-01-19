namespace Repository.Contracts;

public interface IRepositoryManager
{
    IUserRepository UserRepository { get; }
    IRestaurantRepository RestaurantRepository { get; }
    Task SaveAsync();
}