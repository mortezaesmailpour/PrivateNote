namespace PrivateNote.Model;

public class RsaUser : User, IRsaUser
{
    public string PublicKey { get; init; } = string.Empty;
}