namespace PrivateNote.Api.Domain;

public class User
{
    public  Guid Id { get; init; } = Guid.NewGuid();
    
    public  string Username { get; init; }
    
    public  string PublicKey { get; init; }
}