namespace PrivateNote.Model;

public class RsaUser : User, IRsaUser
{
    public string PublickKey { get; } = String.Empty;
}