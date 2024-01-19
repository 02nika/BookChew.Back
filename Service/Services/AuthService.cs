using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Entities.Exceptions.Custom;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Service.Contracts;
using Shared.Config;
using Shared.Dto.Auth;
using Shared.Dto.Policies;

namespace Service.Services;

public class AuthService(IOptions<JwtSettings> jwtSettings) : IAuthService
{
    private readonly JwtSettings _jwtSettings = jwtSettings.Value;
    private static readonly TimeSpan TokenLifetime = TimeSpan.FromMinutes(10);
    private static readonly TimeSpan RefreshLifetime = TokenLifetime.Add(TimeSpan.FromMinutes(10));

    public AuthResponse Auth(int userId)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        var accessToken = tokenHandler.CreateToken(TokenDescriptor(TokenLifetime, userId));
        var refreshToken = tokenHandler.CreateToken(TokenDescriptor(RefreshLifetime, userId));

        return new AuthResponse
        {
            AccessToken = tokenHandler.WriteToken(accessToken),
            RefreshToken = tokenHandler.WriteToken(refreshToken)
        };
    }

    private SecurityTokenDescriptor TokenDescriptor(TimeSpan expires, int userId)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(PolicyData.AdminClaimName, true.ToString()),
            new Claim(PolicyData.UserIdClaimName, userId.ToString()),
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

    public List<Claim> TokenClaims(string tokenHash)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_jwtSettings.Key);

        try
        {
            var principal = tokenHandler.ValidateToken(tokenHash, new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _jwtSettings.Issuer,
                ValidAudience = _jwtSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(key)
            }, out _);

            return principal.Claims.ToList();
        }
        catch (Exception)
        {
            throw new TokenValidException();
        }
    }

    public int GetUserId(string tokenHash)
    {
        var tokenClaims = TokenClaims(tokenHash);

        var userId = tokenClaims.First(c => c.Type == PolicyData.UserIdClaimName).Value;

        return int.Parse(userId);
    }
}