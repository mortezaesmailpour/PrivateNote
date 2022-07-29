namespace PrivateNote.Api.Dto.Responses;

public class RsaSignInResponse
{
    public string EncryptedToken { get; init; }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}
