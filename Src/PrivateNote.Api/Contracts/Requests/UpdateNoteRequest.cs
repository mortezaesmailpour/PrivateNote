namespace PrivateNote.Api.Contracts.Requests;

public class UpdateNoteRequest
{
    public Guid Id { get; init; } = default!;
    public string? Title { get; init; } = default!;
    public string? Description { get; init; } = default!;
    public string? PrivateTitle { get; init; } = default!;
    public string? PrivateDescription { get; init; } = default!;

}