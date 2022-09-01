namespace PrivateNote.Service.Contract; 

public interface IRsaAuthService : IAuthService
{
    Task<IdentityResult> RsaRegisterAsync(string userName, string publicKey, string signature);
    Task<string?> RsaAuthenticateAsync(string userName, string publicKey, string signature);
}