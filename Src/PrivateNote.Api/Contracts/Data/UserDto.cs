using PrivateNote.Api.Services.Contract;

namespace PrivateNote.Api.Contracts.Data;

public class UserDto :IUser
{
    public Guid Id { get; init; } = default!;
    public string UserName { get; init; } = default!;

    public string PublicKey { get; init; } = default!;
}