using PrivateNote.Data;
using Repositories.Contracts;

namespace Repositories;

public class NoteRepository : BaseRepository<RsaNote>, INoteRepository
{
    public NoteRepository(PrivateNoteDbContext context) : base(context) { }
}
