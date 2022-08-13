using PrivateNote.Api.Dto.Request;

namespace PrivateNote.Service.Contract;
public interface INoteService
{
    Task<RsaNote?> CreateNoteAsync(CreateNoteRequest note);
    Task<RsaNote?> GetNoteAsync(Guid noteId);
    Task<RsaNote?> UpdateNoteAsync(UpdateNoteRequest note);
    Task<bool> DeleteNoteAsync(Guid noteId);
    Task<IEnumerable<RsaNote>> GetAllNotes(Guid userId);
    Task<IEnumerable<RsaNote>> GetMyNotesAsync();
}
