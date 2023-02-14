namespace PrivateNote.Service.Contract;

public interface IRsaUser : IUser
{
    string PublicKey { get; }
}