namespace WebApp.Application.Services.Interfaces;

public interface IIdentityService
{
    string GetUserName();
    string GetUserToken();
    bool IsLoogedIn { get; }
    Task<bool> Login(string username, string password);
    void Logout();
}
