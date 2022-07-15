namespace PrivateNote.Service.Contract;

public interface ITokenService
{
    string GenerateToken(string userName, IList<string> roles);
}