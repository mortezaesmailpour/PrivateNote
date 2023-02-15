namespace PrivateNote.Api.Data.Entity;
public interface ICreationAudited<TUserKey> : ICreationTime
{
    public TUserKey CreatorUserId { get; init; }
}
