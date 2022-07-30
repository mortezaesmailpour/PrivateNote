namespace PrivateNote.Service.Contract; 

public interface IAuthService
{
    Task<IUser?> GetUserAsync(string userName);
    Task<bool> RegisterAsync(string userName, string password);
    Task<string> AuthenticateAsync(string userName, string password);
    Task<IUser?> GetMyUserAsync();
}