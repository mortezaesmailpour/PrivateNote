namespace PrivateNote.Api.Dto;

public class UserInfo : IUser
{
    public UserInfo(Guid id, string userName)
    {
        Id = id;
        UserName = userName;
    }

    public Guid Id { get; init; }
    public string UserName { get; init; }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}
