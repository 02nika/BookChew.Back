using Shared.Dtos;

namespace Service.Contracts;

public interface IAuthService
{
    string AuthAsync(AuthRequest authRequest);
}