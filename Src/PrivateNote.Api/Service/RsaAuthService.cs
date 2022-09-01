namespace PrivateNote.Service;

public class RsaAuthService : AuthService, IRsaAuthService
{
    private readonly IRsaService _rsaService;

    public RsaAuthService(IUserManager userManager, IClaimService claimService, ITokenService tokenService,
        ILoggerAdapter<AuthService> logger) : base(userManager, claimService, tokenService, logger)
    {
        _rsaService = new RsaService();
    }

    public async Task<IdentityResult> RsaRegisterAsync(string userName, string publicKey, string signature)
    {
        var user = await GetUserAsync(userName);
        if (user is null)
            return IdentityResult.Failed(new IdentityError() { Description = "user not found" });
        if (!_rsaService.Verify(userName, signature, publicKey))
            return IdentityResult.Failed(new IdentityError() { Description = "signature is not valid" });
        return await _userManager.CreateAsync(new User { UserName = userName, });
    }

    public async Task<(IdentityResult, string)> RsaAuthenticateAsync(string userName, string publicKey,
        string signature)
    {
        var user = await GetUserAsync(userName);
        if (user is null)
            return (IdentityResult.Failed(new IdentityError() { Description = "user not found" }), "");
        var isSignatureValid = _rsaService.Verify(userName, signature, publicKey);
        if (!isSignatureValid)
            return (IdentityResult.Failed(new IdentityError() { Description = "signature is not valid" }), "");
        var roles = await _userManager.GetRolesAsync(user);
        var token = _tokenService.GenerateToken(user.Id.ToString(), user.UserName, roles);
        var encryptedToken = _rsaService.Encrypte(token, publicKey);
        if (string.IsNullOrEmpty(encryptedToken))
            return (IdentityResult.Failed(new IdentityError() { Description = "encryption was failed" }), "");
        return (IdentityResult.Success, encryptedToken);
    }
}