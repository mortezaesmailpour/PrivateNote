namespace PrivateNote.Api.Domain;

public class User
{
    public required Guid Id { get; init; } = Guid.NewGuid();
    
    public required string Username { get; init; }
    
    public required string PublicKey { get; init; }
}