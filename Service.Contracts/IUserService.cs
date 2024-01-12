using Shared.Dtos.User;

namespace Service.Contracts;

public interface IUserService
{
   Task AddUserAsync(AddUserDto userDto);
   Task<bool> UserExistsAsync(LoginUserDto userDto);
}