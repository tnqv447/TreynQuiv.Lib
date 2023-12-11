using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace TreynQuiv.Lib.Database.EntityFrameworkCore;

/// <summary>
/// An abstract implementation of <see cref="IRepository{}"/> using <see cref="DbContext"/>.
/// </summary>
public abstract class EFCoreRepository<TEntity>(DbContext context)
    : BaseEFCoreRepository<TEntity>(context), IRepository<TEntity> where TEntity : class, IEntity
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
