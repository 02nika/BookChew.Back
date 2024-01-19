using Entities.Models;

namespace Repository.Contracts;

public interface IRestaurantRepository
{
    IQueryable<Restaurant> Restaurants();
    Task AddRestaurantAsync(Restaurant restaurant);
}