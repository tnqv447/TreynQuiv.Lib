using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace TreynQuiv.Lib.Database.EntityFrameworkCore;

/// <summary>
/// An abstract implementation of <see cref="IAsyncRepository{}"/> using <see cref="DbContext"/>.
/// </summary>
public abstract class EFCoreAsyncRepository<TEntity>(DbContext context)
    : BaseEFCoreRepository<TEntity>(context), IAsyncRepository<TEntity> where TEntity : class, IEntity
{
    public virtual async Task<TEntity> FirstAsync(params Expression<Func<TEntity, bool>>[] predicates)
    {
        return await Query(predicates).FirstAsync();
    }

    public virtual async Task<TEntity?> FirstOrDefaultAsync(params Expression<Func<TEntity, bool>>[] predicates)
    {
        return await Query(predicates).FirstOrDefaultAsync();
    }

    public virtual async Task<IReadOnlyList<TEntity>> ListAsync(params Expression<Func<TEntity, bool>>[] predicates)
    {
        return (await Query(predicates).ToListAsync()).AsReadOnly();
    }

    public async Task<TEntity> AddAsync(TEntity entity)
    {
        await _dbSet.AddAsync(entity);
        return entity;
    }

    public async Task<TEntity> UpdateAsync(TEntity entity)
    {
        _dbSet.Update(entity);
        await Task.CompletedTask;
        return entity;
    }

    public async Task DeleteAsync(TEntity entity)
    {
        _dbSet.Remove(entity);
        await Task.CompletedTask;
    }

    public TEntity First(params Expression<Func<TEntity, bool>>[] predicates)
    {
        throw new NotImplementedException();
    }

    public TEntity? FirstOrDefault(params Expression<Func<TEntity, bool>>[] predicates)
    {
        throw new NotImplementedException();
    }

    public IReadOnlyList<TEntity> List(params Expression<Func<TEntity, bool>>[] predicates)
    {
        throw new NotImplementedException();
    }

    public TEntity Add(TEntity entity)
    {
        throw new NotImplementedException();
    }

    public TEntity Update(TEntity entity)
    {
        throw new NotImplementedException();
    }

    public void Remove(TEntity entity)
    {
        throw new NotImplementedException();
    }
}
