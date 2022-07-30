namespace PrivateNote.Service.Model;

public class RsaUser : User, IRsaUser
{
    public string PublickKey { get; }
}