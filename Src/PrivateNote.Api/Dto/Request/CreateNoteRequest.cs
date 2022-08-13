namespace PrivateNote.Api.Dto.Request;

public class CreateNoteRequest
{
    public string? Title { get; init; }

    public string? Description { get; init; }
    public string? PrivateTitle { get; init; }

    public string? PrivateDescription { get; init; }


    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}