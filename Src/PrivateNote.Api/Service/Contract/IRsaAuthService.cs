namespace PrivateNote.Service.Contract; 

public interface IRsaAuthService : IAuthService
{
    Task<bool> RsaRegisterAsync(string userName, string publickKey, string signature);
    Task<string> RsaAuthenticateAsync(string userName, string publickKey, string signature);
}