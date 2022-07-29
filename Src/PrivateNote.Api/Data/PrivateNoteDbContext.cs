using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace PrivateNote.Api.Data;
public class PrivateNoteDbContext : IdentityDbContext<RsaUser, Role, Guid>
{
    public PrivateNoteDbContext(DbContextOptions<PrivateNoteDbContext> options)
        : base(options)
    {
    }
}