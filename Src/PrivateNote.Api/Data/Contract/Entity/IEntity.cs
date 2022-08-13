namespace PrivateNote.Contract.Entity;
public interface IEntity<TPrimaryKey>
{
    public TPrimaryKey Id { get; init; }
}