namespace PrivateNote.Api.Dto.Responses;

public class SignInResponse
{
    public string Token { get; init; }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}
