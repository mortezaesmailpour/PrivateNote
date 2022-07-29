namespace PrivateNote.Service;

public class UserManager : IUserManager
{
    public Task<bool> CheckPasswordAsync(IUser user, string password) => Task.FromResult(user.UserName == "Alice" || user.UserName=="Bob");
    public Task<IdentityResult> CreateAsync(IUser user, string? password=null) => Task.FromResult(IdentityResult.Success);
    public Task<IUser> FindByNameAsync(string userName) => Task.FromResult(new User() { UserName = userName } as IUser);
    public Task<IdentityResult> AddToRoleAsync(IUser user, string role) => Task.FromResult(IdentityResult.Success);
    public Task<IList<string>> GetRolesAsync(IUser user) => Task.FromResult(new List<string>() { "admin", "user" } as IList<string>);
}
