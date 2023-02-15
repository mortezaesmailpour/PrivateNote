namespace PrivateNote.Api.Data.Entity;
public interface IEntity<TPrimaryKey>
{
    public TPrimaryKey Id { get; init; }
}