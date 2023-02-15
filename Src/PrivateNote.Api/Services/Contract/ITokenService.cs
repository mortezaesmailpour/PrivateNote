namespace PrivateNote.Api.Services.Contract;

public interface ITokenService
{
    string GenerateToken(string userId, string userName, IList<string> roles);
}