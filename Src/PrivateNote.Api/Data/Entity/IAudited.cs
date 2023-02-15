namespace PrivateNote.Api.Data.Entity;
public interface IAudited<TUserKey> : ICreationAudited<TUserKey>, IModificationAudited<TUserKey> { }