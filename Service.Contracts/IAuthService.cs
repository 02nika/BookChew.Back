using Shared.Dtos;
using Shared.Dtos.Auth;

namespace Service.Contracts;

public interface IAuthService
{
    string AuthAsync(AuthRequest authRequest);
}