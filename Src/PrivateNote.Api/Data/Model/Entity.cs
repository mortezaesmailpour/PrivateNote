using PrivateNote.Contract.Entity;

namespace PrivateNote.Model;
public class Entity<TPrimaryKey, TUserKey> : IAuditedEntity<TPrimaryKey, TUserKey>
{
    public TPrimaryKey Id { get; init; } = default!;
    public TUserKey CreatorUserId { get; init; }= default!;
    public DateTime CreationTime { get; init; }
    public TUserKey LastModifierUserId { get; set; } = default!;
    public DateTime? LastModificationTime { get; set; }
}