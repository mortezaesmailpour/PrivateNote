using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;

namespace PrivateNote.Service;

public class RsaAuthService : AuthService , IRsaAuthService
{
    private readonly IRsaService _rsaService;

    public RsaAuthService(IUserManager userManager, IClaimService claimService, ITokenService tokenService,
        ILoggerAdapter<AuthService> logger) : base(userManager,claimService,tokenService,logger)
    {
        _rsaService = new RsaService();
    }

    public async Task<bool> RsaRegisterAsync(string userName, string publicKey, string signature)
    {
        var user = await GetUserAsync(userName);
        _ = user ?? throw new InvalidDataException();
        var isSignatureValid = _rsaService.Verify(userName, signature, publicKey);
        if (!isSignatureValid) throw new InvalidDataException();
        var result = await _userManager.CreateAsync(new User { UserName = userName, });
        return result == IdentityResult.Success;
    }

    public async Task<string> RsaAuthenticateAsync(string userName, string publicKey, string signature)
    {
        var user = await GetUserAsync(userName);
        _ = user ?? throw new InvalidDataException();
        var isSignatureValid = _rsaService.Verify(userName, signature,publicKey);
        if (!isSignatureValid) throw new InvalidDataException();
        var roles = await _userManager.GetRolesAsync(user);
        var token = _tokenService.GenerateToken(user.UserName, roles);
        var encryptedToken = _rsaService.Encrypte(token, publicKey);
        _ = encryptedToken ?? throw new InvalidDataException();
        return encryptedToken;
    }
}