using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace TreynQuiv.Lib.Database.EntityFrameworkCore;

public abstract class BaseEFCoreRepository<TEntity>(DbContext context) where TEntity : class, IEntity
{
    protected readonly DbContext _context = context;
    protected readonly DbSet<TEntity> _dbSet = context.Set<TEntity>();

    /// <summary>
    /// Query database through all <paramref name="predicates"/>.
    /// </summary>
    /// <param name="predicates"></param>
    /// <returns>A <see cref="IQueryable{}"/>.</returns>
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
    /// <returns>A <see cref="EFCoreQuery{}"/>.</returns>
    public EFCoreQuery<TEntity> Query(params Expression<Func<TEntity, bool>>[] predicates)
    {
        return new(InQuery(predicates));
    }
}
