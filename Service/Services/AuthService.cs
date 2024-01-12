using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Service.Contracts;
using Shared.Config;

namespace Service.Services;

public class AuthService(IOptions<JwtSettings> jwtSettings) : IAuthService
{
    private readonly JwtSettings _jwtSettings = jwtSettings.Value;

    public string AuthAsync()
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[] {
            new Claim(ClaimTypes.Role, "Admin"),
        };
        
        var token = new JwtSecurityToken(_jwtSettings.Issuer,
            _jwtSettings.Issuer,
            claims,
            expires: DateTime.Now.AddMinutes(120),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}