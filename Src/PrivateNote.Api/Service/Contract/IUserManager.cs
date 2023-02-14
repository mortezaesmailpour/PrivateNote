namespace PrivateNote.Service.Contract;

public interface IUserManager
{
    Task<IdentityResult> CreateAsync(IUser user, string? password = null);
    Task<IUser?> FindByNameAsync(string userName);
    Task<bool> CheckPasswordAsync(IUser user, string password);
    Task<IdentityResult> AddToRoleAsync(IUser user, string role);
    Task<IList<string>> GetRolesAsync(IUser user);
}