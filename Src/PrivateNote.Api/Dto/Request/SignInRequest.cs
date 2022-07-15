namespace PrivateNote.Api.Dto.Requests;

public class SignInRequest
{
    [Required]
    public string UserName { get; init; }

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; init; }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}