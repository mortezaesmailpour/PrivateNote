using PrivateNote.Api.Data.Model;
using PrivateNote.Api.Services.Contract;

namespace PrivateNote.Api.Services;

public class RsaAuthService : AuthService, IRsaAuthService
{
    public RsaAuthService(IUserManager userManager, IClaimService claimService, ITokenService tokenService,
        ILoggerAdapter<AuthService> logger) : base(userManager, claimService, tokenService, logger)
    {
    }

    public async Task<IdentityResult?> RsaRegisterAsync(string userName, string publicKey, string signature)
    {
        var user = await GetUserAsync(userName);
        if (user is not null)
            return IdentityResult.Failed(new IdentityError() { Description = "user already exist" });
        if (!RsaService.Verify(userName, signature, publicKey))
            return IdentityResult.Failed(new IdentityError() { Description = "signature is not valid" });
        var userWithSamePublicKey = await UserManager.FindByPublicKeyAsync(publicKey);
        if (userWithSamePublicKey is not null)
            return IdentityResult.Failed(new IdentityError() { Description = "a user with the same PublicKey already exist" });
        return await UserManager.CreateAsync(new RsaUser { UserName = userName, PublicKey = publicKey}, signature);
    }

    public async Task<string?> RsaAuthenticateAsync(string userName, string publicKey,
        string signature)
    {
        var user = await GetUserAsync(userName);
        if (user is null)
        {
            _logger.LogError("user not found");
            return null;
        }
        if ((user as RsaUser)?.PublicKey != publicKey)
        {
            _logger.LogError("publicKeys are not matched");
            return null;
        }
        var isSignatureValid = RsaService.Verify(userName, signature, publicKey);
        if (!isSignatureValid)
        {
            _logger.LogError("signature is not valid");
            return null;
        }
        var roles = await UserManager.GetRolesAsync(user);
        var token = TokenService.GenerateToken(user.Id.ToString(), user.UserName!, roles);
        var encryptedToken = RsaService.Encrypt(token, publicKey);
        if (!string.IsNullOrEmpty(encryptedToken)) return encryptedToken;
        _logger.LogError("encryption was failed");
        return null;
    }
}