namespace PrivateNote.Api.Services.Contract;

public interface IRsaUser : IUser
{
    string PublicKey { get; }
}