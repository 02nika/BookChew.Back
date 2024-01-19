using Entities.Models;
using Repository.Context;
using Repository.Contracts;

namespace Repository.Repositories;

public class RestaurantRepository(AppDbContext appDb) : RepositoryBase<Restaurant>(appDb), IRestaurantRepository
{
    public IQueryable<Restaurant> Restaurants() => FindAll();
    public async Task AddRestaurantAsync(Restaurant restaurant) => await Create(restaurant);
}