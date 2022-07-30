namespace PrivateNote.Api.Dto.Requests;

public class RsaSignUpRequest
{
    [Required]
    public string UserName { get; init; }

    [Required]
    public string PublicKey { get; init; }

    [Required]
    public string Signature { get; init; }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}