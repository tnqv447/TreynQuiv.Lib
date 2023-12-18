using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using TreynQuiv.Lib.Components;

namespace TreynQuiv.Lib.Database.EntityFrameworkCore;

/// <summary>
/// An abstract implementation of <see cref="IRepository{}"/> using <see cref="DbContext"/>.
/// </summary>
public abstract class EFCoreRepository<TEntity>(DbContext context)
    : BaseEFCoreRepository<TEntity>(context), IEFCoreRepository<TEntity> where TEntity : class, IEntity
{
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
}
