using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Shared.Dto.Auth;

namespace Service.Contracts;

public interface IAuthService
{
    AuthResponse Auth(int userId);
    JwtSecurityToken? Decode(string tokenHash);
    List<Claim> TokenClaims(string tokenHash);
    int GetUserId(string tokenHash);
}