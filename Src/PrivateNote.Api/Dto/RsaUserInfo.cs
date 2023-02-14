namespace PrivateNote.Api.Dto;

public class RsaUserInfo : UserInfo, IRsaUser
{
    public string PublicKey { get; init; } = String.Empty;

    public RsaUserInfo(Guid id, string userName) : base(id, userName)
    {
    }
}
