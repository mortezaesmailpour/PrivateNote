namespace PrivateNote.Api.Dto.Requests;

public class SignInRequest
{
    [Required]
    public string UserName { get; init; } = string.Empty;

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; init; } = string.Empty;

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}