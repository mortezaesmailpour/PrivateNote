using PrivateNote.Api.Data;
using PrivateNote.Api.Data.Model;

namespace PrivateNote.Api.Repositories;

public class NoteRepository : BaseRepository<RsaNote>, INoteRepository
{
    public NoteRepository(PrivateNoteDbContext context) : base(context) { }
}
