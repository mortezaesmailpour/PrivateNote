namespace PrivateNote.Service.Contract;

public interface IAuthService : IAuthService<IUser> { }
public interface IAuthService<TUser>
{
    Task<TUser?> GetUserAsync(string userName);
    Task<IdentityResult> RegisterAsync(string userName, string password);
    Task<(IdentityResult,string)> AuthenticateAsync(string userName, string password);
    Task<TUser?> GetMyUserAsync();
}