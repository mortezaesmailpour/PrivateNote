namespace PrivateNote.Api.Data.Entity;
public interface IAuditedEntity<TPrimaryKey, TUserKey> : IEntity<TPrimaryKey>, IAudited<TUserKey> { }