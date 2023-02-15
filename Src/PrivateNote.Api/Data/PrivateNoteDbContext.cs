using PrivateNote.Api.Data.Model;

namespace PrivateNote.Api.Data;
public class PrivateNoteDbContext : IdentityDbContext<RsaUser, Role, Guid>
{
    public PrivateNoteDbContext(DbContextOptions<PrivateNoteDbContext> options)
        : base(options)
    {
    }
    public DbSet<RsaNote>? Notes { get; set; }
}