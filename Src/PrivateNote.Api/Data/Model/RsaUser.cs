using PrivateNote.Api.Services.Contract;

namespace PrivateNote.Api.Data.Model;

public class RsaUser : User, IRsaUser
{
    public string PublicKey { get; init; } = string.Empty;
}