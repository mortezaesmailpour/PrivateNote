namespace PrivateNote.Api.Dto;

public class RsaUserInfo : UserInfo, IRsaUser
{
    public string PublickKey { get; init; }
}
