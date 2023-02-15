using PrivateNote.Api.Data.Entity;

namespace PrivateNote.Api.Repositories;
public interface IBaseRepository<TEntity> where TEntity : IBaseEntity
{
    IQueryable<TEntity> GetQueryableEntities();
    Task<TEntity?> GetByIdAsync(Guid id);
    Task<int> InsertAsync(TEntity entity);
    Task<int> UpdateAsync(TEntity entity);
    Task<int> DeleteAsync(Guid id);
}