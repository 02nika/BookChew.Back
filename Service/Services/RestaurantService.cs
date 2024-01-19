using AutoMapper;
using Entities.Exceptions.Custom;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Contracts;
using Service.Contracts;
using Shared.Dto.Restaurant;

namespace Service.Services;

public class RestaurantService(IRepositoryManager repositoryManager, IMapper mapper) : IRestaurantService
{
    public async Task<List<RestaurantDto>> RestaurantsAsync()
    {
        var restaurants = repositoryManager.RestaurantRepository.Restaurants();

        return mapper.Map<List<RestaurantDto>>(await restaurants.ToListAsync());
    }

    public async Task AddRestaurantAsync(AddRestaurantDto restaurantDto, int userId)
    {
        var user = await repositoryManager.UserRepository.GetUserAsync(userId);

        if (user is null) throw new UserNotFoundException();
        
        var restaurant = mapper.Map<Restaurant>(restaurantDto);
        restaurant.UserId = user.Id;
        
        await repositoryManager.RestaurantRepository.AddRestaurantAsync(restaurant);
        await repositoryManager.SaveAsync();
    }
}