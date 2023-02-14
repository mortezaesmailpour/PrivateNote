using Microsoft.Extensions.Options;

namespace PrivateNote.Service;

public class UserManager : UserManager<RsaUser>, IUserManager
{
    public UserManager(IUserStore<RsaUser> store, IOptions<IdentityOptions> optionsAccessor, IPasswordHasher<RsaUser> passwordHasher, IEnumerable<IUserValidator<RsaUser>> userValidators, IEnumerable<IPasswordValidator<RsaUser>> passwordValidators, ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<RsaUser>> logger) 
        : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
    {
    }

    public Task<IdentityResult> AddToRoleAsync(IUser user, string role) => base.AddToRolesAsync(
        user as RsaUser ?? throw new InvalidCastException("user can not use as as RsaUser"), new List<string> { role });
    public Task<bool> CheckPasswordAsync(IUser user, string password) => base.CheckPasswordAsync(user as RsaUser?? throw new InvalidCastException("user can not use as as RsaUser"), password);
    public Task<IdentityResult> CreateAsync(IUser user, string? password = null) => base.CreateAsync(user as RsaUser?? throw new InvalidCastException("user can not use as as RsaUser"), password??"");
    public Task<IList<string>> GetRolesAsync(IUser user) => base.GetRolesAsync(user as RsaUser?? throw new InvalidCastException("user can not use as as RsaUser"));

    async Task<IUser?> IUserManager.FindByNameAsync(string userName) => await base.FindByNameAsync(userName);

}
