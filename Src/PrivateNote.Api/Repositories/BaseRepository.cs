using PrivateNote.Contract.Entity;
using PrivateNote.Data;
using Repositories.Contracts;

namespace Repositories;
public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
{
    protected readonly PrivateNoteDbContext _context;
    protected readonly DbSet<TEntity> _entities;
    public BaseRepository(PrivateNoteDbContext context)
    {
        _context = context;
        _entities = context.Set<TEntity>();
    }

    public IQueryable<TEntity> GetQueryableEntities()
    {
        IQueryable<TEntity> queryableEntities = _entities;
        if (typeof(TEntity).GetInterfaces().Contains(typeof(ICreationTime)))
            queryableEntities = _entities.Cast<ICreationTime>().OrderBy(t => t.CreationTime).Cast<TEntity>();
        return queryableEntities;
    }
    public async Task<TEntity?> GetByIdAsync(Guid id)
    {
        return await _entities.SingleOrDefaultAsync(s => s.Id == id);
    }
    public async Task<int> InsertAsync(TEntity entity)
    {
        _entities.Add(entity);
        return await _context.SaveChangesAsync();
    }
    public async Task<int> UpdateAsync(TEntity entity)
    {
        _entities.Update(entity);
        return await _context.SaveChangesAsync();
    }
    public async Task<int> DeleteAsync(Guid id)
    {
        TEntity? entity = _entities.SingleOrDefault(s => s.Id == id);
        _ = entity ?? throw new KeyNotFoundException();
        _entities.Remove(entity);
        return await _context.SaveChangesAsync();
    }
}