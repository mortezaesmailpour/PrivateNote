namespace PrivateNote.Service.Contract;

public interface IRsaUser : IUser
{
    string PublickKey { get; }
}