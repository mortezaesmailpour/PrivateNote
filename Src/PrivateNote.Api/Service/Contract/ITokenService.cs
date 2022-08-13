namespace PrivateNote.Service.Contract;

public interface ITokenService
{
    string GenerateToken(string userId, string userName, IList<string> roles);
}