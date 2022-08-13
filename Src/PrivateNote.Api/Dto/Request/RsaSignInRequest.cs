namespace PrivateNote.Api.Dto.Requests;

public class RsaSignInRequest
{
    [Required]
    public string UserName { get; init; } = String.Empty;

    [Required]
    public string PublicKey { get; init; } = String.Empty;

    [Required]
    public string Signature { get; init; } = String.Empty;

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}