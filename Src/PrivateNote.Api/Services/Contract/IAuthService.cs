namespace PrivateNote.Api.Services.Contract;

public interface IAuthService : IAuthService<IUser> { }
public interface IAuthService<TUser>
{
    Task<TUser?> GetUserAsync(string userName);
    Task<IdentityResult> RegisterAsync(string userName, string password);
    Task<string?> AuthenticateAsync(string userName, string password);
    Task<TUser?> GetMyUserAsync();
}