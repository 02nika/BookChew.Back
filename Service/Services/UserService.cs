using AutoMapper;
using Bogus;
using Bogus.Extensions.Denmark;
using Entities.Models;
using Repository.Contracts;
using Service.Contracts;
using Shared.Dtos.User;

namespace Service.Services;

public class UserService(IRepositoryManager repositoryManager, IMapper mapper) : IUserService
{
    public async Task AddUserAsync(AddUserDto userDto)
    {
        var user = mapper.Map<User>(userDto);

        await repositoryManager.UserRepository.AddUserAsync(user);
        await repositoryManager.SaveAsync();
    }

    public async Task FillUsersAsync()
    {
        var userFaker = new Faker<User>()
            .RuleFor(u => u.FirstName, f => f.Person.FirstName)
            .RuleFor(u => u.LastName, f => f.Person.LastName)
            .RuleFor(u => u.PersonalNumber, f => f.Person.Cpr());
        var users = userFaker.Generate(40);

        await repositoryManager.UserRepository.AddUsersAsync(users);
        await repositoryManager.SaveAsync();
    }
}