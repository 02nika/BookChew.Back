using Shared.Dto.User;

namespace Service.Contracts;

public interface IUserService
{
   Task<UserDto> AddUserAsync(AddUserDto userDto);
   Task<UserDto> GetUserAsync(LoginUserDto userDto);
   Task<UserDto> GetUserAsync(int userId);
}