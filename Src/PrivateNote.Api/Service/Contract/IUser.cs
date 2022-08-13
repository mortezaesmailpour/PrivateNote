namespace PrivateNote.Service.Contract;

public interface IUser
{
    Guid Id { get; }
    string UserName { get; }
}