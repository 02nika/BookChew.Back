using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Entities.Exceptions.Custom;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Service.Contracts;
using Shared.Config;
using Shared.Dtos.Auth;
using Shared.Dtos.Policies;

namespace Service.Services;

public class AuthService(IOptions<JwtSettings> jwtSettings) : IAuthService
{
    private readonly JwtSettings _jwtSettings = jwtSettings.Value;
    private static readonly TimeSpan TokenLifetime = TimeSpan.FromMinutes(1);
    private static readonly TimeSpan RefreshLifetime = TokenLifetime.Add(TimeSpan.FromMinutes(1));

    AuthResponse IAuthService.Auth()
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        var accessToken = tokenHandler.CreateToken(TokenDescriptor(TokenLifetime));
        var refreshToken = tokenHandler.CreateToken(TokenDescriptor(RefreshLifetime));

        return new AuthResponse
        {
            AccessToken = tokenHandler.WriteToken(accessToken),
            RefreshToken = tokenHandler.WriteToken(refreshToken)
        };
    }

    private SecurityTokenDescriptor TokenDescriptor(TimeSpan expires)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(PolicyData.AdminClaimName, true.ToString()),
        };

        return new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.Add(expires),
            Issuer = _jwtSettings.Issuer,
            Audience = _jwtSettings.Audience,
            SigningCredentials = credentials
        };
    }
    
    public JwtSecurityToken? Decode(string tokenHash)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        
        return tokenHandler.ReadToken(tokenHash) as JwtSecurityToken;
    }

    public bool TokenIsValid(string tokenHash)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_jwtSettings.Key);

        try
        {
            tokenHandler.ValidateToken(tokenHash, new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _jwtSettings.Issuer,
                ValidAudience = _jwtSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(key)
            }, out _);

            return true;
        }
        catch (Exception)
        {
            throw new TokenValidException();
        }
    }
}