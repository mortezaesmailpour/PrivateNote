namespace PrivateNote.Contract.Entity;
public interface ICreationAuditedEntity<TPrimaryKey, TUserKey> : IEntity<TPrimaryKey>, ICreationAudited<TUserKey> { }