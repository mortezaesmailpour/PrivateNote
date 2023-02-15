namespace PrivateNote.Api.Contracts.Requests;

public class CreateNoteRequest
{
    public string Title { get; init; } = default!;
    public string Description { get; init; } = default!;
    public string PrivateTitle { get; init; } = default!;
    public string PrivateDescription { get; init; } = default!;
}