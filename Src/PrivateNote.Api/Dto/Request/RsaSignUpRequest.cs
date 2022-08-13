namespace PrivateNote.Api.Dto.Requests;

public class RsaSignUpRequest
{
    [Required]
    public string UserName { get; init; } = string.Empty;

    [Required]
    public string PublicKey { get; init; } = string.Empty;

    [Required]
    public string Signature { get; init; } = string.Empty;

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}