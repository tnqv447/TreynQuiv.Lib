using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace TreynQuiv.Lib.Database.EntityFrameworkCore;

public abstract class BaseEFCoreRepository<TEntity>(DbContext context) where TEntity : class, IEntity
{
    protected readonly DbContext _context = context;
    protected readonly DbSet<TEntity> _dbSet = context.Set<TEntity>();

    /// <summary>
    /// Returns a <see cref="IQueryable"/> that has compiled all <paramref name="predicates"/>.
    /// </summary>
    protected IQueryable<TEntity> Query(params Expression<Func<TEntity, bool>>[] predicates)
    {
        var query = _dbSet.AsQueryable();
        foreach (var predicate in predicates)
        {
            query = query.Where(predicate);
        }

        return query;
    }
}
