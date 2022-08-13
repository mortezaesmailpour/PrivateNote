namespace PrivateNote.Api.Dto;

public class UserInfo : IUser
{
    public Guid Id { get; init; }
    public string UserName { get; init; }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}
