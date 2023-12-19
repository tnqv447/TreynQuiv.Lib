using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using TreynQuiv.Lib.Components;
using TreynQuiv.Lib.Database.EntityFrameworkCore.Extensions;

namespace TreynQuiv.Lib.Database.EntityFrameworkCore;

/// <summary>
/// An abstract implementation of <see cref="IEFCoreRepository{}"/> using <see cref="DbContext"/>.
/// </summary>
public abstract class EFCoreRepository<TEntity>(DbContext context) : IEFCoreRepository<TEntity> where TEntity : class, IEntity
{
    protected readonly DbContext _context = context;
    protected readonly DbSet<TEntity> _dbSet = context.Set<TEntity>();

    /// <summary>
    /// Query database through all <paramref name="predicates"/>.
    /// </summary>
    /// <param name="predicates"></param>
    /// <returns>
    /// A <see cref="IQueryable{}"/> that contains elements from the input sequence
    /// that satisfy the condition specified by <paramref name="predicates"/>.
    /// </returns>
    protected IQueryable<TEntity> InQuery(params Expression<Func<TEntity, bool>>[] predicates)
    {
        var query = _dbSet.AsQueryable();
        foreach (var predicate in predicates)
        {
            query = query.Where(predicate);
        }

        return query;
    }

    /// <summary>
    /// Query database through all <paramref name="predicates"/>.
    /// </summary>
    /// <param name="predicates"></param>
    /// <returns>
    /// A <see cref="EFCoreQuery{}"/> that contains elements from the input sequence
    /// that satisfy the condition specified by <paramref name="predicates"/>.
    /// </returns>
    public EFCoreQuery<TEntity> Query(params Expression<Func<TEntity, bool>>[] predicates)
    {
        return new(InQuery(predicates));
    }

    #region Synchronous
    public virtual TEntity First(params Expression<Func<TEntity, bool>>[] predicates)
    {
        return InQuery(predicates).First();
    }

    public virtual TEntity? FirstOrDefault(params Expression<Func<TEntity, bool>>[] predicates)
    {
        return InQuery(predicates).FirstOrDefault();
    }

    public virtual IReadOnlyList<TEntity> List(params Expression<Func<TEntity, bool>>[] predicates)
    {
        return InQuery(predicates).ToList().AsReadOnly();
    }

    public virtual IReadOnlyList<TEntity> List(PagingOptions pagingOptions, out long totalRecords, out long totalPages, params Expression<Func<TEntity, bool>>[] predicates)
    {
        return InQuery(predicates)
            .Paging(pagingOptions, out totalRecords, out totalPages)
            .ToList().AsReadOnly();
    }

    public virtual int Count(params Expression<Func<TEntity, bool>>[] predicates)
    {
        return InQuery(predicates).Count();
    }

    public virtual long LongCount(params Expression<Func<TEntity, bool>>[] predicates)
    {
        return InQuery(predicates).LongCount();
    }

    public TEntity Add(TEntity entity)
    {
        _dbSet.Add(entity);
        return entity;
    }

    public TEntity Update(TEntity entity)
    {
        _dbSet.Update(entity);
        return entity;
    }

    public void Remove(TEntity entity)
    {
        _dbSet.Remove(entity);
    }
    #endregion Synchronous

    #region IAsyncRepository
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
    #endregion IAsyncRepository
}
