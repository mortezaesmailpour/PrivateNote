namespace PrivateNote.Api.Domain;

public class Note
{
    public required Guid Id { get; init; } = Guid.NewGuid();
    
    public required string Title { get; init; }
    
    public required string Description { get; init; }
}