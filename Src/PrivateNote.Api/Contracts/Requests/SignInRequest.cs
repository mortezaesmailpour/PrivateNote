namespace PrivateNote.Api.Contracts.Requests;

public class SignInRequest
{
    [Required] public string UserName { get; init; } = string.Empty;

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; init; } = string.Empty;

}