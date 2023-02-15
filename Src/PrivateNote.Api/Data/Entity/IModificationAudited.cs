namespace PrivateNote.Api.Data.Entity;
public interface IModificationAudited<TUserKey> : IModificationTime
{
    public TUserKey LastModifierUserId { get; set; }
}
