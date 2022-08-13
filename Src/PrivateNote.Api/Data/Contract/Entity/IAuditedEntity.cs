namespace PrivateNote.Contract.Entity;
public interface IAuditedEntity<TPrimaryKey, TUserKey> : IEntity<TPrimaryKey>, IAudited<TUserKey> { }