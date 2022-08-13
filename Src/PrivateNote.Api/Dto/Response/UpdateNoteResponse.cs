namespace PrivateNote.Api.Dto.Responses;

public class UpdateNoteResponse
{ 
    public Guid NoteId { get; init; }
    public DateTime? LastModificationTime { get; init; }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}
