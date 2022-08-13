using PrivateNote.Contract.Entity;

namespace Repositories.Contracts;
public interface IBaseRepository<TEntity> where TEntity : IBaseEntity
{
    IQueryable<TEntity> GetQueryableEntities();
    Task<TEntity?> GetByIdAsync(Guid id);
    Task<int> InsertAsync(TEntity entity);
    Task<int> UpdateAsync(TEntity entity);
    Task<int> DeleteAsync(Guid id);
}