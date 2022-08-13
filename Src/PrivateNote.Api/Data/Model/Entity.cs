using PrivateNote.Contract.Entity;

namespace PrivateNote.Model;
public class Entity<TPrimaryKey, TUserKey> : IAuditedEntity<TPrimaryKey, TUserKey>
{
    public TPrimaryKey Id { get; init; }
    public TUserKey CreatorUserId { get; init; }
    public DateTime CreationTime { get; init; }
    public TUserKey LastModifierUserId { get; set; }
    public DateTime? LastModificationTime { get; set; }
}