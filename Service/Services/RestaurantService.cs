using Repository.Contracts;
using Service.Contracts;

namespace Service.Services;

public class RestaurantService(IRepositoryManager repositoryManager) : IRestaurantService
{
    private readonly IRepositoryManager _repositoryManager = repositoryManager;
}