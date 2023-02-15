namespace PrivateNote.Api.Services.Contract;

public interface IUser
{
    Guid Id { get; }
    string UserName { get; }
}