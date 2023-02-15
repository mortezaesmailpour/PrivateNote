namespace PrivateNote.Api.Domain;

public class Note
{
    public  Guid Id { get; init; } = Guid.NewGuid();
    
    public  string Title { get; init; }
    
    public  string Description { get; init; }
}