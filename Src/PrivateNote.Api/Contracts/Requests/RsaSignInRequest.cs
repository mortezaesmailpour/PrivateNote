namespace PrivateNote.Api.Contracts.Requests;

public class RsaSignInRequest
{
    [Required] public string UserName { get; init; } = default!;
    [Required] public string PublicKey { get; init; } = default!;
    [Required] public string Signature { get; init; } = default!;

}