namespace PrivateNote.Contract.Entity;
public interface ICreationAudited<TUserKey> : ICreationTime
{
    public TUserKey CreatorUserId { get; init; }
}
