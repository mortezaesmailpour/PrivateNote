namespace PrivateNote.Contract.Entity;
public interface IModificationAudited<TUserKey> : IModificationTime
{
    public TUserKey LastModifierUserId { get; set; }
}
