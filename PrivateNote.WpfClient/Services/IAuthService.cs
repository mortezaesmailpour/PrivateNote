using System.Threading.Tasks;

namespace PrivateNote.WpfClient.Services;

public interface IAuthService
{
    bool IsAuthenticated { get; }
    Task<bool> Authenticate(string username, string privateKey);
}