namespace PrivateNote.Service;

public class RsaAuthService : AuthService, IRsaAuthService
{
    private readonly IRsaService _rsaService;

    public RsaAuthService(IUserManager userManager, IClaimService claimService, ITokenService tokenService,
        ILoggerAdapter<AuthService> logger) : base(userManager, claimService, tokenService, logger)
    {
        _rsaService = new RsaService();
    }

    public async Task<IdentityResult?> RsaRegisterAsync(string userName, string publicKey, string signature)
    {
        var user = await GetUserAsync(userName);
        if (user is not null)
            return IdentityResult.Failed(new IdentityError() { Description = "user already exist" });
        if (!_rsaService.Verify(userName, signature, publicKey))
            return IdentityResult.Failed(new IdentityError() { Description = "signature is not valid" });
        return await _userManager.CreateAsync(new RsaUser { UserName = userName, PublicKey = publicKey});
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
        var isSignatureValid = _rsaService.Verify(userName, signature, publicKey);
        if (!isSignatureValid)
        {
            _logger.LogError("signature is not valid");
            return null;
        }
        var roles = await _userManager.GetRolesAsync(user);
        var token = _tokenService.GenerateToken(user.Id.ToString(), user.UserName!, roles);
        var encryptedToken = _rsaService.Encrypte(token, publicKey);
        if (!string.IsNullOrEmpty(encryptedToken)) return encryptedToken;
        _logger.LogError("encryption was failed");
        return null;
    }
}