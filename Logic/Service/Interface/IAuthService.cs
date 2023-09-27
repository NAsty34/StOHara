
namespace Logic.Service.Interface;

public interface IAuthService
{
    Task<string> Login(string email, string password);
}