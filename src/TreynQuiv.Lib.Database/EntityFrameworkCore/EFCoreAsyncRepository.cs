using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using TreynQuiv.Lib.Components;

namespace TreynQuiv.Lib.Database.EntityFrameworkCore;

/// <summary>
/// An abstract implementation of <see cref="IAsyncRepository{}"/> using <see cref="DbContext"/>.
/// </summary>
public abstract class EFCoreAsyncRepository<TEntity>(DbContext context)
    : BaseEFCoreRepository<TEntity>(context), IEFCoreAsyncRepository<TEntity> where TEntity : class, IEntity
{
    public virtual async Task<IReadOnlyList<TEntity>> GetAllAsync()
    {
        return (await _dbSet.ToListAsync()).AsReadOnly();
    }
    public virtual async Task<TEntity> FirstAsync(params Expression<Func<TEntity, bool>>[] predicates)
    {
        return await InQuery(predicates).FirstAsync();
    }

    public virtual async Task<TEntity?> FirstOrDefaultAsync(params Expression<Func<TEntity, bool>>[] predicates)
    {
        return await InQuery(predicates).FirstOrDefaultAsync();
    }

    public virtual async Task<IReadOnlyList<TEntity>> ListAsync(params Expression<Func<TEntity, bool>>[] predicates)
    {
        return (await InQuery(predicates).ToListAsync()).AsReadOnly();
    }

    public virtual async Task<(IReadOnlyList<TEntity> records, long totalRecords, long totalPages)> ListAsync(PagingOptions pagingOptions, params Expression<Func<TEntity, bool>>[] predicates)
    {
        var records = await InQuery(predicates)
            .Paging(pagingOptions, out var totalRecords, out var totalPages)
            .ToListAsync();
        return (records.AsReadOnly(), totalRecords, totalPages);
    }

    public virtual async Task<int> CountAsync(params Expression<Func<TEntity, bool>>[] predicates)
    {
        return await InQuery(predicates).CountAsync();
    }

    public virtual async Task<long> LongCountAsync(params Expression<Func<TEntity, bool>>[] predicates)
    {
        return await InQuery(predicates).LongCountAsync();
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
}
