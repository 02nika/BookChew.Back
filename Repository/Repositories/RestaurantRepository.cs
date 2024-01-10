using Entities.Models;
using Repository.Context;
using Repository.Contracts;

namespace Repository.Repositories;

public class RestaurantRepository(AppDbContext appDbContext) : RepositoryBase<Restaurant>(appDbContext), IRestaurantRepository;