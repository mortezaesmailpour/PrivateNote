﻿using PrivateNote.Api.Contracts.Requests;
using PrivateNote.Api.Data.Model;
using PrivateNote.Api.Repositories;
using PrivateNote.Api.Services.Contract;

namespace PrivateNote.Api.Services;
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
        var userId = _claimService.GetUserId() ?? throw new KeyNotFoundException("User Id not found in the claims");
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


    public async Task<IEnumerable<RsaNote>> GetAllNotes() => await _repository.GetQueryableEntities().ToListAsync();

    public async Task<IEnumerable<RsaNote>> GetNotesByUserId(Guid userId) => await _repository.GetQueryableEntities()
        .Where(n => n.CreatorUserId == userId).ToListAsync();

    public Task<IEnumerable<RsaNote>> GetNotesByUser(RsaUser user) => GetNotesByUserId(user.Id);

    public Task<IEnumerable<RsaNote>> GetMyNotesAsync() => GetNotesByUserId(_claimService.GetUserId()?? throw new KeyNotFoundException("UserId not found in the claims"));

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
        if (userId != null) note.LastModifierUserId = userId.Value;
        var result = await _repository.UpdateAsync(note);
        return result < 1 ? null : note;
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