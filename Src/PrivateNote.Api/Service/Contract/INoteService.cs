using PrivateNote.Api.Dto.Request;

namespace PrivateNote.Service.Contract;

public interface INoteService : INoteService<RsaUser, RsaNote>
{
}

public interface INoteService<TUser,TNote>
{
    Task<TNote?> CreateNoteAsync(CreateNoteRequest note);
    Task<TNote?> GetNoteAsync(Guid noteId);
    Task<TNote?> UpdateNoteAsync(UpdateNoteRequest note);
    Task<bool> DeleteNoteAsync(Guid noteId);
    Task<IEnumerable<TNote>> GetAllNotes();
    Task<IEnumerable<TNote>> GetNotesByUser(TUser user);
    Task<IEnumerable<TNote>> GetNotesByUserId(Guid userId);
    Task<IEnumerable<TNote>> GetMyNotesAsync();
}
