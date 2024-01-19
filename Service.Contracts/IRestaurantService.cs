using Shared.Dto.Restaurant;

namespace Service.Contracts;

public interface IRestaurantService
{
    Task<List<RestaurantDto>> RestaurantsAsync();
    Task AddRestaurantAsync(AddRestaurantDto restaurantDto, int userId);
}