namespace PrivateNote.Contract.Entity;
public interface IAudited<TUserKey> : ICreationAudited<TUserKey>, IModificationAudited<TUserKey> { }