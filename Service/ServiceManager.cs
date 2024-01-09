using Microsoft.Extensions.Options;
using Service.Contracts;
using Shared.Config;

namespace Service;

public class ServiceManager(IOptions<JwtSettings> jwtSettings) : IServiceManager
{
    private readonly Lazy<IAuthService> _authService = new(() => new AuthService(jwtSettings));

    public IAuthService AuthService => _authService.Value;
}