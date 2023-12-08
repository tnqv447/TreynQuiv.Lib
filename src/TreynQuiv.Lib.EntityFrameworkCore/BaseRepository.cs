using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace TreynQuiv.Lib.EntityFrameworkCore;

public class BaseRepository<TEntity>(DbContext context) : IRepository<TEntity> where TEntity : class, IEntity
{
    protected readonly DbContext _context = context;
    protected readonly DbSet<TEntity> _dbSet = context.Set<TEntity>();

    public virtual TEntity? Single(Expression<Func<TEntity, bool>> predicate)
    {
        return _dbSet.FirstOrDefault(predicate);
    }
    public virtual TEntity? Single(IEnumerable<Expression<Func<TEntity, bool>>> predicates)
    {
        var query = _dbSet.AsQueryable();
        foreach (var predicate in predicates)
        {
            query = query.Where(predicate);
        }

        return query.FirstOrDefault();
    }

    public virtual IList<TEntity> List(Expression<Func<TEntity, bool>>? predicate = null)
    {
        var query = predicate is null ? _dbSet : _dbSet.Where(predicate);
        return query.ToList();
    }

    public virtual IList<TEntity> List(IEnumerable<Expression<Func<TEntity, bool>>> predicates)
    {
        var query = _dbSet.AsQueryable();
        foreach (var predicate in predicates)
        {
            query = query.Where(predicate);
        }

        return query.ToList();
    }

    public virtual TEntity Add(TEntity entity)
    {
        var entry = _dbSet.Add(entity);
        return entity;
    }

    public virtual TEntity Update(TEntity entity)
    {
        var entry = _context.Attach(entity);
        entry.State = EntityState.Modified;
        return entity;
    }

    public void Delete(TEntity entity)
    {
        if (_context.Entry(entity).State == EntityState.Detached)
        {
            _dbSet.Attach(entity);
        }

        _dbSet.Remove(entity);
    }
}
