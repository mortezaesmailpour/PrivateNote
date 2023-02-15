namespace PrivateNote.Api.Data.Entity;
public interface ICreationAuditedEntity<TPrimaryKey, TUserKey> : IEntity<TPrimaryKey>, ICreationAudited<TUserKey> { }