namespace PrivateNote.Api.Services.Contract;

public interface IUserManager
{
    Task<IdentityResult> CreateAsync(IUser user, string? password = null);
    Task<IUser?> FindByNameAsync(string userName);
    Task<IUser?> FindByPublicKeyAsync(string publicKey);
    Task<bool> CheckPasswordAsync(IUser user, string password);
    Task<IdentityResult> AddToRoleAsync(IUser user, string role);
    Task<IList<string>> GetRolesAsync(IUser user);
}