public interface IAuthService
{
    Task<IUser?> GetUserAsync(string userName);
    Task<bool> RegisterAsync(string email, string password);
    Task<string> AuthenticateAsync(string userName, string password);
    Task<IUser?> GetMyUserAsync();
}