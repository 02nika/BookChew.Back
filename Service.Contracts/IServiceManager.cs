namespace Service.Contracts;

public interface IServiceManager
{
    IAuthService AuthService { get; }
}