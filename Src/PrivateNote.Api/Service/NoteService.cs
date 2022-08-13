using PrivateNote.Api.Dto.Request;
using PrivateNote.Model;
using Repositories.Contracts;

namespace PrivateNote.Service;
public class NoteService : INoteService
{
    private readonly IClaimService _claimService;
    private readonly INoteRepository _repository;
    private readonly ILoggerAdapter<NoteService> _logger;

    public NoteService(IClaimService claimService, INoteRepository repository, ILoggerAdapter<NoteService> logger)
    {
        _claimService = claimService;
        _repository = repository;
        _logger = logger;
    }

    public async Task<RsaNote?> CreateNoteAsync(CreateNoteRequest request)
    {
        var userId = _claimService.GetUserId();
        var time = DateTime.Now;
        var note = new RsaNote
        {
            Id = Guid.NewGuid(),
            CreationTime = time,
            CreatorUserId = userId,
            LastModificationTime = time,
            LastModifierUserId = userId,
            Title = request?.Title,
            Description = request?.Description,
            PrivateTitle = request?.PrivateTitle,
            PrivateDescription = request?.PrivateDescription,
        };
        var result = await _repository.InsertAsync(note);
        if (result < 1) return null;
        return note;
    }


    public async Task<IEnumerable<RsaNote>> GetAllNotes(Guid userId) => await _repository.GetQueryableEntities().ToListAsync();

    public Task<IEnumerable<RsaNote>> GetMyNotesAsync() => GetAllNotes(_claimService.GetUserId());

    public Task<RsaNote?> GetNoteAsync(Guid noteId) => _repository.GetByIdAsync(noteId);

    public async Task<RsaNote?> UpdateNoteAsync(UpdateNoteRequest request)
    {
        var userId = _claimService.GetUserId();
        var note = await GetNoteAsync(request.Id);
        if (note is null)
            return null;
        note.Title = request.Title;
        note.Description = request.Description;
        note.PrivateTitle = request.PrivateTitle;
        note.PrivateDescription = request.PrivateDescription;
        note.LastModificationTime = DateTime.Now;
        note.LastModifierUserId = userId;
        var result = await _repository.UpdateAsync(note);
        if (result < 1) 
            return null;
        return note;

    }

    public async Task<bool> DeleteNoteAsync(Guid noteId)
    {

        var userId = _claimService.GetUserId();
        var note = await GetNoteAsync(noteId);
        if (note?.CreatorUserId != userId)
            return false;
        return await _repository.DeleteAsync(noteId) > 0;
    }
}