namespace PrivateNote.Api.Contracts.Responses;

public class UpdateNoteResponse
{
    public Guid NoteId { get; init; }
    public DateTime? LastModificationTime { get; init; }

}