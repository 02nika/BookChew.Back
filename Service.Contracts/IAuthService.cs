using System.IdentityModel.Tokens.Jwt;
using Shared.Dtos.Auth;

namespace Service.Contracts;

public interface IAuthService
{
    AuthResponse Auth();
    JwtSecurityToken? Decode(string tokenHash);
    bool TokenIsValid(string tokenHash);
}