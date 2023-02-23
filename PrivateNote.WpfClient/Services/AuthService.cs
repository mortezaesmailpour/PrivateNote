using PrivateNote.Api.Services;
using PrivateNote.Tests.E2E.ApiLayer;
using System.Threading.Tasks;

namespace PrivateNote.WpfClient.Services;

internal class AuthService : IAuthService
{
    private bool _authenticated;

    private readonly PrivateNoteClient _client = new ();
    public bool IsAuthenticated => _authenticated;
    
    public async Task<bool> Authenticate(string username, string privateKey)
    {
        var publicKey = RsaService.GetPublicKeyFromPrivateKey(privateKey);
        var signature = RsaService.Sign(username, privateKey);
        var encryptedToken = await _client.RsaSignInAsync(username, publicKey, signature);
        if (string.IsNullOrEmpty(encryptedToken)) return false;
        var token = RsaService.Decrypt(encryptedToken, privateKey);
        if (string.IsNullOrEmpty(token)) return false;
        var user = await _client.WhoAmI(token);
        if (user is null) return false;
        return user?.UserName == username;
    }
}
