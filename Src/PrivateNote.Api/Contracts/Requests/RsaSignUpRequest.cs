namespace PrivateNote.Api.Contracts.Requests;

public class RsaSignUpRequest
{
    [Required] public string UserName { get; init; } = string.Empty;
    [Required] public string PublicKey { get; init; } = string.Empty;
    [Required] public string Signature { get; init; } = string.Empty;

}