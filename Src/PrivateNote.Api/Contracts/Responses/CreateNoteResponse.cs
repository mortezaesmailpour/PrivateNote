namespace PrivateNote.Api.Contracts.Responses;

public class CreateNoteResponse
{
    public Guid NoteId { get; init; }
    public DateTime? LastModificationTime { get; init; }

}