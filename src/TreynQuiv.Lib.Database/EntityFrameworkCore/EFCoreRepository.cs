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
        return Query(predicates).First();
    }

    public virtual TEntity? FirstOrDefault(params Expression<Func<TEntity, bool>>[] predicates)
    {
        return Query(predicates).FirstOrDefault();
    }

    public virtual IReadOnlyList<TEntity> List(params Expression<Func<TEntity, bool>>[] predicates)
    {
        return Query(predicates).ToList().AsReadOnly();
    }

    public virtual IReadOnlyList<TEntity> List(IEnumerable<Expression<Func<TEntity, dynamic>>> includes,
                                               params Expression<Func<TEntity, bool>>[] predicates)
    {
        var query = Query(predicates);
        foreach (var include in includes)
        {
            if (include.Body is MemberExpression)
            {
                query = query.Include(include);
            }
        }

        return query.ToList().AsReadOnly();
    }

    public virtual IReadOnlyList<TEntity> List(PagingOptions pagingOptions, out long totalRecords, out long totalPages, params Expression<Func<TEntity, bool>>[] predicates)
    {
        var query = Query(predicates);

        totalRecords = query.LongCount();
        totalPages = Math.DivRem(totalRecords, pagingOptions.PageSize, out var rem);
        totalPages += (rem > 0 ? 1 : 0);

        query.Skip(pagingOptions.PageSize * pagingOptions.FromPage)
            .Take(pagingOptions.PageSize * pagingOptions.PageRangeSize);

        return query.ToList().AsReadOnly();
    }

    public virtual int Count(params Expression<Func<TEntity, bool>>[] predicates)
    {
        return Query(predicates).Count();
    }

    public virtual long LongCount(params Expression<Func<TEntity, bool>>[] predicates)
    {
        return Query(predicates).LongCount();
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
